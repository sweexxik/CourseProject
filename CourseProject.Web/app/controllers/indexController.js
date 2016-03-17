'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
  
  	//todo save theme in coockies
   
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }



    $scope.changeTheme = function(){
    	if($scope.theme = "darkbootstrap")

    	console.log($scope.theme)
    }

    $scope.authentication = authService.authentication;

}]);