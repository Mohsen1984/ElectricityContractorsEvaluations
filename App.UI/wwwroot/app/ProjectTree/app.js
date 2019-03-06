var basePath = '/app/ProjectTree';

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
            .state('ProjectTree', {
                abstract: true,
                url: '/List',
                template: '<ui-view/>',
                controller: "RootController",
                controllerAs: 'root'
            })
            .state('ProjectTree.List', {
                url: '',
                templateUrl: function (params) {
                    return viewPath + '/Home.html';
                },
                controllerAs: 'home',
                controller: "HomeController"
            })
            .state('ProjectTree.Create', {
                url: '/Create/:parentId/:level',
                templateUrl: function (params) {
                    return viewPath + '/Create.html';
                },
                controllerAs: 'create',
                controller: 'CreateController'
            })

            .state('ProjectTree.Details', {
                url: '/Details/:id',
                templateUrl: function (params) {
                    return viewPath + '/Details.html';
                },
                controllerAs: 'details',
                controller: 'DetailsController'
            })
            .state('ProjectTree.Edit', {
                url: '/Edit/:id',
                templateUrl: function (params) {
                    return viewPath + '/Edit.html';
                },
                controllerAs: 'edit',
                controller: 'EditController'
            })
            .state('ProjectTree.Delete', {
                url: '/Delete/:id/:levelCode',
                templateUrl: function (params) {
                    return viewPath + '/Delete.html';
                },
                controllerAs: 'delete',
                controller: "DeleteController"
            })

            .state('ProjectTree.Back', {
                url: '/BackFromServiceTree/:projectId/:serviceTreeTempId/:GprojectInfoId',
                controllerAs: 'back',
                controller: "BackController"
            })
            ///////////////////////ServiceTemplateTree
            .state('ProjectTree.ListService', {
                url: '/HomeServiceTemplateTree/:projectref',
                templateUrl: function (params) {
                    return viewPath + '/HomeServiceTemplateTree.html';
                },
                controllerAs: 'homeservice',
                controller: "HomeServiceController"
            })

            .state('ProjectTree.EditService', {
                url: '/Edit/:id/:projecttreeref',
                templateUrl: function (params) {
                    return viewPath + '/EditServiceTemplateTree.html';
                },
                controllerAs: 'editservice',
                controller: 'EditServiceController'
            })

             .state('ProjectTree.CreateService', {
                 url: '/CreateServiceTemplateTree/:parentId/:level/:projecttreeref',
            templateUrl: function (params) {
                return viewPath + '/CreateServiceTemplateTree.html';
            },
                 controllerAs: 'createservice',
                 controller: "CreateServiceController"
            })

            //////////////////////////////////ProjectInfo
            .state('ProjectTree.CreateProjectInfo', {
                url: '/CreateProjectInfo',
            templateUrl: function (params) {
                return viewPath + '/CreateProjectInfo.html';
            },
            controllerAs: 'createprojectinfo',
                controller: "CreateProjectInfoController"
            })

            .state('ProjectTree.EditProjectInfo', {
            url: '/EditProjectInfo/:id',
            templateUrl: function (params) {
                return viewPath + '/EditProjectInfo.html';
            },
            controllerAs: 'editprojectinfo',
            controller: "EditProjectInfoController"
            })

            .state('ProjectTree.DeleteProjectInfo', {
                url: '/DeleteProjectInfo/:id',
            templateUrl: function (params) {
                return viewPath + '/DeleteProjectInfo.html';
            },
                controllerAs: 'deleteprojectinfo',
                controller: "DeleteProjectInfoController"
            })

            .state('ProjectTree.DetailsProjectInfo', {
                url: '/DetailsProjectInfo/:id',
            templateUrl: function (params) {
                return viewPath + '/DetailsProjectInfo.html';
            },
                controllerAs: 'detailsprojectinfo',
                controller: "DetailsProjectInfoController"
        });
        $urlRouterProvider.otherwise('List');
    }]);

app.controller("RootController",
    ["$scope","$rootScope", "$state", "esDatasource", "$http", "$sce", "$uibModal",
        function ($scope,$rootScope, $state, esDatasource, $http, $sce, $uibModal) {
            var vm = this;
            $rootScope.GprojectId = 0;
            $rootScope.GserviceTreeTempId = 0;

            vm.success = function () {
         
                    $state.go("ProjectTree.List");
          
            };
            vm.error = function () {
            };
            
            vm.ds = new esDatasource({
                url: '/ProjectTree/GetAllPaged',
                method:'GET',
                params: {
                    pageIndex: 0,
                    pageSize: 5
                }
            });

            vm.searchMode = 'advanced';
            vm.doAdvancedSearch = function () {
                vm.searchMode = 'advanced';
                vm.ds.setUrl('/ProjectTree/GetAllPaged');
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

app.controller("BackController",
    ["$state", "$scope", "$rootScope", "$stateParams", "esDatasource", function ($state, $scope, $rootScope, $stateParams, esDatasource) {

        var vm = this;

        $rootScope.GprojectId = $stateParams.projectId;
        $rootScope.GserviceTreeTempId = $stateParams.serviceTreeTempId;
   
        $state.go("ProjectTree.List");

      
    }]);
app.controller("HomeController",
    ["$state", "$scope", "$rootScope", "$stateParams", "esDatasource", function ($state, $scope, $rootScope, $stateParams, esDatasource) {

        var vm = this;
        vm.nodeId = 2;
        $scope.$parent.ProjectTreeRef = 6666;

        vm.dsService = new esDatasource({
            url: '/ServiceTemplateTree/GetsParentsService',
            method: 'GET',
            params: {
                ProjectTreeRef: $rootScope.GprojectId,
                pageIndex: 0,
                pageSize: 5
            }
        });
        vm.dsService.refresh();

        vm.dsProjectInfo = new esDatasource({
            url: '/ProjectInfo/GetAllPaged',
            method: 'GET',
            params: {
                ServiceTemplateTreeRef: $rootScope.GserviceTreeTempId,
                pageIndex: 0,
                pageSize: 5
            }
        });
        vm.dsProjectInfo.refresh();

        vm.ProjectTreeClick = function (node) {

            $rootScope.GprojectId = node.id;
            $rootScope.GserviceTreeTempId = 0;

            vm.nodeId = node.id;
            vm.dsService.params = {
                ProjectTreeRef: $rootScope.GprojectId,
                pageIndex: 0,
                pageSize: 5,

            };
            vm.dsService.refresh();
            /////
            vm.dsProjectInfo.params =
                {
                    ServiceTemplateTreeRef: $rootScope.GserviceTreeTempId,
                    pageIndex: 0,
                    pageSize: 5
                };
            vm.dsProjectInfo.refresh();

        };
        vm.GridServiceRowClick = function (selecteditem) {
            vm.dsProjectInfo.params =
                {
                    ServiceTemplateTreeRef: selecteditem.serviceTemplateTreeId,
                    pageIndex: 0,
                    pageSize: 5
                };
            vm.dsProjectInfo.refresh();

            $rootScope.GserviceTreeTempId = selecteditem.serviceTemplateTreeId;
        };


    }]);

app.controller("DetailsController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectTree/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();

        }]);

app.controller("CreateController",
    ["$scope", "$stateParams", "esDatasource",
        function ($scope, $stateParams, esDatasource) {
            var vm = this;
            vm.parentId = $stateParams.parentId;
            vm.loclevel = $stateParams.level;
            vm.a = "saallll";
            if ($stateParams.level == 1) {
                vm.levelCode2 = [
                    { value: "1", name: "پروژه" }
                ];
                if (vm.parentId != null) {
                    vm.entity = new esDatasource({
                        url: '/ProjectTree/CreateAsChild?id=' + vm.parentId + "&level=" + vm.loclevel,
                        method: 'GET'
                    });
                    vm.entity.refresh();
                }
            }
            else if ($stateParams.level == 2) {
                vm.levelCode2 =
                    [
                        { value: "2", name: "خدمات" },
                        { value: "3", name: "نقشها" }
                    ];
            }
        }]);

app.controller("EditController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectTree/GetById/' + $stateParams.id,
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
                url: '/ProjectTree/GetById/' + $stateParams.id,
                method: 'Get'
            });
            vm.ds.refresh();
        }]);

app.controller("SearchController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
        }]);


//////////////////////////////Service
app.controller("HomeServiceController",
    ["$rootScope", "$scope", "$stateParams", "esDatasource", function ($rootScope,$scope, $stateParams, esDatasource) {

        var vm = this;
        vm.a = 100;
        vm.nodeId = 2;
        vm.projectref = $stateParams.projectref
       

        vm.dsServiceTree = new esDatasource({
            url: '/ServiceTemplateTree/EntityServiceTemplateTreeNodes',
            method: 'GET',
            params: {
                ProjectRef: $rootScope.GprojectId,

            }
        });
        vm.dsServiceTree.refresh();

    }]);

app.controller("EditServiceController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;

            vm.projecttreeref = $stateParams.projecttreeref;
            vm.ds = new esDatasource({
                url: '/ServiceTemplateTree/GetById/' + $stateParams.id,
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

app.controller("CreateServiceController",
    ["$scope","$stateParams", "esDatasource",
        function ($scope,$stateParams, esDatasource) {
            var vm = this;
            // $stateParams.parentId;
            vm.loclevel = $stateParams.level;
            vm.parentId = $stateParams.parentId;
            vm.projecttreeref = $stateParams.projecttreeref;
            if ($stateParams.level == 2) {
                vm.levelCode2 =
                    [
                        { value: "2", name: "خدمات" },
                        { value: "3", name: "نقشها" }
                    ];
            }
            else if ($stateParams.level == 3) {
                vm.levelCode2 =
                    [

                        { value: "3", name: "نقشها" }
                    ];
            }
            if (vm.parentId != null) {
                vm.entity = new esDatasource({
                    url: '/ServiceTemplateTree/CreateAsChild?id=' + vm.parentId + "&level=" + vm.loclevel,
                    method: 'GET'
                });
                vm.entity.refresh();

            }

           



        }]);

////////////////////////////////////////

////////////////////////////////////ProjectInfo

app.controller("CreateProjectInfoController",
    ["$rootScope","$stateParams", "esDatasource",
        function ($rootScope,$stateParams, esDatasource) {
            var vm = this;
            //vm.entity.projectTreeRef = $rootScope.GprojectId;
        }]);


app.controller("EditProjectInfoController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectInfo/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();
        }]);

app.controller("DeleteProjectInfoController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectInfo/GetById/' + $stateParams.id,
                method: 'Get'
            });
            vm.ds.refresh();
        }]);

app.controller("DetailsProjectInfoController",
    ["$stateParams", "esDatasource",
        function ($stateParams, esDatasource) {
            var vm = this;
            vm.ds = new esDatasource({
                url: '/ProjectInfo/GetById/' + $stateParams.id,
                method: 'GET'
            });
            vm.ds.refresh();
        }]);


////////////////////////////////////



