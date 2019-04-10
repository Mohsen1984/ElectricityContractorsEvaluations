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
            controller: "PopulateEvaluationForm",
            controllerAs: 'PopulateEvaluationForm',
            templateUrl: viewPath + '/PopulateEvaluationForm.html'
        });
    $urlRouterProvider.otherwise('/');
}]);
//controller
app.controller("PopulateEvaluationForm",
    function ($scope, $state, $stateParams, esDatasource) {
        alert("salam="+$stateParams.id);
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
            vm.params.titleGrade1 = detailsDs.$data.titleGrade1;
            vm.params.titleGrade2 = detailsDs.$data.titleGrade2;
            vm.params.titleGrade3 = detailsDs.$data.titleGrade3;
            vm.params.minGrade = detailsDs.$data.minGrade;
            vm.params.enterDate = detailsDs.$data.enterDate;
            vm.params.actionDate = detailsDs.$data.actionDate;
            vm.params.actionDateColor = detailsDs.$data.actionDateColor;
            vm.params.sumGrade = detailsDs.$data.sumGrade;
            vm.params.sumGrade1 = detailsDs.$data.sumGrade1;
            vm.params.sumGrade2 = detailsDs.$data.sumGrade2;
            vm.params.sumGrade3 = detailsDs.$data.sumGrade3;
        };

        var detailsDs = new esDatasource({
            url: '/PopulateEvaluationForm/GetDetails',
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

        var calculateSumGrade = function () {
            vm.params.sumGrade = 0;
            angular.forEach(vm.params.checkedItems, function (item) {
                vm.params.sumGrade += item.grade;
            });
        };

        var setSumColor = function () {
            if (vm.params.sumGrade < vm.params.minGrade)
                vm.sumColor = 'red';
            else if (vm.show3) {
                if (vm.params.sumGrade < vm.params.sumGrade3)
                    vm.sumColor = 'orange';
                else
                    vm.sumColor = 'black';
                return;
            }
            else if (vm.show2) {
                if (vm.params.sumGrade < vm.params.sumGrade2)
                    vm.sumColor = 'orange';
                else
                    vm.sumColor = 'black';
                return;
            }
            else if (vm.show1) {
                if (vm.params.sumGrade < vm.params.sumGrade1)
                    vm.sumColor = 'orange';
                else
                    vm.sumColor = 'black';
                return;
            }
        };

        vm.changeGrade = function (item) {
            setParentItemGrade(item);
            calculateSumGrade();
            setSumColor();
        };

        vm.showWhat = function (num) {
            if (num == 0) {
                vm.showOne = false;
                vm.showTwo = false;
                vm.showThree = false;
                vm.show1 = false;
                vm.show2 = false;
                vm.show3 = false;
                vm.showZero = true;
                vm.show0 = true;
            }
            else {
                vm.showZero = false;
                vm.show0 = false;
                var count = 0;
                if (vm.show1)
                    count += 1;
                if (vm.show2)
                    count += 1;
                if (vm.show3)
                    count += 1;
                switch (count) {
                    case 1:
                        vm.showThree = false;
                        vm.showTwo = false;
                        vm.showOne = true;
                        break;
                    case 2:
                        vm.showThree = false;
                        vm.showTwo = true;
                        break;
                    case 3:
                        vm.showThree = true;
                        break;
                    case 0:
                        vm.showOne = false;
                        vm.showTwo = false;
                        vm.showThree = false;
                        vm.show1 = false;
                        vm.show2 = false;
                        vm.show3 = false;
                        vm.showZero = true;
                        vm.show0 = true;
                        break;
                }
            }
            setSumColor();

        };

        var setItems = function (data) {
            vm.ItemsList = ds.$data;
            angular.forEach(vm.ItemsList, function (item) {
                setSubItemCheck(item, true);
            });
            vm.params.checkedItems = getCheckedItem(vm.ItemsList);
            vm.TreeLoad = true;
        };

        var ds = new esDatasource({
            url: '/PopulateEvaluationForm/getTreeItems' ,
            method: 'GET',
            params: {
                id: $stateParams.id,

            },
            afterResponse: setItems
        });
        ds.refresh();

        vm.cutTitle = function (input, num) {
            if (input) {
                if (input.length <= num)
                    return input;
                var str = '';
                for (var j = 0; j < num; j++) {
                    str = str + input[j];
                }
                str += '...';
                return str;
            }
        };

        var calculateFilteredItems = function (array, filteredItems) {
            angular.forEach(array, function (item) {
                vm.filteredItemsCount += 1;
                filteredItems[vm.filteredItemsCount] = {};
                filteredItems[vm.filteredItemsCount].id = item.id;
                filteredItems[vm.filteredItemsCount].grade = item.grade;
                if (item.hasChild)
                    calculateFilteredItems(item.subItems, filteredItems);
            });
        };

        vm.send = function (items, url) {
            var filteredItems = [];
            vm.filteredItemsCount = -1;
            calculateFilteredItems(items, filteredItems);
            $http({
                method: 'POST',
                url: url,
                data: {
                    items: filteredItems
                }
            }).then(function (response) {
                console.log(response.data);
            }, function (error) {
                console.log(error.data);
            });
        };

    });