var app = angular.module('esPost', []);

app.directive('esNgPost', function ($http, $compile, $parse, $window) {
    return {
        replace: false,
        restrict: 'A',
        compile: function (element, attributes) {
            return {
                pre: function (scope, element, attrs, controller) {
                },
                post: function (scope, element, attrs, controller) {
                    var object = scope.$eval(attrs.postObject);
                    scope.$watch(attrs.postObject, function (value) {
                        object = value;
                    });

                    var validationSummary = $parse(attrs.esNgPostValidationObject);

                    var disable = scope.$eval(attrs.esNgPostDisableObject);
                    scope.$watch(attrs.esNgPostDisableObject, function (value) {
                        disable = value;
                    });

                    var url = attrs.esNgPostUrl;

                    var start = $parse(attrs.esNgPostStart);
                    var success = $parse(attrs.esNgPostSuccess);
                    var error = $parse(attrs.esNgPostError);

                    element.on('click', function () {
                        if (attrs.esNgPostFormName && scope[attrs.esNgPostFormName]) {
                            var pageHasError = false;
                            for (var item in scope[attrs.esNgPostFormName]) {
                                if (item[0] != '$') {
                                    scope[attrs.esNgPostFormName][item].$dirty = true;
                                    var a = scope[attrs.esNgPostFormName][item].$error;
                                    if (!jQuery.isEmptyObject(a))
                                        pageHasError = true;
                                }
                            }
                            if (pageHasError) {
                                var ret = ['لطفا خطاهای داخل صفحه را رفع نمایید.'];
                                validationSummary.assign(scope, ret);
                            }
                            scope.$apply();
                        }
                        if ($parse(attrs.esNgPostDisableObject)(scope))
                            return;
                        object = scope.$eval(attrs.postObject);
                        start(scope, { data: object, url: url });
                        if (!pageHasError) {
                            element.addClass("btn-loading");
                           
                                $http.post(url, object)
                                    .then(
                                        function (data, status, headers, config) {
                                            element.removeClass("btn-loading");
                                            success(scope, { data: data, status: status, headers: headers, config: config });
                                        },
                                        function (data, status, header, config) {
                                            element.removeClass("btn-loading");
                                            var ret = [];
                                            for (var prop in data.data) {
                                                ret = ret.concat(data.data[prop]);
                                            }
                                            validationSummary.assign(scope, ret);
                                            error(scope, { data: data, status: status, headers: header, config: config });
                                        });
                            
                        }
                    });
                }
            };
        }
    };
});