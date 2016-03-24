'use strict';
app.controller('indexController', ['$http','$scope', '$location', 'authService','searchService',
    function ($http,$scope, $location, authService, searchService) {
  
  	//todo save theme in coockies

    $scope.pattern = '';
    $scope.show = true;

    $scope.hide = function()
    {
        $scope.show = false;
    };
   
   
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.search = function(){ 
        searchService.setSearchPattern($scope.pattern);
        $location.path('/search');
        
       
    };




    $scope.changeTheme = function(){
    	if($scope.theme = "darkbootstrap")

    	console.log($scope.theme)
    }

    $scope.authentication = authService.authentication;

}]);