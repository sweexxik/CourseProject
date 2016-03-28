'use strict';
app.controller('adminCreativesController',  ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 $scope.creatives = [];
 $scope.categories = [];
 $scope.selectedCategory = {};
 $scope.newChapter = '';
 $scope.showLoading = false;
 $scope.savedSuccessfully = true;
 $scope.message = '';	
 
$scope.editCreative = function(id){
		console.log(id);
		$scope.message = '';
		for (var i = 0; i < $scope.creatives.length; i++) {
			if($scope.creatives[i].id === id){
				$scope.selectedCreative = $scope.creatives[i];

				break;
			}
		}
		$( globalModal).toggleClass('global-modal-show');
	};

	$scope.saveCreative = function(){
		$scope.selectedCreative.category = $scope.selectedCategory;
		var result = $window.confirm('Are you sure ?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.updateCreativeByAdmin($scope.selectedCreative).then(function(results){
            $scope.creatives = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Saved successfully"
			}, function(error){
				$scope.savedSuccessfully = false;
				$scope.message = error.data.message;
				$scope.showLoading = false;	
			});
        }        
	};

	$scope.deleteChapter = function(id){
		for (var i = 0; i < $scope.selectedCreative.chapters.length; i++) {
			if ($scope.selectedCreative.chapters[i].id == id){
				$scope.selectedCreative.chapters.splice(i,1);

			}
		}
		console.log($scope.selectedCreative);
	}

	$scope.addChapter = function(){
		if($scope.newChapter.length > 3){
			$scope.message = '';
			$scope.selectedCreative.chapters.push({id:0,name:$scope.newChapter,creativeId:$scope.selectedCreative.id});
		}	
		else{
			 $scope.savedSuccessfully = false;
			 $scope.message = "Name must be more than 3 characters!"
		}
	};

	$scope.deleteCreative = function(){
		
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            creativeService.deleteCreativeByAdmin($scope.selectedCreative.id).then(function(results){
            $scope.creatives = results.data;
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

	creativeService.getAllCreatives().then(function(results){
		$scope.creatives = results.data;
		console.log(results.data);
	});

	creativeService.getCategories().then(function(results){
		$scope.categories = results.data;
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