'use strict';
app.controller('userPageController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService','$window','$uibModal','$route','$routeParams',
  function ($http, $scope, creativeService, localStorageService, $location, authService, $window,$uibModal, $route, $routeParams) {

    var userName = $routeParams.username; 

    var authData = localStorageService.get('authorizationData');     
    
    if( authData != undefined){
        authService.getProfile(authData.userName).then(function(results){
            $scope.currentUserInfo = results.data;
            console.log()              
        }); 
    }
    

 	$scope.authentication = authService.authentication;
    $scope.userInfo = [];
    $scope.creatives = [];


    $scope.sortType = 'created';
    $scope.sortReverse = true;

    $scope.searchCreatives = '';


    $scope.tags = [];

    authService.getProfile(userName).then(function(results){
            $scope.userInfo = results.data;
            console.log()              
        });

    creativeService.getCreativesByName(userName).then(function (results) {        
            $scope.creatives = results.data;
        }, function (error) {
            $location.path('/NotFound')
            console.log(error);            
        });
    
   

    $scope.loadTags = function($query) {
        return $scope.recievedTags.filter(function(tag) {
        return tag.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
      });;
    };

    creativeService.getAllTags().then(function(results){
        $scope.recievedTags = results.data;
    }, function(error){
        console.log(error);
    });
    
}]);