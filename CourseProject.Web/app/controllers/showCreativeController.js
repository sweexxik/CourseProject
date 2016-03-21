'use strict';
app.controller('showCreativeController', ['$route','$scope', '$location','$timeout','$routeParams','authService','creativeService','localStorageService',
    function ($route,$scope, $location,$timeout,$routeParams, authService,creativeService,localStorageService) {
    
    $scope.authentication = authService.authentication;
    $scope.creative = [];   
    $scope.chapters = []; 
    $scope.comments = [];
    $scope.newComment = undefined;
    var creativeId = $routeParams.Id;   
    var newCommentModel = {
        Id:0,
        text: "",      
        creativeId:0,
        userName:""
    };
    var newLikeModel = {
        commentId:0,
        userName:""
    };

    
    creativeService.getCreative(creativeId).then(function (results) {
        $scope.creative = results.data;
        $scope.chapters = creativeService.sortChapters(results.data); 
        console.log($scope.creative);  
        console.log($scope.chapters);       
        }, function (error) {
            console.log(error);
        });

    creativeService.getComments(creativeId).then(function(result){
        $scope.comments = result.data;
         console.log($scope.comments);
    }, function(error){
        console.log(error);
    });

     $scope.addComment = function(){
        if(authService.authentication.isAuth){
         $scope.newComment = {};
        }
        else {            
            $location.path("/login");
        }
    };

     $scope.createComment = function(formData){        
        initComment();
        creativeService.createComment(newCommentModel).then(function(results){
             $route.reload();
         });     
   };

   var initComment = function(){
        newCommentModel.userName = localStorageService.get('authorizationData').userName;   
        newCommentModel.text = $scope.newComment.text;
        newCommentModel.creativeId = creativeId;
   };

    $scope.addLike = function(id){
        newLikeModel.userName = localStorageService.get('authorizationData').userName;
        newLikeModel.commentId = id;
        creativeService.createLike(newLikeModel).then(function(result){
            $route.reload();
        })
   };

}]);