var basePath = '/app/DesignEvaluationForm';
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
        .state('designEvaluationForm', {
            url: '/',
            controller: "designEvaluationForm",
            controllerAs: 'designEvaluationForm',
            templateUrl: viewPath + '/designEvaluationForm.html'
        });
    $urlRouterProvider.otherwise('/');
}]);
//controller
app.controller("designEvaluationForm",
    function ($scope, $state, esDatasource, $http) {

        var vm = this;

        //vm.ItemsList = [
        //    { id: 1, text: 'menu1', hasChild: false },
        //    {
        //        id: 2, text: 'menu2',
        //        hasChild: true,
        //        subItems: [
        //            { id: 21, parentId: 2, text: 'subMenu1', hasChild: false },
        //            {
        //                id: 22,
        //                parentId: 2,
        //                text: 'subMenu2',
        //                hasChild: true,
        //                subItems: [
        //                    { id: 221, parentId: 22, text: 'subMenu1_1', hasChild: false },
        //                    { id: 222, parentId: 22, text: 'subMenu1_2', hasChild: false }
        //                ]
        //            }
        //        ]
        //    },
        //    { id: 3, text: 'menu3', hasChild: false },
        //    {
        //        id: 4, text: 'menu4',
        //        hasChild: true,
        //        subItems: [
        //            { id: 41, parentId: 4, text: 'subMenu1', hasChild: false },
        //            {
        //                id: 42, text: 'subMenu2',
        //                hasChild: true,
        //                parentId: 4,
        //                subItems: [
        //                    { id: 421, parentId: 42, text: 'subMenu1_1', hasChild: false },
        //                    { id: 422, parentId: 42, text: 'subMenu1_2', hasChild: false }
        //                ]
        //            },
        //            { id: 43, parentId: 4, text: 'subMenu3', hasChild: false }
        //        ]
        //    }
        //];

        vm.params = {};

        var setDetails = function () {
            vm.params.titleForm = detailsDs.$data.titleForm;
            vm.params.codeForm = detailsDs.$data.codeForm;
            vm.params.project = detailsDs.$data.project;
            vm.params.evaluator = detailsDs.$data.evaluator;
            vm.params.evaluated = detailsDs.$data.evaluated;
            vm.params.description = detailsDs.$data.description;
        };

        var detailsDs = new esDatasource({
            url: '/DesignEvaluationForm/GetDetails',
            afterResponse: setDetails
        });
        detailsDs.refresh();

        vm.TreeLoad = false;

        var setItems = function (data) {
            vm.ItemsList = ds.$data;
            vm.TreeLoad = true;
        };

        var ds = new esDatasource({
            url: '/DesignEvaluationForm/getTreeItems',
            afterResponse: setItems
        });
        ds.refresh();

        var level = -1;

        vm.New = function () {
            level = -1;
            $("#esModal").modal({ backdrop: "static" });
            $('#esModal').modal('show');
        };

        vm.Close = function () {
            $('#esModal').modal('hide');
        };

        var getCheckedItem = function (array) {

            var filteredItems = angular.copy(array.filter(function (item) {
                return item.checked === true;
            }));
            var TArray = angular.copy(filteredItems);
            angular.forEach(TArray, function (item, key) {
                level += 1;
                item.level = level;
                if (item.hasChild) {
                    item.subItems = [];
                    item.subItems = getCheckedItem(filteredItems[key].subItems);
                }
                level -= 1;
            });
            return TArray;

        };

        vm.Confirm = function () {
            $('#esModal').modal('hide');
            vm.params.checkedItems = [];
            vm.params.checkedItems = getCheckedItem(vm.ItemsList);
            console.log(vm.params.checkedItems);
        };

        var setSubItemCheck = function (item, value) {
            item.checked = value;
            if (item.hasChild) {
                if (value)
                    item.show = value;
                item.childrenSelectAll = value;
                angular.forEach(item.subItems, function (subItem) {
                    setSubItemCheck(subItem, value);
                });
            }
        };

        var getByParentId = function (array, parentId) {
            angular.forEach(array, function (item) {
                if (item.hasChild)
                    getByParentId(item.subItems, parentId);
                if (item.id === parentId)
                    vm.currentParent = item;
            });
        };

        var checkChildren = function (item) {
            if (item.hasChild) {
                angular.forEach(item.subItems, function (subItem) {
                    checkChildren(subItem);
                    if (subItem.checked)
                        item.checked = true;
                });
            }
        };

        var AreChildrenSelectAll = function (item) {
            var flag = true;
            angular.forEach(item.subItems, function (subItem) {
                if (!subItem.checked)
                    flag = false;
                if (subItem.hasChild && !subItem.childrenSelectAll)
                    flag = false;
            });
            item.childrenSelectAll = flag;
        };

        var setParentItemCheck = function (item) {
            if (item.parentId) {
                getByParentId(vm.ItemsList, item.parentId);
                vm.currentParent.checked = false;
                checkChildren(vm.currentParent);
                AreChildrenSelectAll(vm.currentParent);
                setParentItemCheck(vm.currentParent);
            }
        };

        vm.check = function (item) {
            item.checked = !item.checked;
            setSubItemCheck(item, item.checked);
            setParentItemCheck(item);
        };

        var calculateChildrenGrades = function (item) {
            var count = 0;
            angular.forEach(item.subItems, function (subItem) {
                count += subItem.grade;
            });
            item.grade = count;
        };

        var setParentItemGrade = function (item) {
            if (item.parentId) {
                getByParentId(vm.params.checkedItems, item.parentId);
                calculateChildrenGrades(vm.currentParent);
                setParentItemGrade(vm.currentParent);
            }
        };

        vm.changeGrade = function (item) {
            setParentItemGrade(item);
        };

    });