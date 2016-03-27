'use strict';
app.controller('indexController', ['$http','$scope', '$location', 'authService','searchService',
    function ($http,$scope, $location, authService, searchService) {

   
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