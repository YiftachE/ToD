(function () {
    "use strict";
    angular
        .module("ToDStreamingApp.services", [])
        .service('Querier', ["$http",
            function ($http) {
                this.BuildQuery = function (computerName, startDate, endDate) {
                    var query = "?computerId=" + computerName;
                    if (startDate != undefined)
                        query = query + "&from=" + startDate;
                    if (endDate != undefined)
                        query = query + "&to=" + endDate;
                    return query;

                };
                this.Query = function (queryText, callback) {
                    $http.get("http://localhost:53752/api/pictures" + queryText).success(callback);
                };
            }])
        .service('SharedData', function () {
            this.Images = {};
        });

}());