(function () {
    "use strict";
    angular
        .module("ToDStreamingApp.services", [])
        .service('Querier', ["$http",
            function ($http) {
                this.BuildQuery = function (computerName, textData, tags, startDate, endDate) {
                    var query = "?computerId=" + computerName;
                    if (startDate !== undefined)
                        query = query + "&from=" + startDate;
                    if (endDate !== undefined)
                        query = query + "&to=" + endDate;
                    if (textData)
                        query = query + "&text=" + textData;
                    if (tags)
                        query = query + "&tags=" + JSON.stringify(tags);
                    return query;

                };
                this.Query = function (queryText, callback) {
                    $http.get("http://localhost:53752/api/query" + queryText).success(callback);
                };
            }])
        .service('SharedData', function () {
            this.Images = {};
        });

}());