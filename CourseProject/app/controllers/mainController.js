'use strict';
app.controller('mainController', ['$scope','creativeService','searchService','$location',
    function ($scope,creativeService,searchService,$location) {

    $scope.creatives = [];
    $scope.tags = [];
    $scope.allCreatives = [];
    var delimiter = 0;


    $scope.sortType = 'id';
    $scope.sortReverse = false;

    var getInitialCreatives = function (){      
        creativeService.getPartialCreatives(0).then(function (results) {  
            $scope.allCreatives = results.data; 
          
        for(var i = 0; i< $scope.allCreatives.length; i++) {   
            $scope.allCreatives[i].popular = $scope.allCreatives[i].comments.length;      
            $scope.allCreatives[i].chapters = creativeService.sortChapters($scope.allCreatives[i]); 
        }   

          $scope.creatives =  results.data.slice(0,2);  
    
            
        }, function (error) {
            console.log(error);
        });
    };

    var getAdditionalCreatives = function (del){      
        creativeService.getPartialCreatives(del).then(function (results) {          
            $scope.allCreatives = $scope.allCreatives.concat(results.data);  
            console.log($scope.allCreatives);         
        for(var i = 0; i< $scope.creatives.length; i++) {   
            $scope.creatives[i].popular = $scope.allCreatives[i].comments.length;      
            $scope.creatives[i].chapters = creativeService.sortChapters($scope.allCreatives[i]); 
        }   
            
        }, function (error) {
            console.log(error);
        });
    };
    
    var counter = 0;
    getInitialCreatives();

    $scope.loadMore = function() {   

     
        if($scope.creatives.length < $scope.allCreatives.length){
            $scope.creatives.push($scope.allCreatives[$scope.creatives.length]);      
            console.log($scope.creatives.length);
       }   


       if( $scope.creatives.length > $scope.allCreatives.length - 2){
            if(counter == 0 ){ counter = 1;}
             if(counter < 5 && counter != 0) {           
                 getAdditionalCreatives(counter);
            counter++;
            }
           
       }

    }
       
       

   

    

   
   
    var x,i;
    creativeService.getAllTags().then(function (results) {
            var tags = results.data;  
            $scope.tags = tags.splice(1,10);            
        }, function (error) {
            console.log(error);
        });


    $scope.search = function(pattern){
         searchService.setSearchPattern(pattern);
        $location.path('/search');
    };

}]);