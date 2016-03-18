'use strict';
app.controller('homeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService',
  function ($http,$scope,creativeService, localStorageService, $location, authService) {

 	$scope.authentication = authService.authentication;
    $scope.creatives = [];

    if(authService.authentication.isAuth){
    	creativeService.getCreatives().then(function (results) {
            $scope.creatives = results.data;
      		console.log(results.data);       
        }, function (error) {
            console.log(error);
        });
    }

    $scope.newCreative = function(){
    	$location.path('/newCreative');
    }
}]);