(function () {
    "use strict";

    angular
        .module("TodStreamingApp")
        .controller("mediaController", function ($scope) {
            // controller homePageController for module TodStreamingApp
            $scope.images=["1.png","5.png","9.png"];
        });

}());