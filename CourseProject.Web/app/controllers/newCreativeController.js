'use strict';
app.controller('newCreativeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location', 
    function ($http, $scope, creativeService, localStorageService, $location) {

    $scope.categories = [];
    $scope.currentCategory = "";
    $scope.creativeName = "";
    $scope.creativeDescription = ""

    var newCreative = {
    	name:"",
        categoryId: 0,    	
    	userName:"",
        Description:""   
    };

    creativeService.getCategories().then(function (results) {
        $scope.categories = results.data;
        $scope.currentCategory = results.data[0];
        console.log(results.data);   
    }, function (error) {
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