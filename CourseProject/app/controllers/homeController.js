'use strict';
app.controller('homeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService','$window','$uibModal',
  function ($http, $scope, creativeService, localStorageService, $location, authService, $window,$uibModal) {

 	$scope.authentication = authService.authentication;
    $scope.userInfo = [];
    $scope.creatives = [];
    $scope.chapters = [];

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


    if (authService.authentication.isAuth){

        authService.getProfileInfo().then(function(results){
                $scope.userInfo = results.data;              
        });

    	creativeService.getCreatives().then(function (results) {        
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
            $scope.creatives = results.data;
        });     
   }; 

    
}]);