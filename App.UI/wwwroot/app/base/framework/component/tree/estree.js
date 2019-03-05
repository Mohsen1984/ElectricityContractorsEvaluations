var templatePath = window.frameworkPath + "/component/tree/classicTemplate.html";
var treeModule = angular.module('esTree', ['ui.tree']);
//treeModule.requires.push('ui.tree');
treeModule.config(function (treeConfig) {
    treeConfig.defaultCollapsed = true; // collapse nodes by default
});
treeModule.directive('esTree', ["$compile", "esDatasource", function ($compile, esDatasource) {
    return {
        restrict: "EA",
        scope: {
            datasource: '=treeDatasource',
            dataMode: '@?treeData',
            lazyload: '@?treeLazy',
            autoLoad: '@?autoLoad',
            nodeTemplate: '@?treeNodeTemplate'
        },
        template: '<div ui-tree data-drag-enabled="false"><i class="fa fa-refresh fa-spin" ng-if="rootLoadState == 2"></i><ul ui-tree-nodes ng-model="treeData" class="nav nav-list">' +
            '<li ng-repeat=\"node in treeData\" ui-tree-node ng-include="\'' + templatePath+'\'\" ></li >' +
            '</ul></div>',
        controller: function ($scope) {
            $scope.parent = $scope.$parent;
            $scope.templatePath = templatePath;
        },
        compile: function (element, attributes) {
            return {
                pre: function (scope, element, attrs, controller) {

                },
                post: function (scope, element, attributes, controller) {
                    if (scope.autoLoad == 'undefined') {
                        scope.autoLoad = true;
                    }
                    if (scope.dataMode == "remote" || scope.dataMode == "lazy") {
                        scope.datasource.BeforeRequest = function (data) {
                            scope.rootLoadState = 2;
                        }
                        scope.datasource.afterResponse = function (data) {
                            scope.rootLoadState = 1;
                            scope.treeData = data;
                        }
                        scope.datasource.afterError = function (data) {
                            scope.rootLoadState = 1;
                        }
                        if (scope.autoLoad)
                            scope.datasource.refresh();
                    }
                    else {
                        scope.treeData = scope.datasource.$data;
                    }

                    scope.toggleChildren = function (sc) {
                        if (scope.dataMode == "lazy") {
                            if (sc.$modelValue.hasChild == true && !sc.$modelValue.loadState == 1) {
                                scope.datasource.setParams(sc.$modelValue);
                                scope.datasource.afterResponse = function (data) {
                                    if (!data.length)
                                        sc.$modelValue.hasChild = false;
                                    else {
                                        sc.$modelValue.loadState = 1
                                        sc.$modelValue.nodes = data;
                                        sc.toggle();
                                    }
                                }
                                sc.$modelValue.loadState = 2;
                                scope.datasource.refresh();
                            }
                            else {
                                sc.toggle();
                            }
                        }
                        else
                            sc.toggle();
                    }
                    scope.$watch("datasource.$data", function (value) {
                        if (!value)
                            scope.treeData = value;
                    }, true);
                    element.addClass("es-tree");
                }
            }
        },
        link: function (scope, element, attrs) {

        }
    }
}]);
