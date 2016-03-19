'use strict';
app.factory('creativeService', ['$http', 'ngAuthSettings','localStorageService', function ($http, ngAuthSettings,localStorageService) {

 	var serviceBase = ngAuthSettings.apiServiceBaseUri;
   
    var creativesServiceFactory = {};

    var _getCreatives = function () {
    	var currentUserName = localStorageService.get('authorizationData').userName;
        return $http.get(serviceBase + 'api/creatives/getall/' + currentUserName)
            .then(function (results) { return results; });
    };

    var _getAllCreatives = function () {      
        return $http.get(serviceBase + 'api/creatives/getall')
            .then(function (results) { return results; });
    };

    var _getCreative = function (id) {        
        return $http.get(serviceBase + 'api/creatives/' +id)
            .then(function (results) { return results; });
    };

    var _deleteCreative = function (id) {   
        return $http.post(serviceBase + 'api/creatives/delete/' + id)
            .then(function (results) { return results; });
    };

    var _getCategories = function () {
        return $http.get(serviceBase + 'api/categories')
            .then(function (results) { return results; });
    };

    creativesServiceFactory.getAllCreatives = _getAllCreatives
    creativesServiceFactory.getCreatives = _getCreatives;
    creativesServiceFactory.getCreative = _getCreative;
    creativesServiceFactory.getCategories = _getCategories;
    creativesServiceFactory.deleteCreative = _deleteCreative;
    
    return creativesServiceFactory;

}]);