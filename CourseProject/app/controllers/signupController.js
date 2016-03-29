'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    $scope.myHtml = 'basic text sample';
    console.log($scope.myHtml);

    $scope.signUp = function () {
        authService.saveRegistration($scope.registration).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = $scope.translation.REG_SUCC;
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = $scope.translation.REG_ERR + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    };


   function fontSelected(e){
    var select = e.target;
    if (select.selectedIndex > 0) { // web font
        var fontID = select.options[select.selectedIndex].value;
        if (!document.getElementById(fontID)) {
            var head = document.getElementsByTagName('head')[0];
            var link = document.createElement('link');
            link.id = fontID;
            link.rel = 'stylesheet';
            link.type = 'text/css';
            link.href = 'http://fonts.googleapis.com/css?family='+fontID;
            link.media = 'all';
            head.appendChild(link);
        }
        document.getElementById("theText").style.fontFamily = select.options[select.selectedIndex].innerHTML;
    }else{ // default browser font
        document.getElementById("theText").style.fontFamily = null;
    }
}

}]);