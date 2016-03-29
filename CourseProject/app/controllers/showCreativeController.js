'use strict';
app.controller('showCreativeController', ['$showdown','$sce','$window','$route','$scope', '$location','$timeout','$routeParams',
    'authService','creativeService','localStorageService', 'searchService',
    function ($showdown,$sce, $window, $route, $scope, $location, $timeout, $routeParams,
     authService, creativeService, localStorageService,searchService) {
    
    $scope.authentication = authService.authentication;
    $scope.creative = [];   
    $scope.chapters = []; 
    $scope.comments = [];
    $scope.tags = [];
    $scope.ratings = [];
    
    $scope.percentage1 = 0;
    $scope.percentage2 = 0;
    $scope.percentage3 = 0;
    $scope.percentage4 = 0;
    $scope.percentage5 = 0;

    $scope.newComment = undefined;

    var creativeId = $routeParams.Id; 

    var savedChapter = 0;

    var newCommentModel = {
        Id:0,
        text: "",      
        creativeId:0,
        userName:""
    };
    
    var newLikeModel = {
        commentId:0,
        userName:"",
        id:0
    };

    var newRatingModel = {
        value:0,
        creativeId:0,
        userName:""
    };

    $scope.storeChapterId = function(id){
        savedChapter++;       
    }  
    
    creativeService.getCreative(creativeId).then(function (results) {
        initCreative(results.data);       
        }, function (error) {
            console.log(error);
        });

    creativeService.getComments(creativeId).then(function(result){
        $scope.comments = result.data;
       }, function(error){
        console.log(error);
        });

    creativeService.getCreativeTags(creativeId).then(function(result){
        $scope.tags = result.data;      
    }, function(error){
        console.log(error);
    });

     $scope.showNewComment = function(){
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
            $scope.comments = results.data;
            console.log(results.data);
         });     
    };

   $scope.deleteComment = function(id){
        var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
            creativeService.deleteComment(id).then(function(results) {
                $scope.comments = results.data;
            });  
        }        
    }

    $scope.setLike = function(id){
        var commentId = id;
        newLikeModel.userName = localStorageService.get('authorizationData').userName;
        newLikeModel.commentId = commentId;

        creativeService.createLike(newLikeModel).then(function(result){
            for (var i = 0; i < $scope.comments.length; i++) {
                if($scope.comments[i].id === commentId){
                    $scope.comments[i] = result.data;  
                    console.log(result.data);               
                }
            }      
        })
   };

   $scope.setRating = function(id) {

        newRatingModel.creativeId = creativeId;
        newRatingModel.value = id;
        newRatingModel.userName = localStorageService.get('authorizationData').userName;

        creativeService.createRating(newRatingModel).then(function(results){          
            $scope.ratings = results.data;
            $scope.ratingsAvg = calcAvg();
            setPercentage();
        });     
   };

   $scope.search = function(pattern){
         searchService.setSearchPattern(pattern);
        $location.path('/search');
    };

    var calcAvg = function (){        
        var sum = 0;
        var defaultResult = 0;
        for (var i = 0; i < $scope.ratings.length; i++) {            
            sum = sum + $scope.ratings[i].value;          
        }
        var value = (sum/$scope.ratings.length).toPrecision(3);
        if ($.isNumeric(value)) {
            return value;
        }
        return defaultResult;
   };

    var initCreative = function (data) {
        console.log(data);
        if(data != null){
            $scope.creative = data;
            $scope.chapters = creativeService.sortChapters(data); 
            $scope.ratings = data.rating;
            $scope.ratingsAvg = calcAvg();
            setPercentage();

            for (var i = 0; i < $scope.chapters.length; i++) {
                var md = $showdown.makeHtml($scope.chapters[i].body);
                $scope.chapters[i].body = $sce.trustAsHtml(md);
            }      
        }        
   }

   var initComment = function(){
        newCommentModel.userName = localStorageService.get('authorizationData').userName;   
        newCommentModel.text = $scope.newComment.text;
        newCommentModel.creativeId = creativeId;
   };   

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

        //magic value
        var len =  $scope.ratings.length /255;

        $scope.percentage1 =  ($scope.percentage1/len).toPrecision(3);
        $scope.percentage2 =  ($scope.percentage2/len).toPrecision(3);
        $scope.percentage3 =  ($scope.percentage3/len).toPrecision(3);
        $scope.percentage4 =  ($scope.percentage4/len).toPrecision(3);
        $scope.percentage5 =  ($scope.percentage5/len).toPrecision(3);
   }  

}]);