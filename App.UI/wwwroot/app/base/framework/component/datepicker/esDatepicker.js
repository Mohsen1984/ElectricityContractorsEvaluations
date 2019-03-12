(function () {
    var module = angular.module('esDatepicker',[]);

    module.directive('esDatepicker', ["$http", "$parse", "$compile",
        function ($http, $parse, $compile) {
            return {
                require: 'ngModel',
                replace: true,
                restrict: 'A',
                controller: function ($scope, $element, $attrs) {
                },
                compile: function (tElem, tAttrs) {
                    return {
                        pre: function (scope, iElem, iAttrs) {

                        },
                        post: function (scope, iElem, iAttrs, ngModel) {
                            Date.prototype.getJulian = function () {
                                return this.getTime() / 86400000 + 2440587.5;
                            }
                            var isInline = iAttrs.datepickerInline;
                            var calendar = iAttrs.datepickerCalendar;
                            var language = iAttrs.datepickerLanguage;
                            var dateFormat = iAttrs.datepickerDateformat;

                            var displayinput;
                            if (isInline) {
                                displayinput = iElem;
                            }
                            else {
                                displayinput = iElem.clone();
                                iElem.attr("type", "hidden");
                                displayinput.removeAttr("datepicker-inline");
                                displayinput.removeAttr("datepicker-calendar");
                                displayinput.removeAttr("datepicker-language");
                                displayinput.removeAttr("datepicker-dateformat");
                                displayinput.removeAttr("es-datepicker");
                                displayinput.addClass("form-control");
                                iElem.after(displayinput);
                                $compile(displayinput)(scope);
                            }

                            $(displayinput).vestadp({
                                calendar: calendar,
                                language: language,
                                dateFormat: dateFormat
                            });

                            scope.$watch(iAttrs.ngModel, function (value) {
                                var x = /(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-5]\d\.\d+)|(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-5]\d)|(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d)/;
                                var z = x.test(value);
                                var jsDate = new Date(value);
                                //console.log('$watch', value+'_'+z);
                                if (z) {
                                    var cal = $(displayinput).vestadp('getCalendar');
                                    var jd = jsDate.getJulian();
                                    $(displayinput).vestadp('getDate');
                                    cal.setJulianDay(jd);
                                    if (!isInline)
                                        $(displayinput).val(cal.toString(dateFormat));
                                }
                            });

                            $(displayinput).on("dateChanged",
                                function (event, elm, dateStr, calendar) {
                                    //console.log('dateChanged');

                                    if (dateStr) {
                                        var jsDate = $(elm)
                                            .vestadp('getDate', false, dateFormat);
                                        if (jsDate) {
                                            var isoDate = jsDate.toISOString();
                                            var jd = jsDate.getJulian();
                                            ngModel.$setViewValue(isoDate);
                                        }
                                    }
                                    else {
                                        ngModel.$setViewValue(null);
                                    }
                                });
                            $(displayinput).on("change", function (value) {
                                console.log('change');
                                var isValid = true, year, month, day;
                                value = this.value.toString();
                                year = parseInt(value.substr(0, 4));
                                month = parseInt(value.substr(4, 2));
                                day = parseInt(value.substr(6, 2));

                                var oldDate = $(displayinput).vestadp('getDate');
                                if (year == oldDate.getFullYear() & month == oldDate.getMonth() & day == oldDate.getDay())
                                    return;

                                if (value)
                                    isValid = false;
                                if (value.length != 10)
                                    isValid = false;
                                var cal = $(displayinput).vestadp('getCalendar');
                                try {
                                    $(displayinput).vestadp('setDate', { year: year, month: month, day: day }, true);
                                } catch (e) {
                                    $(displayinput).vestadp('setDate', { year: oldDate.getFullYear(), month: oldDate.getMonth(), day: oldDate.getDay() }, false);
                                    console.log('error');
                                }
                            });
                            if (!isInline) {

                                displayinput.wrap("<div class='input-group' >");

                                var span = angular.element('<span class="input-group-btn" ng-disabled="' + iAttrs.ngDisabled + '"></span>');
                                var open = angular.element("<button class='btn btn-primary' type='button' ng-disabled='" + iAttrs.ngDisabled + "'><i class='fa fa-calendar'></i></button>");
                                span.append(open);
                                var clear = angular.element("<button class='btn btn-danger' type='button' ng-disabled='" + iAttrs.ngDisabled + "'><i class='fa fa-close'></i></button>");
                                span.append(clear);
                                displayinput.after(span);
                                $compile(span)(scope);
                                open.on("click", function (e) {
                                    scope.$apply(function () {
                                        $(displayinput).vestadp('showPicker');

                                    });
                                });
                                clear.on("click", function (e) {
                                    scope.$apply(function () {
                                        ngModel.$setViewValue(null);
                                        e.stopPropagation();
                                    });
                                });
                            }
                        }
                    }
                }
            }
        }]);
})();

