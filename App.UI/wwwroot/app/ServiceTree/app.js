var basePath = '/app/ServiceTree';

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
            .state('ServiceTree', {
                abstract: true,
                url: '/List',
                template: '<ui-view/>',
                controller: "RootController",
                controllerAs: 'root'
            })
            .state('ServiceTree.List', {
                url: '/:projectInfoId/:projectId/:serviceTreeTempId',
                templateUrl: function (params) {
                    return viewPath +'/Home.html';
                },
                controllerAs: 'home',
                controller: "HomeController"
            })
            .state('ServiceTree.Create', {
                url: '/Create',
                templateUrl: function (params) {
                    return viewPath +'/Create.html';
                },
                controllerAs: 'create',
                controller: 'CreateController'
            })
            .state('ServiceTree.CreateAsChild', {
                url: '/CreateAsChild/:id/:level',
                templateUrl: function (params) {
                    return viewPath + '/CreateAsChild.html';
                },
                controllerAs: 'createaschild',
                controller: 'CreateAsChildController'
            })
            .state('ServiceTree.Details', {
                url: '/Details/:id',
                templateUrl: function (params) {
                    return viewPath +'/Details.html';
                },
                controllerAs: 'details',
                controller: 'DetailsController'
            })
            .state('ServiceTree.Edit', {
                url: '/Edit/:id',
                templateUrl: function (params) {
                    return viewPath +'/Edit.html';
                },
                controllerAs: 'edit',
                controller: 'EditController'
            })
            .state('ServiceTree.Delete', {
                url: '/Delete/:id/:levelCode',
                templateUrl: function (params) {
                    return viewPath +'/Delete.html';
                },
                controllerAs: 'delete',
                controller: "DeleteController"
            });
        $urlRouterProvider.otherwise('List');
    }]);

app.controller("RootController",
    ["$scope", "$state", "esDatasource", "$http", "$sce", "$uibModal",
        function ($scope, $state, esDatasource, $http, $sce, $uibModal) {
            var vm = this;

           
            
            vm.success = function () {
                $state.go("ServiceTree.List");
            };
            vm.error = function () {
            };

            vm.ds = new esDatasource({
                url: '/ServiceTree/GetAllPaged',
                method:'GET',
                params: {
                    pageIndex: 0,
                    pageSize: 5
                }
            });
            vm.searchMode = 'advanced';
            vm.doAdvancedSearch = function () {
                vm.searchMode = 'advanced';
                vm.ds.setUrl('/ServiceTree/GetAllPaged');
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
    ["$rootScope", "$stateParams", "esDatasource", function ($rootScope,$stateParams, esDatasource) {

        var vm = this;
        $rootScope.GprojectInfoId = $stateParams.projectInfoId;
        $rootScope.projectId = $stateParams.projectId
        $rootScope.serviceTreeTempId = $stateParams.serviceTreeTempId;
        vm.dsProjectInfo = new esDatasource({
            url: '/ServiceTree/EntityServiceTreeNodes',
            method: 'GET',
            params: {
                ProjectInfoRef: $rootScope.GprojectInfoId,

            }
        });
        vm.dsProjectInfo.refresh();
    }]);

app.controller("DetailsController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.entityTreeListDs = new esDatasource({
                url: '/ServiceTree/EntityServiceTreeNodes/' + $stateParams.id,
                method: 'GET',
                params: {
                    ProjectInfoRef: $stateParams.projectInfoRef,

                }
            });
            vm.ds.refresh();

        }]);


app.controller("CreateController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.levelCode2 = [
                { value: "2", name: "خدمات" }
            ];

      
       
        }]);


app.controller("CreateAsChildController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            var locid=$stateParams.id;
            var loclevel = $stateParams.level;
            
            vm.ds = new esDatasource({
                url: '/ServiceTree/CreateAsChild?id='+locid + "&level=" + loclevel,
                method: 'GET'
            });
            vm.ds.refresh();
                   
            vm.levelCode2 =
                [
                    { value: "1", name: "پروژه" },
                    { value: "2", name: "خدمات" },
                    { value: "3", name: "نقشها" }
                ];
        }]);

app.controller("EditController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ServiceTree/GetById/' + $stateParams.id,
                method:'GET'
            });
            vm.ds.refresh();
          
            vm.levelCode2 =
                [
                    { value: "1", name: "پروژه" },
                    { value: "2", name: "خدمات" },
                    { value: "3", name: "نقشها" }
                ];
        }]);


app.controller("EditController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ServiceTree/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();
            vm.vaziat = $stateParams.levelCode;
            vm.levelCode2 =
                [
                    { value: "1", name: "پروژه" },
                    { value: "2", name: "خدمات" },
                    { value: "3", name: "نقشها" }
                ];
        }]);

app.controller("DeleteController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ServiceTree/GetById/' + $stateParams.id,
                method:'Get'
            });
            vm.ds.refresh();
        }]);

app.controller("SearchController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);

