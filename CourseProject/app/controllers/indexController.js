'use strict';
app.controller('indexController', ['$http','$scope', '$location', 'authService','searchService',
    function ($http,$scope, $location, authService, searchService) {

    $scope.pattern = '';
   
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }


    $scope.search = function(){
        console.log($scope.pattern)
        searchService.setSearchPattern($scope.pattern);
        $location.path('/search');
    };



    $scope.changeTheme = function(){
        if($scope.theme = "darkbootstrap")

        console.log($scope.theme)
    }

    $scope.authentication = authService.authentication;

}]);