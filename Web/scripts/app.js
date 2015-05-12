(function () {
    "use strict";
    angular
        .module("TodStreamingApp", ['ui.bootstrap', 'ngRoute', 'ui.bootstrap.datetimepicker', 'angular-loading-bar', 'ngAnimate', 'angular-flexslider','ToDStreamingApp.services'])
        .config(function ($locationProvider, $routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: 'pages/home.html',
                    controller: 'homePageController'
                })
                .when('/results/:query', {
                    templateUrl: 'pages/results.html',
                    controller: 'resultPageController'
                })
                .when('/media/:query', {
                    templateUrl: '/pages/mediaPage.html',
                    controller: 'mediaController'
                })
                .otherwise({
                    redirectTo: '/'
                });

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });
        })
        .run(function () {

        })
        .factory("$exceptionHandler", function () {
            // any uncaught exception in angular expressions is delegated to this service
            return function (exception, cause) {
                exception.message += " (caused by '" + cause + "')";
                throw exception;
            };
        });
}());