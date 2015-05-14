(function () {
    "use strict";

    angular
        .module("TodStreamingApp")
        .controller("mediaController", ["$scope", "Querier",
            function ($scope, Querier) {
                // controller homePageController for module TodStreamingApp
                $scope.images = [];
        }]);
}());