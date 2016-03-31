'use strict';
app.controller('adminController', ['$http','$scope', '$location', 'authService','adminService', 'creativeService', '$window',
 function ($http, $scope, $location, authService, adminService,creativeService, $window) {

 	
	$scope.sortReverse = false; 
  	$scope.savedSuccessfully = true;
  	$scope.showLoading = false;

  	$scope.newUserName = '';
  	$scope.newUserPassword = '';

  	$scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

     $scope.userInfo = {};  
	 $scope.isAdmin = false;

 	$scope.message = '';
 	$scope.message1 = '';

 	$scope.searchUsers = '';   
  	$scope.searchCreatives = ''; 
  	$scope.newPassword = '';
  	$scope.sortType = 'name'; 

 	$scope.users = []; 
 	$scope.medals = [];
 	$scope.medalsModel = [];

 	$scope.selectedUser = {}; 

  	$scope.resetPasswrodModel = {
  		userId:"",
  		newPassword:""
  	};

  	if (!authService.authentication.isAuth){
  		$location.path('/home');
  	}
  	else {
  		authService.getProfileInfo().then(function(results){
	        $scope.userInfo = results.data;       
	        $scope.isAdmin = $scope.userInfo.isAdmin;
	        
	        if(!$scope.isAdmin){
	        	$location.path('/home');
	        }   
	        else{
	        	adminService.getAllUsers().then(function(results){
					$scope.users = results.data;	
					console.log($scope.users);
				});

				adminService.getAllMedals().then(function(results){		
					$scope.medals = results.data;	
					console.log($scope.medals);
				});
	        }
    	}, function(error){
    	console.log(error);
    	});	        
  	}
	

	$scope.editUser = function(id){
		$scope.message = '';
		$scope.message1 = '';
		$scope.newPassword = '';
		for (var i = 0; i < $scope.users.length; i++) {
			if($scope.users[i].id === id){
				$scope.selectedUser = $scope.users[i];
				break;
			}
		}
		$( globalModal ).toggleClass('global-modal-show');
	};	

	$scope.saveUserData = function(){	
		$scope.showLoading = true;	
		adminService.saveUserData($scope.selectedUser).then(function(results){
			$scope.users = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Saved successfully"
		}, function(error){
			$scope.savedSuccessfully = false;
			$scope.showLoading = false;	
			$scope.message = error.data.message;
		});
	};

	$scope.deleteUser = function(id){
		var result = $window.confirm('Are you absolutely sure you want to delete?');
        if (result) {
        	$scope.showLoading = true;	
            adminService.deleteUser($scope.selectedUser).then(function(results){
            $scope.users = results.data;
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

	$scope.resetPassword = function(){
		$scope.showLoading = true;
		$scope.resetPasswrodModel.userId = $scope.selectedUser.id;
		$scope.resetPasswrodModel.newPassword = $scope.newPassword;
		adminService.resetPassword($scope.resetPasswrodModel).then(function(results){
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message = "Password reset succcessfull"
		},function(error){
			$scope.savedSuccessfully = false;
			$scope.message = error.data.message;
			$scope.showLoading = false;	
		});
	};

	$scope.createNewUser = function(){
		$scope.registration.userName = $scope.newUserName;
		$scope.registration.password = $scope.newUserPassword;
		$scope.registration.confirmPassword = $scope.newUserPassword;
		
		adminService.registerNewUser($scope.registration).then(function(results){			
			$scope.users = results.data;
			$scope.savedSuccessfully = true;
			$scope.showLoading = false;	
			$scope.message1 = "User created succcessfull"
		},function(error){
			$scope.savedSuccessfully = false;
			$scope.message = error.data.message;
			$scope.showLoading = false;	
		});
	};

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