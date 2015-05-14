(function () {
    "use strict";

    angular
        .module("TodStreamingApp")
        .controller("mediaController", ["$scope", "Querier", "SharedData",
            function ($scope, Querier, SharedData) {
                // controller homePageController for module TodStreamingApp
                $scope.sliderLoaded = false;
                $scope.currentImageIndex=1;
                $scope.showProgressBar = function () {
                    $scope.sliderLoaded = true;
                };
                $scope.after = function ($slider) {
                    
                    $scope.currentImageIndex = $slider.element.currentSlide+1;
                };
                $scope.before = function () {
                    $scope.currentImageIndex = $scope.currentImageIndex - 1;
                };
                $scope.images = SharedData.Images;
        }]);
}());