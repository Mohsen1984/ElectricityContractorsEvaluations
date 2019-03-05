var basePath = '/app/TaskList';
//module
var app = angular.module("app", ['base']);
app.config(['$translateProvider', '$stateProvider', '$urlRouterProvider', 'cultureProvider', function ($translateProvider, $stateProvider, $urlRouterProvider, cultureProvider) {

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
        .state('TaskList', {
            url: '/',
            controller: "TaskList",
            controllerAs: 'TaskList',
            templateUrl: viewPath + '/TaskList.html'
        });
    $urlRouterProvider.otherwise('/');
}]);
//controller
app.controller("TaskList",
    function ($scope, $state, esDatasource, $http) {
        var vm = this;

        var disactiveBoxes = function (boxes) {
            angular.forEach(boxes, function (item) {
                item.isActive = false;
            });
        };

        var show = function (data) {
            var a = data;
            vm.Boxes = vm.BoxesDs.$data;
            if (vm.Boxes) {
                vm.boxClick(vm.Boxes[0]);
                disactiveBoxes(vm.Boxes);
                vm.Boxes[0].isActive = true;
            };
        };

        vm.BoxesDs = new esDatasource({
            url: '/TaskList/GetBoxes',
            afterResponse: show
        });
        vm.BoxesDs.refresh();

        vm.GridDs = new esDatasource({
            url: '/TaskList/GetAllPaged',
            method: 'GET',
            params: {
                pageIndex: 0,
                pageSize: 10
            }
        });
       // vm.GridDs.refresh();

        vm.boxClick = function (box) {
            disactiveBoxes(vm.Boxes);
            box.isActive = true;
            vm.GridDs.setUrl(box.dsUrl);
            vm.GridDs.refresh();
        };

        vm.doAdvancedSearch = function () {
            vm.GridDs.refresh();
        };

        vm.cancelAdvancedSearch = function (mode) {
            vm.searchMode = mode;
            vm.GridDs.params = {
                pageIndex: 0,
                pageSize: 10
            };
            vm.GridDs.refresh();
        }

        vm.action = function (item, model) {
            location.href = model;
        }

        vm.getPlaceholderTranslated = function (value) {
            return value;
        };

    });