'use strict';
app.controller('searchController', ['$http','$scope', '$location','searchService',
    function ($http,$scope, $location, searchService) {
  
  	//todo save theme in coockies
    $scope.results = [];

    searchService.search().then(function(results){
         $scope.results = results.data;
    }); 
  
    
   

}]);