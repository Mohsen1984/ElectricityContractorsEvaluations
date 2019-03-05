var basePath = '/app/PopulateEvaluationForm';
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
        .state('PopulateEvaluationForm', {
            url: '/:id',
            //url: '/',
            controller: "PopulateEvaluationForm",
            controllerAs: 'PopulateEvaluationForm',
            templateUrl: viewPath + '/PopulateEvaluationForm.html'
        });
    $urlRouterProvider.otherwise('/');
}]);
//controller
app.controller("PopulateEvaluationForm",
    function ($scope, $state, $stateParams, esDatasource, $http) {

        var vm = this;

        vm.ItemsList = [];
        //vm.ItemsList = [
        //    { id: 1, text: 'menu1', hasChild: false ,grade1: 12 ,grade2:13 ,grade3: 20},
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
            url: '/PopulateEvaluationForm/GetDetails',
            method: 'GET',
            params: {
                id: $stateParams.id
            },
            afterResponse: setDetails
        });
        detailsDs.refresh();

        vm.TreeLoad = false;

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
            if (array) {
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
            }

        };

        vm.Confirm = function () {
            $('#esModal').modal('hide');
            vm.params.checkedItems = [];
            vm.params.checkedItems = getCheckedItem(vm.ItemsList);
            //console.log(vm.params.checkedItems);
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

        vm.showWhat = function (num, valid) {
            if (valid) {
                vm.showZero = false;
                vm.showOne = false;
                vm.showTwo = false;
                vm.showThree = false;
                vm.show0 = false;
                vm.show1 = false;
                vm.show2 = false;
                vm.show3 = false;
                if (num == 0) {
                    vm.showZero = true;
                    vm.show0 = true;
                }
                else if (num == 1) {
                    vm.showOne = true;
                    vm.show1 = true;
                }
                else if (num == 2) {
                    vm.showTwo = true;
                    vm.show1 = true;
                    vm.show2 = true;
                }
                else if (num == 3) {
                    vm.showThree = true;
                    vm.show1 = true;
                    vm.show2 = true;
                    vm.show3 = true;
                }
            }
            else {
                vm.showZero = false;
                vm.showOne = false;
                vm.showTwo = false;
                vm.showThree = false;
                vm.show0 = false;
                vm.show1 = false;
                vm.show2 = false;
                vm.show3 = false;
                if (num == 0 || num == 1) {
                    vm.showZero = true;
                    vm.show0 = true;
                }
                else if (num == 2) {
                    vm.showOne = true;
                    vm.show1 = true;
                }
                else if (num == 3) {
                    vm.showOne = true;
                    vm.show1 = true;
                    vm.showTwo = true;
                    vm.show2 = true;
                }
            }
        };

        var setItems = function (data) {
            console.log(ds.$data);
            vm.ItemsList = ds.$data;
            angular.forEach(vm.ItemsList, function (item) {
                setSubItemCheck(item, true);
            });
            vm.params.checkedItems = getCheckedItem(vm.ItemsList);
            vm.TreeLoad = true;
        };

        var ds = new esDatasource({
            url: '/PopulateEvaluationForm/getTreeItems',
            method:'GET',
            params: {
                id: $stateParams.id
            },
            afterResponse: setItems
        });
        ds.refresh();

    });