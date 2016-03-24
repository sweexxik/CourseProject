'use strict';
app.factory('searchService', ['$http',
 function ($http) {

      var searchServiceFactory = {};

      var data = {
        pattern:''
        };

      var _setSearchPattern = function(pat){
          data.pattern = pat;
      };

    //   var _search = function () {      
    //     return $http.get(serviceBase + 'api/creatives/search/' + pattern)
    //         .then(function (results) { return results; });
    // };

    var _search = function() {
        return $http.post(serviceBase + 'api/creatives/search/', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

      
    
    searchServiceFactory.setSearchPattern = _setSearchPattern;
    searchServiceFactory.search = _search;

    return searchServiceFactory;
}]);