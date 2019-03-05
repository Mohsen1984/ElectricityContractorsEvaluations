var app = angular.module('esSelect', ['ui.select', "ngSanitize"]);
app.directive('esDropdown', ["$http", "$compile", "$parse", function ($http, $compile, $parse) {
    return {
        restrict: 'E',
        require: 'ngModel',
        scope: {
            ngModel: '=',
            datasource: '=',
            dropdownChange: '&',
            onSelectItem: '&',
            onRemoveItem: '&',
            displayColumnNames: '=',
            selectedDisplayName: '=',
            valueMember: '@',
            ngDisabled: '='
        },
        controller: ['$scope', function ($scope) {
        }],
        link: function (scope, element, attr, ngModel) {

            scope.innerModel = scope;
            scope.innerModel.loading = true;

            var exp = $parse(attr.datasource);
            var arrayList = $parse(attr.datasource)(scope.$parent);
            if (jQuery.isArray(exp(scope))) {
                scope.innerModel.loading = false;
                scope.data = scope.datasource;
            }
            else if (jQuery.isArray(arrayList)) {
                scope.data = arrayList;
                scope.innerModel.loading = false;
            }
            else {
                scope.$watch("datasource.$data", function () {
                    if (scope.datasource) {
                        scope.data = scope.datasource.$data;
                        if (scope.data) {
                            scope.innerModel.loading = false;
                        }
                    }
                }, true);
            }

            if (!attr.theme)
                attr.theme = '';
            if (!attr.placeholder)
                attr.placeholder = '';
            if (!attr.ngDisabled)
                attr.ngDisabled = '';
            if (!attr.name)
                attr.name = '';

            var onSelect = '';
            var onRemove = '';
            if (attr.dropdownChange) {
                scope.innerModel.func1 = function () {
                    scope.innerModel.dropdownChange();
                };
                scope.innerModel.removeItem = function ($item, $model) {
                    scope.innerModel.onRemoveItem({ item: $item, model: $model });
                    scope.innerModel.func1();
                };
                scope.innerModel.selectItem = function ($item, $model) {
                    scope.innerModel.onSelectItem({ item: $item, model: $model });
                    scope.innerModel.func1();
                };
                onSelect = 'on-select="innerModel.selectItem($item, $model)"';
                onRemove = 'on-remove="innerModel.removeItem($item, $model)"';
            }

            var trackBy = '';
            if (!scope.valueMember) {
                trackBy = "$index";
                scope.valueMember = "item";
            }
            else {
                scope.valueMember = "item." + scope.valueMember;
                trackBy = scope.valueMember;
            }
            var spin = angular.element('<span ng-disabled="true" ng-show="innerModel.loading" class="form-control spinner" style="display:inline-block"> در حال بارگذاری <div class="bounce1"></div> <div class="bounce2"></div> <div class="bounce3"></div></span>');

            var UISelect;
            var UISelectMatchString = '<ui-select-match placeholder="' + attr.placeholder + '" >';
            if (element.attr('multiple')) {
                UISelect = angular.element('<ui-select ng-show="!innerModel.loading" theme="' + attr.theme + '" ng-model="innerModel.ngModel" ng-disabled="innerModel.ngDisabled"' + onSelect + ' ' + onRemove + ' multiple></ui-select >');
                angular.forEach(scope.selectedDisplayName, function (item, index) {
                    UISelectMatchString += '{{$item.' + item + '}}&nbsp;';
                });
            }
            else {
                UISelect = angular.element('<ui-select ng-show="!innerModel.loading" theme="' + attr.theme + '" ng-model="innerModel.ngModel" ng-disabled="innerModel.ngDisabled" ' + onSelect + ' ' + onRemove + '></ui-select >');
                angular.forEach(scope.selectedDisplayName, function (item) {
                    UISelectMatchString += '{{$select.selected.' + item + '}}&nbsp;';
                });
            }
            UISelectMatchString += '</ui-select-match>';
            var UISelectMatch = angular.element(UISelectMatchString);
            if (!element.attr('multiple')) {
                let str = "";
                angular.forEach(scope.selectedDisplayName, function (item) {
                    str += '{{$select.selected.' + item + '}} ';
                });
                if (str.trim()) {
                    UISelectMatch.attr("title", str);
                }
            }
            //var UISelectChoicesString = '<ui-select-choices repeat="' + scope.valueMember + ' as item in data| filter: $select.search track by ' + trackBy + ' ">';
            var UISelectChoicesString = '<ui-select-choices repeat="' + scope.valueMember + ' as item in data| filter: $select.search">';
            angular.forEach(scope.displayColumnNames, function (item) {
                UISelectChoicesString += '<span ng-bind-html="item.' + item + ' | highlight: $select.search"></span>&nbsp;';
            });
            UISelectChoicesString += '</ui-select-choices>';
            var UISelectChoices = angular.element(UISelectChoicesString);

            UISelect.append(UISelectMatch);
            UISelect.append(UISelectChoices);

            UISelect.addClass(attr.class);
            UISelect.attr('style', attr.style);

            if (!element.attr('required') && !element.attr('multiple')) {
                var inputGroup = angular.element("<div class='input-group' ng-show='!innerModel.loading'></div>");
                inputGroup.append(UISelect);
                var inputGroupAddon = angular.element('<div class="input-group-btn"><button ng-disabled="innerModel.ngDisabled" class="btn btn-danger"><i class="fa fa-close"></i></button></div>');
                inputGroup.append(inputGroupAddon);
                element.after(inputGroup);
                inputGroupAddon.on("click", function () {
                    scope.$apply(function () {
                        scope.ngModel = '';
                    });
                    scope.innerModel.dropdownChange();
                });
                $compile(inputGroup)(scope);
            }
            else {
                if (element.attr('required')) {
                    scope.$watch("ngModel", function (value) {
                        //console.log(ngModel.$dirty);
                        if (value) {
                            if (jQuery.isArray(value) && (value.length == 0)) {
                                ngModel.$setValidity('required', false);
                            }
                            else {
                                ngModel.$setValidity('required', true);
                                ngModel.$dirty = true;
                            }
                        }
                        //else
                        //    ngModel.$setValidity('required', false);
                    }, true);
                }
                element.after(UISelect);
                $compile(UISelect)(scope);
            }

            element.after(spin);
            $compile(spin)(scope);
            element.css('display', 'none');

        }
    };
}]);
app.directive('esTagInput', ["$compile", function ($compile) {
    return {
        restrict: 'E',
        require: 'ngModel',
        replace: true,
        scope: {
            ngModel: '=',
            ngDisabled: '='
        },
        controller: ['$scope', function ($scope) {
            //console.log($scope.ngModel);
            //console.log($scope.dataSource.$data);
        }],
        link: function (scope, element, attr, ngModel) {

            scope.innerModel = scope;

            if (!attr.theme)
                attr.theme = '';
            if (!attr.limit)
                attr.limit = 10;
            if (!attr.placeholder)
                attr.placeholder = '';
            if (!attr.ngDisabled)
                attr.ngDisabled = '';
            if (!attr.name)
                attr.name = '';
            if (element.attr('required'))
                required = 'required';
            else
                required = '';

            var UISelect = angular.element('<ui-select name="' + attr.name + '" theme="' + attr.theme + '" limit="' + attr.limit + '" ' + required + ' ng-model="innerModel.ngModel" ng-disabled="innerModel.ngDisabled"  multiple tagging></ui-select >');

            var UISelectMatch = angular.element('<ui-select-match placeholder="' + attr.placeholder + '" >' +
                '{{$item}}</ui-select-match>');

            var UISelectChoices = angular.element('<ui-select-choices repeat="item in data" style="display:none;"></ui-select-choices>');

            UISelect.append(UISelectMatch);
            UISelect.append(UISelectChoices);

            UISelect.addClass(attr.class);
            UISelect.attr('style', attr.style);
            element.after(UISelect);
            $compile(UISelect)(scope);
            element.css('display', 'none');

        }
    }
}]);
app.directive('esTaggingDropdown', ["$http", "$compile", "$parse", function ($http, $compile, $parse) {
    return {
        restrict: 'E',
        require: 'ngModel',
        replace: true,
        scope: {
            ngModel: '=',
            datasource: '=',
            dropdownChange: '&',
            ngDisabled: '=',
            valueMember: '@',
            valueMembers: '@'
        },
        controller: ['$scope', function ($scope) {
            //console.log($scope.ngModel);
            //console.log($scope.dataSource.$data);

        }],
        link: function (scope, element, attr, ngModel) {

            var exp = $parse(attr.datasource);
            if (jQuery.isArray(exp(scope))) {
                scope.data = scope.datasource;
                if (attr.valueMember) {

                    function getFields(input, field) {
                        return input.map(function (o) {
                            return o[field];
                        });
                    };

                    var input = scope.datasource;
                    var field = scope.valueMember;

                    if (!jQuery.isArray($parse(field)()))
                        scope.data = getFields(input, field);
                    else
                        console.log("develope error: value-member must be string!");

                }
                else if (attr.valueMembers) {

                    function getFields(input, fields) {
                        return input.map(function (o) {
                            var result = "";
                            angular.forEach(fields, function (item) {
                                result = result + " " + o[item];
                            });
                            return result;
                        });
                    };

                    var input = scope.datasource;
                    var fields = $parse(scope.valueMembers);
                    if (jQuery.isArray(fields()))
                        scope.data = getFields(input, fields());
                    else
                        console.log("develope error: value-members must be array!");
                }

            }
            else {
                scope.$watch("datasource.$data", function () {
                    scope.data = scope.datasource.$data;
                    if (scope.data) {
                        if (attr.valueMember) {

                            function getFields(input, field) {
                                return input.map(function (o) {
                                    return o[field];
                                });
                            };

                            var input = scope.datasource.$data;
                            var field = scope.valueMember;

                            if (!jQuery.isArray($parse(field)()))
                                scope.data = getFields(input, field);
                            else
                                console.log("value-member must be string!");


                        }
                        else if (attr.valueMembers) {

                            function getFields(input, fields) {
                                return input.map(function (o) {
                                    var result = "";
                                    angular.forEach(fields, function (item) {
                                        result = result + " " + o[item];
                                    });
                                    return result;
                                });
                            };

                            var input = scope.data;
                            var fields = $parse(scope.valueMembers);
                            if (jQuery.isArray(fields())) {
                                scope.data = getFields(input, fields());
                            }
                            else
                                console.log("value-members must be array!");
                        }
                    }
                }, true);
            }

            scope.innerModel = scope;

            if (!attr.theme)
                attr.theme = '';
            if (!attr.placeholder)
                attr.placeholder = '';
            if (!attr.limit)
                attr.limit = '';
            if (!attr.name)
                attr.name = '';
            if (element.attr('required'))
                required = 'required';
            else
                required = '';

            var onSelect = '';
            var onRemove = '';
            if (attr.dropdownChange) {
                scope.innerModel.func1 = function () {
                    scope.innerModel.dropdownChange();
                }
                onSelect = 'on-select="innerModel.func1()"';
                onRemove = 'on-remove="innerModel.func1()"';
            }

            var UISelect = angular.element('<ui-select name="' + attr.name + '" theme="' + attr.theme + '" limit="' + attr.limit + '" ng-model="innerModel.ngModel" ng-disabled="innerModel.ngDisabled" ' + onSelect + ' ' + onRemove + ' ' + required + ' multiple tagging tagging-label=""></ui-select >');

            var UISelectMatch = angular.element('<ui-select-match placeholder="' + attr.placeholder + '" >' +
                '{{$item}}</ui-select-match>');

            var UISelectChoices = angular.element('<ui-select-choices repeat="item as item in data| filter: $select.search track by $index ">' +
                '<span ng-bind-html="item | highlight: $select.search"></span></ui-select-choices>');

            UISelect.append(UISelectMatch);
            UISelect.append(UISelectChoices);

            UISelect.addClass(attr.class);
            UISelect.attr('style', attr.style);
            element.after(UISelect);
            $compile(UISelect)(scope);
            element.css('display', 'none');

        }
    };
}]);