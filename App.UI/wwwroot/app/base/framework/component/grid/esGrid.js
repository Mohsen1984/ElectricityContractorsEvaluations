var gridModule = angular.module('esGrid', ['esDatasource']);
gridModule.directive('esGrid', ["$http", "esDatasource", "$parse", "$compile", "$sce",
    function ($http, esDatasource, $parse, $compile, $sce) {
        return {
            restrict: 'A',
            transclude: true,
            scope: {
                datasource: '=gridDatasource',
                url: '&?apiUrl',
                rowSelect: '&gridRowSelect',
                currentPage: '=?defaultPage',
                itemsPerPage: '=?gridPageSize',
                enablePaging: '@?gridPaging',
                dataMode: '@?gridData'
            },
            controller: ["$scope", "$element",
                function ($scope, $element) {
                }],
            compile: function (tElem, tAttrs) {
                return {
                    pre: function (scope, iElem, iAttrs) {

                    },
                    post: function (scope, iElem, iAttrs, ctrl, transclude) {
                        var sc = scope.$parent.$new();
                        sc.datasource = scope.$parent.$eval(iAttrs.gridDatasource);
                        sc.rowSelect = iAttrs.gridRowSelect ? scope.$parent[iAttrs.gridRowSelect] : null;
                        sc.$rowId = iAttrs.gridValueMember || null;
                        scope.$selectedValue = $parse(iAttrs.gridSelectedValue)(scope.$parent) || null;
                        sc.rowClick = iAttrs.gridRowSelect ? scope.$parent[iAttrs.gridRowClick] : null;
                        sc.rowDblclick = iAttrs.gridRowSelect ? scope.$parent[iAttrs.gridRowDblclick] : null;
                        sc.$maxSize = iAttrs.gridPagerSize ? iAttrs.gridPagerSize : 10;
                        sc.url = scope.$parent.$eval(iAttrs.apiUrl);
                        sc.paging = scope.$parent.$eval(iAttrs.gridPaging) || false;
                        sc.dataMode = iAttrs.gridData || 'local';

                        sc.$multiselect = iAttrs.gridMultiselect || false;
                        sc.$items = sc.datasource.$data;
                        sc.$selectedItem = null;
                        if (sc.paging) {
                            sc.itemsPerPage = scope.$parent.$eval(iAttrs.itemsPerPage) || sc.datasource.params.pageSize || 5;
                            sc.currentPage = scope.$parent.$eval(iAttrs.currentPage) || sc.datasource.params.pageIndex + 1 || 1;
                            sc.$totalItems = sc.itemsPerPage * sc.currentPage;
                        }
                        sc.$watch("$selectedItem", function (item) {
                            if (sc.rowSelect)
                                sc.rowSelect(item);
                        }, true);

                        iElem.addClass("es-grid");
                        sc.$refresh = function () {
                            if (sc.datasource instanceof esDatasource === false)
                                throw "the datasource provided is not valid";
                            if (sc.dataMode === "remote") {
                                if (sc.paging) {
                                    sc.datasource.afterResponse = function (response) {
                                        addRowNumber(response.pageIndex + 1, response.pageSize, response.items);
                                        sc.$items = response.items;
                                        sc.itemsPerPage = response.pageSize;
                                        sc.$totalItems = response.totalItemsCount;
                                    };
                                }
                                else {
                                    sc.datasource.afterResponse = function (response) {
                                        sc.$items = response;
                                        sc.$totalItems = response.length;
                                    }
                                }
                                sc.datasource.refresh();
                            }
                            else if (sc.dataMode === "local") {
                                if (sc.paging) {
                                    var begin = ((sc.datasource.params.pageIndex) * sc.datasource.params.pageSize);
                                    var end = begin + sc.datasource.params.pageSize;
                                    sc.$items = sc.datasource.$data.slice(begin, end);
                                    sc.itemsPerPage = sc.datasource.params.pageSize;
                                    sc.$totalItems = sc.datasource.$data.length;
                                }
                                else {
                                    sc.$items = sc.datasource.$data;
                                    sc.$totalItems = sc.datasource.$data.length;
                                }
                            }
                        };
                        sc.$filter = function (params, url) {
                            if (url)
                                sc.datasource.setUrl(url);
                            if (params)
                                sc.datasource.addParams(params);
                            sc.datasource.refresh();
                        };
                        sc.$watch("$items", function (value) {
                            sc.$selectedItem = null;
                        });
                        sc.$watch("currentPage", function (value) {
                            if (sc.paging && value) {
                                sc.datasource.params.pageIndex = value - 1;
                                sc.$refresh();
                            }
                        });
                        sc.$selectItem = function (item) {
                            if (sc.$multiselect == true) {
                                if (!sc.$selectedItem)
                                    sc.$selectedItem = [];
                                var clickedItem = item;
                                var x = $(sc.$selectedItem).filter(function (i, n) {
                                    return n[sc.$rowId] == clickedItem[sc.$rowId];
                                });
                                var itemNumber = jQuery.inArray(item, sc.$selectedItem);
                                if (x.length)
                                    sc.$selectedItem = $(sc.$selectedItem).filter(function (i, n) {
                                        return n[sc.$rowId] != clickedItem[sc.$rowId];
                                    });
                                else
                                    sc.$selectedItem.push(item);
                            }
                            else
                                sc.$selectedItem = item;
                        };
                        sc.$init = function () {
                            if (sc.datasource.url && sc.dataMode === "local") {
                                sc.datasource.afterResponse = function (response) {
                                    sc.$refresh();
                                }
                                sc.datasource.refresh();
                            }
                            

                            if (sc.dataMode === "local") {
                                sc.datasource.addDataChangeHandler(function (item) {
                                    sc.$refresh();
                                });
                            }
                        }

                        sc.$init();
                        transclude(sc, function (clone, scope) {
                            iElem.append(clone);
                            addPager(clone, scope);
                        });

                        function addPager(clone, scope) {
                            if (scope.paging) {
                                var templatePath = window.frameworkPath + "/component/grid/gridPagination.html";
                                var pager = "<div class='panel-footer'> <ul uib-pagination " +
                                    "template-url='" + templatePath+"'" +
                                    "items-per-page='itemsPerPage'" +
                                    "total-items='$totalItems'" +
                                    "ng-model='currentPage'" +
                                    "max-size='$maxSize'" +
                                    "class='pagination-xs'" +
                                    "boundary-links='true'" +
                                    "force-ellipses='true'></ul>" +
                                    "<button ng-click='$refresh()' class=' refresh hidden-xs btn btn-default btn-sm pull-right'>" +
                                    "<i class='fa fa-refresh' ng-class=\"{ 'fa-spin' : datasource.loadState == 2 }\"  style='color: #337ab7;'></i>" +
                                    "</button>" +
                                    "</div>";
                                var keyEl = angular.element(pager);
                                iElem.append(keyEl);
                                $compile(keyEl)(scope);
                            }
                        }
                        function addRowNumber(pageIndex, pageSize, items) {
                            startIndex = ((pageIndex - 1) * pageSize) + 1;
                            items.forEach(function (item) {
                                item.$index = startIndex;
                                startIndex += 1;
                            });
                        }

                    }
                }
            },
        }
    }]);
gridModule.directive('gridSorting', ["$http", "esDatasource", "$parse", "$compile",
    function ($http, esDatasource, $parse, $compile) {
        return {
            //priority:1,
            restrict: 'A',
            transclude: true,
            require: '^esGrid',
            controller: function ($scope, $element, $attrs) {
            },
            compile: function (tElem, tAttrs) {
                return {
                    pre: function (scope, iElem, iAttrs) {
                    },
                    post: function (scope, iElem, iAttrs, ctrl, transclude) {
                        var sc = scope;
                        var dsName = "datasource";
                        var sortField = iAttrs.gridSorting;
                        if (!sortField || sortField === undefined)
                            throw "gridSorting : sortField is required";

                        if (!sc[dsName].params)
                            sc[dsName].params = {};
                        if (!sc[dsName].params.sortingOptions)
                            sc[dsName].params.sortingOptions = [];
                        iElem.on("click", function () {
                            sc.$apply(function () {
                                if (sc[dsName].params.sortingOptions[0] === null || sc[dsName].params.sortingOptions[0] === undefined) {
                                    sc[dsName].params.sortingOptions[0] = {
                                        field: sortField,
                                        order: 'asc'
                                    }
                                }
                                else if (sc[dsName].params.sortingOptions[0].field === sortField) {
                                    sc[dsName].params.sortingOptions[0].order = sc[dsName].params.sortingOptions[0].order === 'asc' ? 'dsc' : 'asc';
                                }
                                else {
                                    sc[dsName].params.sortingOptions[0].order = 'asc';
                                    sc[dsName].params.sortingOptions[0].field = sortField;
                                }
                                sc[dsName].refresh();
                            });
                        });
                        transclude(scope, function (clone, scope) {
                            iElem.append(clone);
                            addCarret(clone, scope);
                        });
                        function addCarret(clone, scope) {
                            var downCaret = angular.element('<span ng-show=\" ' + dsName + '.' + 'params.sortingOptions[0].field == \'' + sortField + '\' && ' + dsName + '.' + "params.sortingOptions[0].order == 'dsc' \" " + " class='fa fa-caret-down'></span>");
                            var upCaret = angular.element('<span ng-show=\" ' + dsName + '.' + 'params.sortingOptions[0].field == \'' + sortField + '\' && ' + dsName + '.' + "params.sortingOptions[0].order == 'asc' \" " + " class='fa fa-caret-up'></span>");
                            iElem.prepend(downCaret);
                            iElem.prepend(upCaret);
                            $compile(downCaret)(sc);
                            $compile(upCaret)(sc);
                        }
                    }
                }
            },
        }
    }]);
gridModule.directive('gridSelectable', ["$http", "esDatasource", "$parse", "$compile",
    function ($http, esDatasource, $parse, $compile) {
        return {
            restrict: 'A',
            require: '^esGrid',
            controller: function ($scope, $element, $attrs) {
            },
            compile: function (tElem, tAttrs) {
                return {
                    pre: function (scope, iElem, iAttrs) {
                    },
                    post: function (scope, iElem, iAttrs, ctrl) {
                        var item = scope.$eval(iAttrs.gridSelectable)
                        iElem.on("click", function () {
                            scope.$apply(function () {
                                scope.$selectItem(item);
                                //if (sc.rowClick)
                                //    sc.rowClick(sc.$selectedItem);
                            });
                        });
                        iElem.on("dblclick", function (e, u) {
                            scope.$apply(function () {
                                if (scope.rowDblclick)
                                    scope.rowDblclick(scope.$selectedItem);
                            });
                        });
                        iElem.attr("data-row-id", item);

                        scope.$watch('$selectedItem', function (value) {
                            if (!value) return;
                            if (scope.$multiselect != true) {
                                if (value === item)
                                    iElem.addClass('success');
                                else
                                    iElem.removeClass('success');
                            }
                            else {
                                var x = $(value).filter(function (i, n) {
                                    return n[scope.$rowId] == item[scope.$rowId];
                                });
                                if (x.length)
                                    iElem.addClass('success');
                                else
                                    iElem.removeClass('success');
                            }
                        }, true);
                    }
                }
            },
        }
    }]);


