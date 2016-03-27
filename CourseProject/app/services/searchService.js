'use strict';
app.factory('searchService', ['$http',
 function ($http) {

  var searchServiceFactory = {};

  var _search = function(data) {
      return $http.post(serviceBase + 'api/creatives/search/', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    searchServiceFactory.search = _search;

    return searchServiceFactory;
}]);