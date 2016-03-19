'use strict';
app.controller('newCreativeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location', function ($http, $scope, creativeService, localStorageService, $location) {

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


    // $scope.getThing = function(id,index){
    // 	$scope.creativeId = id;
    // 	console.log(index);
    // 	var selectedchapter = $scope.orders[id];     	
    // 	$scope.selectedchapter = selectedchapter.chapters[index];
    // 	console.log($scope.selectedchapter);    
    // 		$scope.show = true;
    // }

    // $scope.showCreativeDialog = function(){
    // 	$scope.showAddForm = true;
    // 	$scope.currentChapter = 1;
    // }

    $scope.check = function(){
        console.log($scope.currentCategory);
    }

   
    $scope.createCreative = function(formData){
    	
    	var authData = localStorageService.get('authorizationData');   
    	
        $scope.newCreative.userName = authData.userName;
        $scope.newCreative.categoryId = $scope.currentCategory.id;

    	var data = $scope.newCreative;
        console.log(data);
    	//todo change into variable    	
        $http.post('http://localhost:57507/api/creatives', JSON.stringify(data), {
         headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } }).success(function (response) {
                console.log(response);
                $location.path('/home');
      

        }).error(function (err, status) {
            console.log(err);
             console.log(status);

        });
    };
}]);