'use strict';
app.controller('chaptersController', ['$showdown','$scope','creativeService','$route','$routeParams', '$location', 'authService',
 function ($showdown,$scope, creativeService,$route,$routeParams, $location, authService) {

    $scope.chapter = {};
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
        creativeService.saveChapter($scope.chapter).then(function(result){
            $location.path('/edit/' + creativeId);
        });
     };


}]);