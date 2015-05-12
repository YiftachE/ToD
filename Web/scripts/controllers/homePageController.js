(function () {
    "use strict";

    angular
        .module("TodStreamingApp")
        .controller("homePageController", ["$scope", "$location", "Querier",
            function ($scope, $location, Querier) {
                $scope.dateTimeNow = function () {
                    $scope.startDate = new Date();
                    $scope.endDate = new Date();
                };
                $scope.Querier = Querier;
                $scope.dateTimeNow();

                $scope.maxDate = new Date('2014-06-22');

                $scope.dateOptions = {
                    startingDay: 1,
                    showWeeks: false
                };

                $scope.hourStep = 1;
                $scope.minuteStep = 15;

                $scope.timeOptions = {
                    hourStep: [1, 2, 3],
                    minuteStep: [1, 5, 10, 15, 25, 30]
                };

                $scope.showMeridian = true;
                $scope.timeToggleMode = function () {
                    $scope.showMeridian = !$scope.showMeridian;
                };

                $scope.message = {};
                $scope.dateFlag = true;
                $scope.isCollapsed = $scope.dateFlag;
                $scope.message[$scope.dateFlag] = "Filter by Date"
                $scope.message[!$scope.dateFlag] = "Cancel"
                $scope.dateFilterMessage = $scope.message[$scope.dateFlag]
                $scope.ToggleDateSelector = function () {
                    $scope.dateFlag = !$scope.dateFlag;
                    $scope.isCollapsed = $scope.dateFlag;
                    $scope.dateFilterMessage = $scope.message[$scope.dateFlag]
                };
                $scope.Search = function () {
                    if (!$scope.dateFlag) {
                        var query = Querier.BuildQuery($scope.computerName, $scope.startDate.toUTCString(), $scope.endDate.toUTCString());
                    }
                    else
                    {
                        var query = Querier.BuildQuery($scope.computerName);
                    }
                    Querier.Query(query, function (data) {
                        console.log(data);
                    });
                    $location.path("/media/query=" + query);
                };
        }]);
}());