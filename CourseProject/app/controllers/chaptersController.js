'use strict';
app.controller('chaptersController', ['$showdown','$scope','$timeout','creativeService','$route','$routeParams', '$location', 'authService',
 function ($showdown,$scope,$timeout, creativeService,$route,$routeParams, $location, authService) {

    $scope.chapter = {};
    $scope.savedSuccessfully = true;
    $scope.showLoading = false; 
    $scope.message = '';

    // $scope.html = "4555"
    
    

    var chapterId = $routeParams.chapterId; 
    var creativeId = $routeParams.creativeId;   

    if(chapterId == 0){
        $scope.chapter.text = "# Write your markdown here!\n##It's easy."
    }
    else {
        creativeService.getChapter(chapterId).then(function(results){
            $scope.chapter = results.data;  
        });    

    }

    

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
        }, function(response){
            $scope.savedSuccessfully = false;
            $scope.showLoading = false; 
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
        
           
        });
     };
}]);