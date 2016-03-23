'use strict';
app.controller('newCreativeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location', 
    function ($http, $scope, creativeService, localStorageService, $location) {

    $scope.categories = [];
    $scope.currentCategory = "";
    $scope.creativeName = "";
    $scope.creativeDescription = ""
    $scope.tags = [];

    var newCreative = {
    	name:"",
        categoryId: 0,    	
    	userName:"",
        Description:""   
    };

    $scope.loadTags = function(query) {
        return $scope.recievedTags;
    };

    creativeService.getCategories().then(function (results) {
        $scope.categories = results.data;
        $scope.currentCategory = results.data[0];
    }, function (error) {
        console.log(error);
    });

    creativeService.getTags().then(function(results){
        $scope.recievedTags = results.data;
        console.log($scope.recievedTags);
    }, function(error){
        console.log(error);
    });
    
    $scope.create = function(formData){
        newCreative.userName = localStorageService.get('authorizationData').userName;
        newCreative.categoryId = $scope.currentCategory.id;
        newCreative.name = $scope.creativeName;
        newCreative.Description = $scope.creativeDescription;
        console.log(newCreative.Description);
        
        creativeService.createCreative(newCreative).then(function(results){
            $location.path("/home");
        });     
   };

}]);