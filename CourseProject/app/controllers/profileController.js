'use strict';
app.controller('profileController', ['ngAuthSettings','$anchorScroll','$http','$scope', '$location', 'authService','$timeout','Upload',
   function (ngAuthSettings,$anchorScroll,$http,$scope, $location, authService, $timeout,Upload) {
  
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    $scope.authentication = authService.authentication;
    $scope.userInfo = {};
    $scope.newUserInfo = {};
    $scope.flagHide = false;
    $scope.progressPercentage = 0;
    $scope.showUpload = "";
    $scope.isAdmin = false;
    $scope.showLoading = false;
    $scope.savedSuccessfully = false;
    $scope.message = '';

   var newData = {
      OldPassword:"",
      NewPassword:"",
      ConfirmPassword:"",
      userName:""
    };

    authService.getProfileInfo().then(function(results){
        $scope.userInfo = results.data;       
        $scope.isAdmin = $scope.userInfo.isAdmin;
        console.log($scope.userInfo);        
    });

   $scope.scrollTo = function (id) {
        $scope.showUpload = "show";    
        $anchorScroll(id);      
    };

    $scope.saveUserInfo = function () {
        $scope.showLoading = true;  
        authService.saveUserInfo($scope.userInfo).then(function(results){  
            $scope.savedSuccessfully = true;
            $scope.showLoading = false; 
            $scope.message = "Saved successfully."         
            $scope.userInfo = results.data;       
        }, function(error){
              $scope.savedSuccessfully = false;
              $scope.showLoading = false; 
              $scope.message = error.data.message;
            });
      };

      $scope.changePassword = function(){
        $scope.showLoading = true;  
        newData.OldPassword = $scope.newUserInfo.oldPassword;
        newData.NewPassword = $scope.newUserInfo.newPassword;
        newData.ConfirmPassword = $scope.newUserInfo.passConfirm;
        newData.userName = $scope.userInfo.userName;

        authService.changePassword(newData).then(function(results){
            $scope.savedSuccessfully = true;
            $scope.showLoading = false; 
            $scope.message = "Password changed successfully."         
            $scope.userInfo = results.data;       
        }, function(error){
              $scope.savedSuccessfully = false;
              $scope.showLoading = false; 
              $scope.message = error.data.message;
            });
      };

   var reader = new FileReader();
      $scope.updateAvatar = function(){
           $scope.upload($scope.file);
       
    };
    
    $scope.$watch('file', function () {      
       // console.log($scope.file);
    });
  

    $scope.log = '';

    $scope.upload = function (file) {               
              if (!file.$error) {
                $scope.showLoading = true;
                Upload.upload({
                    url: serviceBase + 'api/upload/',
                    data: {
                      username: $scope.authentication.userName,
                      file: file  
                    }
                }).then(function (resp) {                    
                    $timeout(function() {                      
                      $scope.userInfo = resp.data;
                      $scope.showLoading = false;
                      $('.global-modal').toggleClass('global-modal-show');                     
                    });
                }, null, function (evt) {
                    $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);    
                });
          }
    };


       var globalModal = $('.global-modal');
    $( ".btn-green-flat-trigger" ).on( "click", function(e) {
      e.preventDefault();
      $( globalModal ).toggleClass('global-modal-show');
    });
    $( ".overlay" ).on( "click", function() {
      $( globalModal ).toggleClass('global-modal-show');
    });



}]);