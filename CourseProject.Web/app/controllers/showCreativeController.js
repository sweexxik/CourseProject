'use strict';
app.controller('showCreativeController', ['$route','$scope', '$location','$timeout','$routeParams','authService','creativeService','localStorageService',
    function ($route,$scope, $location,$timeout,$routeParams, authService,creativeService,localStorageService) {
    
    $scope.authentication = authService.authentication;
    $scope.creative = [];   
    $scope.chapters = []; 
    $scope.comments = [];
    $scope.ratings = [];

    $scope.percentage1 = 0;
    $scope.percentage2 = 0;
    $scope.percentage3 = 0;
    $scope.percentage4 = 0;
    $scope.percentage5 = 0;

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

    var newRatingModel = {
        value:0,
        creativeId:0
    };

    
    creativeService.getCreative(creativeId).then(function (results) {
        initCreative(results);
        }, function (error) {
            console.log(error);
        });

    creativeService.getComments(creativeId).then(function(result){
        $scope.comments = result.data;
       }, function(error){
        console.log(error);
    });

     $scope.addComment = function(){
        if(authService.authentication.isAuth) {
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

    $scope.setLike = function(id){
        var commentId = id;
        newLikeModel.userName = localStorageService.get('authorizationData').userName;
        newLikeModel.commentId = commentId;
        creativeService.createLike(newLikeModel).then(function(result){
            for (var i = 0; i < $scope.comments.length; i++) {
                if($scope.comments[i].id === commentId){
                    $scope.comments[i] = result.data;
                    console.log($scope.comments[i]);
                    console.log(result.data);
                }
            }      
        })
   };

   $scope.setRating = function(id) {
        newRatingModel.creativeId = creativeId;
        newRatingModel.value = id;
        creativeService.createRating(newRatingModel).then(function(results){          
            $scope.ratings = results.data;
            $scope.ratingsAvg = (calcAvg()/$scope.ratings.length).toPrecision(3);
            setPercentage();
        });     
   };

    var calcAvg = function (){        
        var sum = 0;
        for (var i = 0; i < $scope.ratings.length; i++) {            
            sum = sum + $scope.ratings[i].value;          
        }
        return sum;
   };

   var initComment = function(){
        newCommentModel.userName = localStorageService.get('authorizationData').userName;   
        newCommentModel.text = $scope.newComment.text;
        newCommentModel.creativeId = creativeId;
   };

   var initCreative = function (results) {
        $scope.creative = results.data;
        $scope.chapters = creativeService.sortChapters(results.data); 
        $scope.ratings = results.data.rating;
        $scope.ratingsAvg = (calcAvg()/$scope.ratings.length).toPrecision(3);
        setPercentage();
   }

    var setPercentage = function () {

        $scope.percentage1 = 0;    
        $scope.percentage2 = 0;
        $scope.percentage3 = 0;    
        $scope.percentage4 = 0;    
        $scope.percentage5 = 0;   
        for (var i = 0; i < $scope.ratings.length; i++) {
        switch($scope.ratings[i].value){
            case 1: $scope.percentage1++;
                break;
            case 2: $scope.percentage2++;
                break;
            case 3: $scope.percentage3++;
                break;
            case 4: $scope.percentage4++;
                break;
            case 5: $scope.percentage5++;
            default: 
                break;
            }
        }

        var len =  $scope.ratings.length /200;

        $scope.percentage1 =  ($scope.percentage1/len).toPrecision(3);
        $scope.percentage2 =  ($scope.percentage2/len).toPrecision(3);
        $scope.percentage3 =  ($scope.percentage3/len).toPrecision(3);
        $scope.percentage4 =  ($scope.percentage4/len).toPrecision(3);
        $scope.percentage5 =  ($scope.percentage5/len).toPrecision(3);
   }  

}]);