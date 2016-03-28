'use strict';
app.controller('adminTagsController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 $scope.tags = [];

 $scope.selectedTag = {};

 $scope.newTag = '';

 $scope.showLoading = false;

 $scope.savedSuccessfully = true;

 $scope.message = '';	
 
	$scope.editTag = function(id){
		console.log(id);
		$scope.message = '';
		for (var i = 0; i < $scope.tags.length; i++) {
			if($scope.tags[i].id === id){
				$scope.selectedTag = $scope.tags[i];
				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};

	$scope.saveTags = function(){
		// $scope.selectedCreative.category = $scope.selectedCategory;
		// var result = $window.confirm('Are you sure ?');
  //       if (result) {
  //       $scope.showLoading = true;	
  //           creativeService.updateCreativeByAdmin($scope.selectedCreative).then(function(results){
  //           $scope.creatives = results.data;
		// 	$scope.savedSuccessfully = true;
		// 	$scope.showLoading = false;	
		// 	$scope.message = "Saved successfully"
		// 	}, function(error){
		// 		$scope.savedSuccessfully = false;
		// 		$scope.message = error.data.message;
		// 		$scope.showLoading = false;	
		// 	});
  //       }        
	};

	$scope.deleteTag = function(id){
		// for (var i = 0; i < $scope.selectedCreative.chapters.length; i++) {
		// 	if ($scope.selectedCreative.chapters[i].id == id){
		// 		$scope.selectedCreative.chapters.splice(i,1);

		// 	}
		// }
		// console.log($scope.selectedCreative);
	}

	$scope.addTag = function(){
		if($scope.newTag.length > 3){
			$scope.selectedTag.name = $scope.newTag;
			$scope.selectedTag.id = 0;
			$scope.message = '';
			creativeService.setTag($scope.selectedTag).then(function(results){
            $scope.tags = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Saved successfully"
			}, function(error){
				$scope.savedSuccessfully = false;
				$scope.message = error.data.message;
				$scope.showLoading = false;	
			});		
		}	
		else{
			 $scope.savedSuccessfully = false;
			 $scope.message = "Name must be more than 3 characters!"
		}
	};

	$scope.deleteTag = function(id){		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.deleteTag(id).then(function(results){
            $scope.tags = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Deleted successfully"
		}, function(error){
			$scope.savedSuccessfully = false;
			$scope.message = error.data.message;
			$scope.showLoading = false;	
		});
	
        }        
	};

	creativeService.getAllTags().then(function(results){
		$scope.tags = results.data;
		console.log(results.data);
	});
	

	$scope.close = function(){
		$( globalModal ).toggleClass('global-modal-show');
	}

	var globalModal = $('.global-modal');
    $( ".trigger" ).on( "click", function(e) {
      e.preventDefault();
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".overlay" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });

}]);