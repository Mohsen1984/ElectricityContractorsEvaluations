var basePath = '/app/EVFormTemplate';

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
            .state('EVFormTemplate', {
                abstract: true,
                url: '/List',
                template: '<ui-view/>',
                controller: "RootController",
                controllerAs: 'root'
            })
            .state('EVFormTemplate.List', {
                url: '',
                templateUrl: function (params) {
                    return viewPath + '/Home.html';
                },
                controllerAs: 'home',
                controller: "HomeController"
            })
            .state('EVFormTemplate.Create', {
                url: '/Create',
                templateUrl: function (params) {
                    return viewPath + '/Create.html';
                },
                controllerAs: 'create',
                controller: 'CreateController'
            })
            .state('EVFormTemplate.Details', {
                url: '/Details/:id',
                templateUrl: function (params) {
                    return viewPath + '/Details.html';
                },
                controllerAs: 'details',
                controller: 'DetailsController'
            })
            .state('EVFormTemplate.Edit', {
                url: '/Edit/:id',
                templateUrl: function (params) {
                    return viewPath + '/Edit.html';
                },
                controllerAs: 'edit',
                controller: 'EditController'
            })
            .state('EVFormTemplate.Delete', {
                url: '/Delete/:id',
                templateUrl: function (params) {
                    return viewPath + '/Delete.html';
                },
                controllerAs: 'delete',
                controller: "DeleteController"
            })
////////////////////////////////////////////////////////////////////
            .state('EVFormTemplate.ListD', {
                url: '/ListD/:id',
                templateUrl: function (params) {
                    return viewPath + '/Home_EVFormTemplateItem.html';
                },
                controllerAs: 'homed',
                controller: 'HomeDController'
            })

            .state('EVFormTemplate.CreateD', {
                url: '/EVFormTemplateItem/Create',
            templateUrl: function (params) {
                return viewPath + '/Create_EVFormTemplateItem.html';
            },
            controllerAs: 'created',
                controller: 'CreateDController'
            });
//////////////////////////////////////////////////////////////
        $urlRouterProvider.otherwise('List');
    }]);

app.controller("RootController",
    ["$scope", "$state", "esDatasource", "$http", "$sce", "$uibModal",
        function ($scope, $state, esDatasource, $http, $sce, $uibModal) {
            var vm = this;
            vm.success = function () {
                $state.go("EVFormTemplate.List");
            };
            vm.error = function () {
            };

            vm.ds = new esDatasource({
                url: '/EVFormTemplate/GetAllPaged',
                method: 'GET',
                params: {
                    pageIndex: 0,
                    pageSize: 5
                }
            });
            vm.searchMode = 'advanced';
            vm.doAdvancedSearch = function () {
                vm.searchMode = 'advanced';
                vm.ds.setUrl('/EVFormTemplate/GetAllPaged');
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

////////////////////////////////////////////////////
var show = function (data) {
    var a = data;
    vm.test = vm.BoxesDs.$data;

};



app.controller("HomeDController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EVFormTemplateItem/GetAllPaged',
                method: 'GET',
                params: {
                    pageIndex: 0,
                    pageSize: 5,
                    EVFormTemplateId: $stateParams.id,
                    afterResponse: show
                }
            });
            // vm.ds.refresh();
        }]);

app.controller("CreateDController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);

/////////////////////////////////////////////////////////////

app.controller("HomeController",
    ["esDatasource", function (esDatasource) {
    }]);







app.controller("DetailsController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EVFormTemplate/GetById/' + $stateParams.id,
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
                url: '/EVFormTemplate/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();
        }]);

app.controller("DeleteController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/EVFormTemplate/GetById/' + $stateParams.id,
                method: 'Get'
            });
            vm.ds.refresh();
        }]);

app.controller("SearchController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);

