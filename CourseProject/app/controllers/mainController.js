'use strict';
app.controller('mainController', ['$scope','creativeService','searchService','$location',
    function ($scope,creativeService,searchService,$location) {

    $scope.creatives = [];
    $scope.tags = [];
    $scope.words = [];

    $scope.sortType = 'created';
    $scope.sortReverse = true;

    creativeService.getAllCreatives().then(function (results) {
            $scope.creatives = results.data;
             for(var i = 0; i< $scope.creatives.length; i++) {   
                $scope.creatives[i].popular = $scope.creatives[i].comments.length;      
                $scope.creatives[i].chapters = creativeService.sortChapters($scope.creatives[i]); 
            }   
            console.log($scope.creatives);       
        }, function (error) {
            console.log(error);
        });
    var x,i;
    creativeService.getAllTags().then(function (results) {
            var tags = results.data;  
            $scope.tags = tags.splice(1,10);            
            console.log($scope.tags);
            
        }, function (error) {
            console.log(error);
        });

    $scope.tagClick = function(id){
        console.log(id);
    };

    $scope.search = function(pattern){
         searchService.setSearchPattern(pattern);
        $location.path('/search');
    };

}]);