'use strict';
app.controller('homeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService','$window',
  function ($http, $scope, creativeService, localStorageService, $location, authService, $window) {

 	$scope.authentication = authService.authentication;
    $scope.creatives = [];
    $scope.chapters = [];

    if (authService.authentication.isAuth){
    	creativeService.getCreatives().then(function (results) {
            console.log(results.data);
            $scope.creatives = results.data;           
            for(var i = 0; i< $scope.creatives.length; i++) {             
                $scope.creatives[i].chapters = creativeService.sortChapters($scope.creatives[i]); 
            }   
        }, function (error) {
            console.log(error);            
        });
    }

    $scope.newCreative = function(){
    	$location.path('/newCreative');
    }

    $scope.deleteCreative = function (id) {     
        console.log(id);
        var result = $window.confirm('are you absolutely sure you want to delete?');
        if (result) {
            creativeService.deleteCreative(id).then(function(results){
                $scope.creatives = results.data;
                });         

            }        
        }    
    
}]);