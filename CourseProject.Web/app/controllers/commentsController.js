'use strict';
app.controller('commentsController', ['$scope', '$routeParams','$location', 'creativeService','$window','$route','localStorageService',
    function ($scope, $routeParams, $location,creativeService, $window,$route,localStorageService) {
    
    var creativeId = $routeParams.creativeId;
    
    var newCommentModel = {
        Id:0,
        text: "",      
        creativeId:0,
        userName:"" 
    };

    $scope.newComment = undefined;
    $scope.commentText = "";

    var newLikeModel = {
        commentId:0,
        userName:""
    };

    creativeService.getComments(creativeId).then(function(result){
        $scope.comments = result.data;
         console.log($scope.comments);
    }, function(error){
        console.log(error);
    });

    $scope.delete = function(id){
        var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
            creativeService.deleteComment(id).then(function(results) {
                $route.reload();
            });  
        }        
    }

    $scope.addComment = function(){
         $scope.newComment = {};
    };

    $scope.createComment = function(formData){        
        initComment();
        creativeService.createComment(newCommentModel).then(function(results){
             $route.reload();
         });     
   };

   $scope.addLike = function(id){
        newLikeModel.userName = localStorageService.get('authorizationData').userName;
        newLikeModel.commentId = id;
        creativeService.createLike(newLikeModel).then(function(result){
            $route.reload();
        })
   };

   var initComment = function(){
        newCommentModel.userName = localStorageService.get('authorizationData').userName;   
        newCommentModel.text = $scope.newComment.text;
        newCommentModel.creativeId = creativeId;
   };
}]);