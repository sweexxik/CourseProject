'use strict';
app.factory('ordersService', ['$http', 'ngAuthSettings','localStorageService', function ($http, ngAuthSettings,localStorageService) {

 	var serviceBase = ngAuthSettings.apiServiceBaseUri;
   
    var ordersServiceFactory = {};

    var _getOrders = function () {
	var authData = localStorageService.get('authorizationData');
    var currentUserName = authData.userName;
    console.log("GET userName" + currentUserName);
        return $http.get(serviceBase + 'api/orders/?username=' + currentUserName).then(function (results) {
            return results;
        });
    };

    ordersServiceFactory.getOrders = _getOrders;

    return ordersServiceFactory;

}]);