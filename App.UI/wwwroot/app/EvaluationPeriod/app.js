var basePath = '/app/EvaluationPeriod';

var app = angular.module('app', ["base"]);

app.config(['$translateProvider', '$stateProvider', "$locationProvider", "$urlRouterProvider", "cultureProvider",
    function ($translateProvider, $stateProvider, $locationProvider, $urlRouterProvider, cultureProvider) {
        $translateProvider.useStaticFilesLoader({
            prefix: basePath + '/i18n/dictionary_',
            suffix: '.json'
        });
        var culture = cultureProvider.getCultureName();
        $translateProvider.useSanitizeValueStrategy('sanitizeParameters');
        $translateProvider.preferredLanguage(culture);
        $translateProvider.fallbackLanguage(culture);
        var viewPath = basePath + "/views";
        $stateProvider
            .state('EvaluationPeriod', {
                abstract: true,
                url: '/List',
                template: '<ui-view/>',
                controller: "RootController",
                controllerAs: 'root'
            })
            .state('EvaluationPeriod.List', {
                url: '',
                templateUrl: function (params) {
                    return viewPath + '/Home.html';
                },
                controllerAs: 'home',
                controller: "HomeController"
            })
            .state('EvaluationPeriod.Create', {
                url: '/Create',
                templateUrl: function (params) {
                    return viewPath + '/Create.html';
                },
                controllerAs: 'create',
                controller: 'CreateController'
            })
            .state('EvaluationPeriod.Details', {
                url: '/Details/:id',
                templateUrl: function (params) {
                    return viewPath + '/Details.html';
                },
                controllerAs: 'details',
                controller: 'DetailsController'
            })
            .state('EvaluationPeriod.Edit', {
                url: '/Edit/:id',
                templateUrl: function (params) {
                    return viewPath + '/Edit.html';
                },
                controllerAs: 'edit',
                controller: 'EditController'
            })
            .state('EvaluationPeriod.Delete', {
                url: '/Delete/:id',
                templateUrl: function (params) {
                    return viewPath + '/Delete.html';
                },
                controllerAs: 'delete',
                controller: "DeleteController"
            })

            .state('EvaluationPeriod.DetailRecord', {
                url: '/DetailRecord/:id',
                templateUrl: function (params) {
                    return viewPath + '/DetailRecord.html';
                },
                controllerAs: 'detailrecord',
                controller: 'DetailRecordController'
            });
        $urlRouterProvider.otherwise('List');
    }]);

app.controller("RootController",
    ["$scope", "$state", "esDatasource", "$http", "$sce", "$uibModal",
        function ($scope, $state, esDatasource, $http, $sce, $uibModal) {
            var vm = this;
            vm.success = function () {
                $state.go("EvaluationPeriod.List");
            };
            vm.error = function () {
            };

            vm.ds = new esDatasource({
                url: '/EvaluationPeriod/GetAllPaged',
                method: 'GET',
                params: {
                    pageIndex: 0,
                    pageSize: 5
                }
            });
            vm.searchMode = 'advanced';
            vm.doAdvancedSearch = function () {
                vm.searchMode = 'advanced';
                vm.ds.setUrl('/EvaluationPeriod/GetAllPaged');
                vm.ds.refresh();
            };
            vm.changeSearchMode = function (searchMode) {
                console.log(searchMode);
                vm.searchMode = searchMode;
                vm.ds.params = {
                    pageIndex: 0,
                    pageSize: 5,
                    sortingOptions: []
                };
                switch (searchMode) {
                    case 'advanced':
                        vm.doAdvancedSearch();
                        break;
                }
            };
        }]);

app.controller("HomeController",
    ["esDatasource", function (esDatasource) {
    }]);

var show = function (data) {
    var a = data;
    vm.test = vm.BoxesDs.$data;

};

app.controller("DetailRecordController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EvaluationProject/GetAllPaged',
                method: 'GET',
                params: {
                    pageIndex: 0,
                    pageSize: 5,
                    PeriodId: $stateParams.id,
                    afterResponse: show
                }
            });
            // vm.ds.refresh();
        }]);


app.controller("DetailsController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EvaluationPeriod/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();
        }]);


app.controller("CreateController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);

app.controller("EditController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EvaluationPeriod/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();
        }]);

app.controller("DeleteController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EvaluationPeriod/GetById/' + $stateParams.id,
                method: 'Get'
            });
            vm.ds.refresh();
        }]);

app.controller("SearchController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);

