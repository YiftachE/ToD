(function () {
    "use strict";
    angular
        .module("TodStreamingApp", ['ui.bootstrap', 'ngRoute', 'ui.bootstrap.datetimepicker', 'angular-loading-bar', 'ngAnimate', 'angular-flexslider', 'ToDStreamingApp.services', 'ui.splash','uiGmapgoogle-maps'])
        .config(function ($locationProvider, $routeProvider, uiGmapGoogleMapApiProvider) {
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
                .when('/mapsearch', {
                    templateUrl: '/pages/mapSearch.html',
                    controller: 'mapSearchController'
                })
                .otherwise({
                    redirectTo: '/'
                });
            uiGmapGoogleMapApiProvider.configure({
                //    key: 'your api key',
                v: '3.17',
                libraries: 'weather,geometry,visualization'
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