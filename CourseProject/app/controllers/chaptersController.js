'use strict';
app.controller('chaptersController', ['$showdown','$scope','$timeout','creativeService','$route','$routeParams', '$location', 'authService',
 function ($showdown,$scope,$timeout, creativeService,$route,$routeParams, $location, authService) {

    $scope.chapter = {};
    $scope.savedSuccessfully = true;
    $scope.showLoading = false; 
    $scope.message = '';

    var chapterId = $routeParams.chapterId; 
    var creativeId = $routeParams.creativeId;     

    creativeService.getChapter(chapterId).then(function(results){
        $scope.chapter = results.data;
        console.log(results.data);
    });    

    $scope.update = function(chapter) {
      $scope.chapter.text = chapter;
      $scope.chapter.edit = false;
    };

    $scope.saveChapter = function(){    
        $scope.chapter.creativeId = parseInt(creativeId);   
        $scope.showLoading = true;     
        creativeService.saveChapter($scope.chapter).then(function(results){           
            $scope.savedSuccessfully = true;
            $scope.showLoading = false; 
            $scope.message = "Saved successfully"
            $timeout(function() {
                $location.path('/edit/' + creativeId);
            }, 5);
        }, function(error){
            $scope.savedSuccessfully = false;
            $scope.showLoading = false; 
            $scope.message = error.data.message;
           
        });
     };
}]);