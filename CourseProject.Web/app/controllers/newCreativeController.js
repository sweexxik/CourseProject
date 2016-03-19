'use strict';
app.controller('newCreativeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location', 
    function ($http, $scope, creativeService, localStorageService, $location) {

    $scope.categories = [];
    $scope.currentCategory = "";
    $scope.newCreative = {
    	name:"",
        categoryId: 0,    	
    	userName:""   
    };

    creativeService.getCategories().then(function (results) {
        $scope.categories = results.data;
        $scope.currentCategory = results.data[0];
        console.log(results.data);   
    }, function (error) {
        console.log(error);
    });
    
    $scope.create = function(formData){
        $scope.newCreative.userName = localStorageService.get('authorizationData').userName;
        $scope.newCreative.categoryId = $scope.currentCategory.id;
        
        creativeService.createCreative($scope.newCreative).then(function(results){
            $location.path("/home");
        });     
   };
   
}]);