var basePath = '/app/home';
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
        .state('home', {
            url: '/',
            controller: "home",
            controllerAs: 'home',
            templateUrl: viewPath + '/home.html'
        });
    $urlRouterProvider.otherwise('/');
}]);
//controller
app.controller("home",
    function ($scope, $state, esDatasource) {
    });


