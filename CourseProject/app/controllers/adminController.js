'use strict';
app.controller('adminController', ['$http','$scope', '$location', 'authService','adminService',
 function ($http, $scope, $location, authService, adminService) {

 	$scope.users = []; 
 	$scope.medals = [];
 	$scope.selectedUser = {};
 	$scope.sortType = 'name'; 

  	$scope.sortReverse = false;  

  	$scope.searchUsers = '';   

  

  	$scope.medalsModel = [];



	adminService.getAllUsers().then(function(results){
		$scope.users = results.data;	
		console.log($scope.users);
	});

	adminService.getAllMedals().then(function(results){		
		$scope.medals = results.data;	
		console.log($scope.medals);
	});

	$scope.editUser = function(index){
		$scope.selectedUser = $scope.users[index];
		  $( globalModal ).toggleClass('global-modal-show');
	};

	$scope.saveUserData = function(){		
		adminService.saveUserData($scope.selectedUser).then(function(results){
			$scope.users = results.data;
			console.log(results.data);
		}, function(error){
			console.log(error);
		});
	};



   var globalModal = $('.global-modal');
    $( ".trigger" ).on( "click", function(e) {
      e.preventDefault();
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".overlay" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".global-modal_close" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });
    $(".mobile-close").on("click", function(){
      $( globalModal ).toggleClass('global-modal-show');
    });



























	$scope.show1 = true;
	$scope.show2 = true;
	$scope.show3 = true;
	$scope.show4 = true;
	$scope.show5 = true;
	$scope.show = function(id){
		switch(id)
		{
			case 1: {
				console.log(id);
				$scope.show1 = false;	
				$scope.show2 = true;
				$scope.show3 = true;
				$scope.show4 = true;
				$scope.show5 = true;			
			};
			break;
			case 2: {
				$scope.show1 = true;	
				$scope.show2 = false;
				$scope.show3 = true;
				$scope.show4 = true;
				$scope.show5 = true;	
			};
			break;
			case 3 : {
				$scope.show1 = true;	
				$scope.show2 = true;
				$scope.show3 = false;
				$scope.show4 = true;
				$scope.show5 = true;	
				
			};
			break;
			case 4 : {
				$scope.show1 = true;	
				$scope.show2 = true;
				$scope.show3 = true;
				$scope.show4 = false;
				$scope.show5 = true;				
			};
			break;
			case 5 : {
				$scope.show1 = true;	
				$scope.show2 = true;
				$scope.show3 = true;
				$scope.show4 = true;
				$scope.show5 = false;
				
			};
			break;
			default:
				break;
		};
		
	};




}]);