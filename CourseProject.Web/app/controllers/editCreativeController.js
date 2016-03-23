'use strict';
app.controller('editCreativeController', ['$http', '$scope','$route','$routeParams','creativeService','$window','$location',
	function ($http, $scope, $route, $routeParams,creativeService, $window, $location) {
 		
		var creativeId = $routeParams.Id; 	

 		$scope.creative = []; 	
 		$scope.chapters = []; 
 		$scope.stub = []; 
 		$scope.selectedchapter = {};
		$scope.selectedchapter.creativeId = creativeId;
		$scope.recievedTags = [];
		$scope.tags = [];

 			
 		var chapterModel = {
	    	body:"",
	        creativeId:0,    	
	    	id:0,
	        name:"",
	        number:0  
   		}; 

   		var updateCreative = {
    	name:"",
    	description:"",
        Id: 0
    	};       

		$scope.editChapter = function(item){			
        	 $scope.selectedchapter = item;
        }

     	$scope.deleteChapter = function(item){			
        	var data = item;
        	var result = $window.confirm("Are you absolutely sure you want to delete?");
        	if(result){
        		postData(data, 'chapters/delete');
        	}        	   	
        };

 		creativeService.getCreative(creativeId).then(function (results) {
            $scope.creative = results.data;
            $scope.chapters = creativeService.sortChapters(results.data); 
            $scope.tags = $scope.creative.tags;
            console.log($scope.creative);  
        }, function (error) {
            console.log(error);
        });

        $scope.deleteCreative = function(id){
	        var result = $window.confirm('Are you absolutely sure you want to delete?');
	        if (result) {
	       		creativeService.deleteCreative(id).then(function(results){
	       			$location.path("/home");
	       		});	       	
       			console.log(id);
       		}        
    	}       

		creativeService.getAllTags().then(function(results){
	        $scope.recievedTags = results.data;
		    }, function(error){
		        console.log(error);
		  	});

		$scope.loadTags = function($query) {
       		return $scope.recievedTags.filter(function(tag) {
	        	return tag.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
	      		});
    	};

    	$scope.saveTags = function(){
    		
    		for (var i = 0; i < $scope.tags.length; i++) {
    			$scope.tags[i].creativeId = creativeId;
    		}

    		creativeService.setTags(creativeId,$scope.tags).then(function(results){
    			$scope.creative = results.data;
    			console.log(results.data);
    		});
    	}

		$scope.savePositions = function(){
			postData($scope.chapters,'chapters/all');
		}

   		$scope.saveChapter = function(){    
	        initChapterModel();
	        postData(chapterModel,'chapters');	 	      
	    };

	    $scope.changeCreativeName = function(){
	    	updateCreative.name = $scope.creative.name;
	    	updateCreative.description = $scope.creative.description;
	    	updateCreative.Id = creativeId;
	    	postData(updateCreative,'creatives/update');	 	 
	    };

	    var postData = function(data, link){
	    	 $http.post('http://localhost:57507/api/' + link, JSON.stringify(data), {
	             headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
	    	 .success(function (response) {
	                console.log(response);
	               	            
	            })
	    	 .error(function (err, status) {
	            console.log(err);
	            console.log(status);
	            });
	    };

	     $scope.dropCallback = function(event, index, item, external, type, allowedType) {  
        	$scope.selectedchapter = undefined; 
	       	console.log(index);	 
	       	var startNumber;
	        for (var chapter in $scope.chapters){
	        	if ($scope.chapters[chapter].id === item.id){
	        		startNumber = $scope.chapters[chapter].number;	        		
	        		if ( startNumber > index ) {
	        			$scope.chapters[chapter].number = index + 1;		        	
	        			item.number = index + 1;
	        			for (var i = index; i < startNumber; i++) {	   		
		        			$scope.chapters[i].number++;
		        		}
	        		}	
	        		else if (startNumber < index)
	        		{
	        			console.log("startNumber = " + startNumber + ", index = " + index);
						$scope.chapters[chapter].number = index;		        	
						item.number = index;
					
						for (var i = index - 1; i > startNumber-1; i--) {							        	
			        		$scope.chapters[i].number--;
	        			}
	        		}
	        	}
	        }       
	        return item;     
   		};

   		

	    var initChapterModel = function(){
	    	chapterModel.body = $scope.selectedchapter.body;
	        chapterModel.number = $scope.selectedchapter.number;
	        chapterModel.id = $scope.selectedchapter.id;
	        chapterModel.creativeId = $scope.selectedchapter.creativeId;
	        chapterModel.name = $scope.selectedchapter.name;
	    };

   		
}]);