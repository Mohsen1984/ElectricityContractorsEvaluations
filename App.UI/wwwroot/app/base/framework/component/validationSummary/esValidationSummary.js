var app = angular.module('esValidationsummary', []);

app.directive('esValidationsummary', function ($http, $parse) {
    return {
        restrict: 'A', //E = element, A = attribute, C = class, M = comment         
        priority: 10000,
        scope: {
            ////@ reads the attribute value, = provides two-way binding, & works with functions
            errors: '=esNgValidationsummary'
        },
        template: '<div class="text-danger" >' +
            '<div ng-repeat="item in errors">' +
            '<i class="fa fa-exclamation-triangle" style="margin-right:20px;color:red;"></i><b style="margin-right:5px;color:red;">{{item}}</b>' +
            '</div>' +
            '</div>',
        controller: function ($scope) {

        },
        link: function (scope, element, attrs) {
            scope.$parent.$watch(attrs.esNgValidationsummary, function (value) {
                if (value != undefined) {
                    if (value.length == 0) {
                        element.hide();
                    }
                    else {
                        element.show();
                    }
                }
                else {
                    element.hide();
                }
            }, true);
        }
    }
});