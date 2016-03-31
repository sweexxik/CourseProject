'use strict';
app.controller('editCreativeController', ['$scope','$route','$routeParams','creativeService','$window','$location','localStorageService',
	function ($scope, $route, $routeParams,creativeService, $window, $location,localStorageService) {
 		
		var creativeId = $routeParams.Id; 	
		$scope.creativeId = creativeId;
 		$scope.creative = {}; 	
 		$scope.chapters = [];  	
		$scope.recievedTags = [];
		$scope.tags = [];

		$scope.savedSuccessfully = false;
		$scope.showLoading = true;	
		$scope.message = '';

		var currentUserName = '';

		if($scope.authentication.isAuth){
        	currentUserName = localStorageService.get('authorizationData').userName;        
    	}

     	creativeService.getCreative(creativeId).then(function (results) {
 			$scope.showLoading = false;	
            $scope.creative = results.data;
            $scope.chapters = creativeService.sortChapters(results.data); 
            $scope.tags = $scope.creative.tags;
            if($scope.creative.userName !== currentUserName){
            	$location.path('/home');
            }
            console.log($scope.creative);  
        }, function (error) {
            	$scope.savedSuccessfully = false;					
				$scope.message = error.data.message;
				$location.path('/NotFound')
        });

        $scope.deleteCreative = function(){
        	$scope.showLoading = false;	
	        var result = $window.confirm('Are you absolutely sure you want to delete?');
	        if (result) {
	       		creativeService.deleteCreative(creativeId).then(function(results){
	       			$scope.chapters = results.data;   
		        	$scope.savedSuccessfully = true;
					$scope.showLoading = false;	
					$scope.message = "Deleted successfully" 
		            console.log(results.data);  
	       			$location.path("/home");
	       		}, function(error){
	       			$scope.savedSuccessfully = false;
					$scope.showLoading = false;	
					$scope.message = error.data.message;
	       		});	       	

       		}        
    	}       

    	$scope.deleteChapter = function(id){	
        	var result = $window.confirm("Are you absolutely sure you want to delete?");
        	if(result){
	        	creativeService.deleteChapter(id).then(function (results) {     
		        	$scope.chapters = results.data;   
		        	$scope.savedSuccessfully = true;
					$scope.showLoading = false;	
					$scope.message = "Deleted successfully" 
		            console.log(results.data);  
		        }, function (error) {
		          	$scope.savedSuccessfully = false;
					$scope.showLoading = false;	
					$scope.message = error.data.message;
		        });
	        }        	   	
        };

    	$scope.saveCreative = function () {
    		$scope.showLoading = true;
    		setChpatersPositions();
    		$scope.creative.chapters = $scope.chapters;
    		creativeService.updateCreative($scope.creative).then(function(results){
	    		$scope.savedSuccessfully = true;
				$scope.showLoading = false;	
				$scope.message = "Saved successfully" 
		        console.log(results);
			}, function(error){
			    $scope.savedSuccessfully = false;
				$scope.showLoading = false;	
				$scope.message = error.data.message;
			});
    	}

		creativeService.getAllTags().then(function(results){
	        $scope.recievedTags = results.data;
		    }, function(error){
		        $scope.savedSuccessfully = false;
				$scope.showLoading = false;	
				$scope.message = error.data.message;
		  	});

		$scope.loadTags = function($query) {
       		return $scope.recievedTags.filter(function(tag) {
	        	return tag.name.toLowerCase().indexOf($query.toLowerCase()) != -1;
	      		});
    	};

    	// var saveTags = function(){    		
    	// 	for (var i = 0; i < $scope.tags.length; i++) {
    	// 		$scope.tags[i].creativeId = creativeId;
    	// 	}
    	// 	creativeService.setTags(creativeId,$scope.tags).then(function(results){
    	// 		$scope.creative = results.data;
    	// 		console.log(results.data);
    	// 	});
    	// }

		var setChpatersPositions = function(){
			for (var i = 0; i < $scope.chapters.length; i++) {
				$scope.chapters[i].number = i + 1;
			}
		}

   		// $scope.saveChapter = function(){    
	    //     initChapterModel();
	    //     postData(chapterModel,'chapters');	 	      
	    // };

	    // $scope.changeCreativeName = function(){
	    // 	updateCreative.name = $scope.creative.name;
	    // 	updateCreative.description = $scope.creative.description;
	    // 	updateCreative.Id = creativeId;
	    // 	postData(updateCreative,'creatives/update');	 	 
	    // };

	    // var postData = function(data, link){
	    // 	 $http.post(serviceBase + 'api/' + link, JSON.stringify(data), {
	    //          headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
	    // 	 .success(function (response) {
	    //             console.log(response);
	               	            
	    //         })
	    // 	 .error(function (err, status) {
	    //         console.log(err);
	    //         console.log(status);
	    //         });
	    // };

	    //  $scope.dropCallback = function(event, index, item, external, type, allowedType) {  
     //    	$scope.selectedchapter = undefined; 
	    //    	console.log(index);	 
	    //    	var startNumber;
	    //     for (var chapter in $scope.chapters){
	    //     	if ($scope.chapters[chapter].id === item.id){
	    //     		startNumber = $scope.chapters[chapter].number;	        		
	    //     		if ( startNumber > index ) {
	    //     			$scope.chapters[chapter].number = index + 1;		        	
	    //     			item.number = index + 1;
	    //     			for (var i = index; i < startNumber; i++) {	   		
		   //      			$scope.chapters[i].number++;
		   //      		}
	    //     		}	
	    //     		else if (startNumber < index)
	    //     		{
	    //     			console.log("startNumber = " + startNumber + ", index = " + index);
					// 	$scope.chapters[chapter].number = index;		        	
					// 	item.number = index;
					
					// 	for (var i = index - 1; i > startNumber-1; i--) {							        	
			  //       		$scope.chapters[i].number--;
	    //     			}
	    //     		}
	    //     	}
	    //     }       
	    //     return item;     
   		// };

   		

	    // var initChapterModel = function(){
	    // 	chapterModel.body = $scope.selectedchapter.body;
	    //     chapterModel.number = $scope.selectedchapter.number;
	    //     chapterModel.id = $scope.selectedchapter.id;
	    //     chapterModel.creativeId = $scope.selectedchapter.creativeId;
	    //     chapterModel.name = $scope.selectedchapter.name;
	    // };   


  
	 $scope.sortableOptions = {
	  	update: function(e, ui) {
		    var logEntry = $scope.chapters.map(function(i){
		    	return i.number;
		    }).join(', ');
	
	    },
	    stop: function(e, ui) {	   
	    	console.log($scope.chapters);
	      var logEntry = $scope.chapters.map(function(i){
	        return i.number;
	      }).join(', ');	 
	    }
	  };

}]);