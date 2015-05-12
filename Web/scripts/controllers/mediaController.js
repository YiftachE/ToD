(function () {
    "use strict";

    angular
        .module("TodStreamingApp")
        .controller("mediaController", ["$scope", "Querier",
            function ($scope, Querier) {
                // controller homePageController for module TodStreamingApp
                $scope.images = ["http://localhost:53752/api/pictures?guid=eb935ee1-ee78-4482-895d-d22b358f6bb5", "http://localhost:53752/api/pictures?guid=0ecfa588-9ee8-411e-9668-49f7cfa38771", "http://localhost:53752/api/pictures?guid=4613ebf4-3e60-4685-9f15-62eb541e263f"];
        }]);
}());