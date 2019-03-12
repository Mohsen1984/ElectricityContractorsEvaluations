var app1 = angular.module('esDatasource',[]);

app1.factory('esDatasource', function ($http, $location) {
    function DatasourceClass(dsoptions) {
        var defaultOptions = {
            useState: false,
            autoLoad: true,
            $data: null,
            method: 'POST'
        };
        var dataChangedHandlers = [];
        var options = angular.extend({}, defaultOptions, dsoptions);
        this.method = options.method;
        this.url = options.url;
        this.params = options.params;
        this.beforeRequest = options.beforeRequest;
        this.afterResponse = options.afterResponse;
        this.addDataChangeHandler = function (handler) {
            dataChangedHandlers.push(handler);
        };
        this.afterError = options.afterError;
        this.useState = options.useState;
        //0=not loaded || 1=loaded  || 2=loading
        this.loadState = 0;

        this.$data = options.$data;
        if (self.useState) {
            var urlObject = $location.search();
            if (urlObject.url)
                this.url = urlObject.url;
            if (urlObject.params) {
                this.params = JSON.parse(urlObject.params);
            }
        }
        this.setResult = function (result) {
            var oldData = this.$data;
            this.$data = result;
            dataChangedHandlers.forEach(function (handler) {
                handler.call(result, result, oldData);
            });
        };
        this.setUrl = function (url) {
            this.url = url;
        }
        this.addParams = function (params) {
            this.params = angular.extend({}, params, this.params);
        }
        this.setParams = function (params) {
            this.params = params;
        }
        this.removeParams = function (param) {
            for (var propertyName in param) {
                delete this.params[propertyName];
            }
        }
        this.clearParams = function () {
            this.params = null;
        }
        this.refresh = function () {
            var self = this;
            if (isFunction(self.beforeRequest))
                self.beforeRequest(self.url, self.params);
            if (!self.url) {
                console.log('esDatasource : url property is not definded');
                return;
            }
            if (!self.params)
                self.params = {};

            self.loadState = 2;
            var reqObj = {
                url: self.url,
                method: self.method,
            };
            if (self.method === "GET")
                reqObj.params = self.params;
            else
                reqObj.data = self.params;
            $http(reqObj)
                .then(function (response) {
                    self.loadState = 1;
                    self.setResult(response.data);
                    if (self.useState) {
                        $location.search('url', self.url);
                        $location.search('params', JSON.stringify(self.params));
                    }
                    if (isFunction(self.afterResponse))
                        self.afterResponse(response.data);
                },
                    function (error) {
                        self.loadState = 3;
                        if (isFunction(self.afterError))
                            self.afterError(error);
                        //console.log('esDatasource : error in remote source', error);
                    });
        }
        function isFunction(functionToCheck) {
            var getType = {};
            return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
        }
    }
    return DatasourceClass;
});
app1.directive('esDatasource', ["$compile", "esDatasource", "$parse",
    function ($compile, esDatasource, $parse) {
        return {
            restrict: 'EA',
            scope: false,
            controller: function ($element) {
            },
            compile: function (tElem, tAttrs) {
                return {
                    pre: function (scope, iElem, iAttrs) {

                    },
                    post: function (scope, iElem, iAttrs, ctrl) {

                        var autoload = scope.$eval(iAttrs.autoload) || false;
                        var name = iAttrs.name;
                        var data = $parse(iAttrs.data)(scope);
                        var url = iAttrs.url;
                        var method = iAttrs.method || false;
                        var params = $parse(iAttrs.params)(scope);
                        var ds = new esDatasource({

                        });
                        if (url)
                            ds.url = url;
                        if (method)
                            ds.method = method;
                        if (params)
                            ds.params = params;
                        if (iAttrs.data)
                            ds.$data = data;
                        if (autoload)
                            ds.refresh();
                        scope.$watch(iAttrs.params, function (newValue, oldValue) {
                            if (ds)
                                ds.params = newValue;
                        }, true);

                        var model = $parse(name);
                        model.assign(scope, ds);
                    }
                };
            }
        };
    }]);
