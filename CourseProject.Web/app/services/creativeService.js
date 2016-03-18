'use strict';
app.factory('creativeService', ['$http', 'ngAuthSettings','localStorageService', function ($http, ngAuthSettings,localStorageService) {

 	var serviceBase = ngAuthSettings.apiServiceBaseUri;
   
    var creativesServiceFactory = {};

    var _getCreatives = function () {
    	var authData = localStorageService.get('authorizationData');
        var currentUserName = authData.userName;
        console.log("GET userName" + currentUserName);
        return $http.get(serviceBase + 'api/orders/?username=' + currentUserName)
            .then(function (results) { return results; });
    };

    var _getCategories = function () {
        return $http.get(serviceBase + 'api/categories')
            .then(function (results) { return results; });
    };

    creativesServiceFactory.getCreatives = _getCreatives;
    creativesServiceFactory.getCategories = _getCategories;

    return creativesServiceFactory;

}]);