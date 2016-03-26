'use strict';
app.controller('chaptersController', ['$showdown','$scope','creativeService','$route','$routeParams', '$location', 'authService',
 function ($showdown,$scope, creativeService,$route,$routeParams, $location, authService) {

    $scope.chapter = {};
    var chapterId = $routeParams.chapterId; 
    var creativeId = $routeParams.creativeId;     

    $scope.addChapter = function(){
        $scope.newChapter = {};
        $scope.newChapter.createdOn = Date.now();
        $scope.newChapter.text = ' ';
        $scope.newChapter.edit = true;
        $scope.chapter = $scope.newChapter;
        $scope.newChapter = {};
    };

   
    
    if (chapterId == 0) {
        $scope.addChapter();
    }
    else {
        creativeService.getChapter(chapterId).then(function(results){
        $scope.chapter = results.data;
        console.log(results.data);
        });
    };

    $scope.deleteChapter = function () {
        var res = confirm("Are you sure you want to delete this chapter?");
        if (res){ 
            creativeService.deleteChapter(chapterId).then(function(results){
                console.log(results);
                $location.path('/edit/' + creativeId);
            });
        }
    };   

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