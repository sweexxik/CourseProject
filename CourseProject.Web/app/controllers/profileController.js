'use strict';
app.controller('profileController', ['$anchorScroll','$http','$scope', '$location', 'authService','$timeout','Upload', function ($anchorScroll,$http,$scope, $location, authService, $timeout,Upload) {
  
    $scope.authentication = authService.authentication;
    $scope.userInfo = {};
    $scope.newUserInfo = {};
    $scope.flagHide = false;
    $scope.progressPercentage = 0;
    $scope.showUpload = "";

    var newData = {
        firstName:"",
        lastName: "",      
        userName:"",
        email:"",
        OldPassword:"",
        NewPassword:"",
        ConfirmPassword:""   
    };


     authService.getProfileInfo().then(function(results){
        $scope.userInfo = results.data;
          console.log( $scope.userInfo);
    });

   $scope.scrollTo = function (id) {
        $scope.showUpload = "show";    
        $anchorScroll(id);  
    
    };

    $scope.initNewUserInfo = function () {
        $scope.newUserInfo = $.extend({}, $scope.userInfo);
    };
    
    $scope.saveUserInfo = function () {
        newData.userName = $scope.userInfo.userName;
        newData.firstName = $scope.userInfo.firstName;
        newData.lastName = $scope.userInfo.lastName;
        newData.email = $scope.userInfo.email;
        newData.OldPassword = $scope.newUserInfo.oldPassword;
        newData.NewPassword = $scope.newUserInfo.newPassword;
        newData.ConfirmPassword = $scope.newUserInfo.passConfirm;

       
        authService.saveUserInfo(newData).then(function(results){            
          $scope.userInfo = results.data;       
        });
      };


      $scope.updateAvatar = function(){
          $scope.upload($scope.file);
      };

       


    //$scope.upload($scope.files);

    $scope.$watch('file', function () {
       console.log($scope.file);

    });
  

    $scope.log = '';

    $scope.upload = function (file) {
        console.log("Upload starts");
            console.log(file);
           
              if (!file.$error) {
                Upload.upload({
                    url: 'http://localhost:57507/api/upload/',
                    data: {
                      username: $scope.authentication.userName,
                      file: file  
                    }
                }).then(function (resp) {
                    console.log(resp);
                    $timeout(function() {
                        $scope.log = 'file: ' +
                        resp.config.data.file.name +                     
                        '\n' + $scope.log;
                         $scope.userInfo = resp.data;
                         $scope.showUpload = "hide";
                    });
                }, null, function (evt) {

                     $scope.progressPercentage = parseInt(100.0 *
                            evt.loaded / evt.total);
                    $scope.log = 'progress: ' +  $scope.progressPercentage + 
                        '% ' + evt.config.data.file.name + '\n' + 
                      $scope.log;
                                       console.log($scope.progressPercentage);
                      
                });
              }
            
        
    };






}]);