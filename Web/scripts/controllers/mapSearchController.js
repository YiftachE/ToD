(function () {
    "use strict";

    angular
        .module("TodStreamingApp")
        .controller("mapSearchController", function ($scope, uiGmapGoogleMapApi) {
            // controller homePageController for module TodStreamingApp
            var createRandomMarker = function (i, bounds, idKey) {
                var lat_min = bounds.southwest.latitude,
                    lat_range = bounds.northeast.latitude - lat_min,
                    lng_min = bounds.southwest.longitude,
                    lng_range = bounds.northeast.longitude - lng_min;

                if (idKey == null) {
                    idKey = "id";
                }

                var latitude = lat_min + (Math.random() * lat_range);
                var longitude = lng_min + (Math.random() * lng_range);
                var ret = {
                    latitude: latitude,
                    longitude: longitude,
                    title: 'm' + i
                };
                ret[idKey] = i;
                return ret;
            };
            $scope.computerMarkers = [];
            $scope.$watch(function () {
                return $scope.map.bounds;
            }, function (nv, ov) {
                // Only need to regenerate once
                if (!ov.southwest && nv.southwest) {
                    var markers = [];
                    for (var i = 0; i < 50; i++) {
                        markers.push(createRandomMarker(i, $scope.map.bounds))
                    }
                    $scope.randomMarkers = markers;
                }
            }, true);
            $scope.map = {
                center: {
                    latitude: 31.743818,
                    longitude: 35.203161
                },
                zoom: 8
            };
            uiGmapGoogleMapApi.then(function (maps) {

            });
        });

}());