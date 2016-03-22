'use strict';
app.controller('profileController', ['$http','$scope', '$location', 'authService','$timeout','Upload', function ($http,$scope, $location, authService, $timeout,Upload) {
  
    $scope.authentication = authService.authentication;
    $scope.userInfo = {};
    $scope.newUserInfo = undefined;
    $scope.flagHide = false;

    console.log($scope.authentication);

    $scope.profileInfo = authService.getProfileInfo().then(function(results){
        $scope.userInfo = results.data;
        console.log($scope.userInfo);

    });

   

    $scope.initNewUserInfo = function () {
        $scope.newUserInfo = $.extend({}, $scope.userInfo);
        console.log($scope.newUserInfo);
        $scope.userInfo.firstName = undefined;
        $scope.userInfo.lastName = undefined;
        $scope.userInfo.email = undefined;
        $scope.userInfo.phoneNumber = undefined;
        console.log($scope.newUserInfo);


    }
    $scope.saveUserInfo = function () {
        $scope.newUserInfo.userName = $scope.userInfo.userName;
        authService.saveUserInfo($scope.newUserInfo).then(function(results){
            $scope.userInfo = results.data;
            $scope.newUserInfo = undefined;
            });
       
    };


    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });
    $scope.$watch('file', function () {
        if ($scope.file != null) {
            $scope.files = [$scope.file]; 
        }
    });
    $scope.log = '';

    $scope.upload = function (files) {
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
              var file = files[i];
              if (!file.$error) {
                Upload.upload({
                    url: 'http://localhost:57507/api/upload/',
                    data: {
                      username: $scope.authentication.userName,
                      file: file  
                    }
                }).then(function (resp) {
                    $timeout(function() {
                        $scope.log = 'file: ' +
                        resp.config.data.file.name +
                       // ', Response: ' + JSON.stringify(resp.data) +
                        '\n' + $scope.log;
                    });
                }, null, function (evt) {
                    var progressPercentage = parseInt(100.0 *
                            evt.loaded / evt.total);
                    $scope.log = 'progress: ' + progressPercentage + 
                        '% ' + evt.config.data.file.name + '\n' + 
                      $scope.log;
                });
              }
            }
        }
    };






}]);