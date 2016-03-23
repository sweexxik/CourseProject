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
        Description:"",
        tags:{}   
    };

    $scope.loadTags = function($query) {
        return $scope.recievedTags.filter(function(tag) {
        return tag.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
      });;
    };



    creativeService.getCategories().then(function (results) {
        $scope.categories = results.data;
        $scope.currentCategory = results.data[0];
    }, function (error) {
        console.log(error);
    });

    creativeService.getAllTags().then(function(results){
        $scope.recievedTags = results.data;
    }, function(error){
        console.log(error);
    });


    
    $scope.createCreative = function(formData){
        newCreative.userName = localStorageService.get('authorizationData').userName;
        newCreative.categoryId = $scope.currentCategory.id;
        newCreative.name = $scope.creativeName;
        newCreative.Description = $scope.creativeDescription;
        newCreative.tags = $scope.tags;
        console.log(newCreative);
        creativeService.createCreative(newCreative).then(function(results){
            $location.path("/home");
        });     
   };

}]);