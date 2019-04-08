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
            .state('ServiceTree.createProjectMember', {
                url: '/CreateProjectMember',
                templateUrl: function (params) {
                    return viewPath +'/CreateProjectMember.html';
                },
                controllerAs: 'createProjectMember',
                controller: 'CreateProjectMemberController'
            })
            .state('ServiceTree.DetailsProjectMember', {
                url: '/DetailsProjectMember/:id',
                templateUrl: function (params) {
                    return viewPath +'/DetailsProjectMember.html';
                },
                controllerAs: 'detailsProjectMember',
                controller: 'DetailsProjectMemberController'
            })
            .state('ServiceTree.EditProjectMember', {
                url: '/EditProjectMember/:id',
                templateUrl: function (params) {
                    return viewPath +'/EditProjectMember.html';
                },
                controllerAs: 'editprojectmember',
                controller: 'EditProjectMemberController'
            })
            .state('ServiceTree.DeleteProjectMember', {
                url: '/DeleteProjectMember/:id',
                templateUrl: function (params) {
                    return viewPath +'/DeleteProjectMember.html';
                },
                controllerAs: 'deleteprojectmember',
                controller: "DeleteProjectMemberController"
            });
        $urlRouterProvider.otherwise('List');
    }]);

app.controller("RootController",
    ["$rootScope","$scope", "$state", "esDatasource", "$http", "$sce", "$uibModal",
        function ($rootScope,$scope, $state, esDatasource, $http, $sce, $uibModal) {
            var vm = this;

           
            
            vm.success = function () {
                $state.go("ServiceTree.List");
            };
            vm.success2 = function () {
                $state.go("ServiceTree.List", { "projectInfoId": $rootScope.GprojectInfoId, "projectId": $rootScope.GprojectId, "serviceTreeTempId": $rootScope.GserviceTreeTempId });

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
        $rootScope.GprojectId = $stateParams.projectId
        $rootScope.GserviceTreeTempId = $stateParams.serviceTreeTempId;
       //$rootScope.GserviceId=0



        vm.dsServiceTree = new esDatasource({
            url: '/ServiceTree/EntityServiceTreeNodes',
            method: 'GET',
            params: {
                ProjectInfoRef: $rootScope.GprojectInfoId,

            }
        });
        vm.dsServiceTree.refresh();


        vm.dsProjectMember = new esDatasource({

            url: '/ProjectMember/GetAllPaged',
            method: 'GET',
            params: {
                ServiceTreeRef: $rootScope.GserviceId,
                pageIndex: 0,
                pageSize: 5,
            }

        });
        vm.dsProjectMember.refresh();

        vm.ServiceTreeClick = function (node) {

            $rootScope.GserviceId = node.id;

          //  $rootScope.GserviceTreeTempId = 0;

            //vm.nodeId = node.id;
            vm.dsProjectMember.params = {

                url: '/ProjectMember/GetAllPaged',
                method: 'GET',
                ServiceTreeRef: $rootScope.GserviceId,
                pageIndex: 0,
                pageSize: 5,

            };
            vm.dsProjectMember.refresh();


        };

    }]);

app.controller("DetailsProjectMemberController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectMember/GetById' ,
                method: 'GET',
                params: {
                    id: $stateParams.id

                }
         
            });
            vm.ds.refresh();

        }]);


app.controller("CreateProjectMemberController",
    ["$state","$rootScope","$stateParams", "esDatasource",
        function ($state,$rootScope,$stateParams, esDatasource) {
            var vm = this;

            vm.entity = new esDatasource({
                url: '/ProjectMember/Initialize?projectInfoRef=' + $rootScope.GprojectInfoId + '&ServiceTreeRef=' + $rootScope.GserviceId,
                method: 'GET'
            });
            vm.entity.refresh();
       
        }]);

app.controller("EditProjectMemberController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectMember/GetById/' + $stateParams.id,
                method:'GET'
            });
            vm.ds.refresh();
          
        }]);

app.controller("DeleteProjectMemberController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectMember/GetById/' + $stateParams.id,
                method:'Get'
            });
            vm.ds.refresh();
        }]);

app.controller("SearchController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);

