'use strict';
app.controller('chaptersController', ['$showdown','$scope','creativeService','$route','$routeParams', '$location', 'authService',
 function ($showdown,$scope, creativeService,$route,$routeParams, $location, authService) {

    $scope.notes = [];

    $scope.addNote = function(){
        $scope.newNote = {};
        $scope.newNote.createdOn = Date.now();
        $scope.newNote.text = ' ';
        $scope.newNote.edit = true;
        $scope.notes.push($scope.newNote);
        $scope.newNote = {};
    };

    var chapterId = $routeParams.chapterId; 
    var creativeId = $routeParams.creativeId;     
    
    if (chapterId == 0){
      $scope.addNote();
    }
    else {
          creativeService.getChapter(chapterId).then(function(results){
          $scope.notes = results.data;
          console.log(results.data);
        });
    };

    $scope.delete = function (i) {
      var r = confirm("Are you sure you want to delete this note?");
      if (r == true) 
        $scope.notes.splice(i, 1);
    };
    

    $scope.update = function(i, note) {
      $scope.notes[i].text = note;
      $scope.notes[i].edit = false;
    };

    $scope.saveChapter = function(){
      creativeService.saveChapter($scope.notes[0]).then(function(result){
        $location.path('/edit/' + creativeId);
      });
    };
  


}]);