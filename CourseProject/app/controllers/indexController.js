'use strict';
app.controller('indexController', ['$http', '$scope', '$location', 'authService', 'searchService', 'translationService',
    function ($http, $scope, $location, authService, searchService, translationService) {

        $scope.currentTheme = 'lumen';
        $scope.pattern = '';
        $scope.selectedLanguage = 'en';
        $scope.authentication = authService.authentication;

        $scope.translate = function () {
            translationService.getTranslation($scope, $scope.selectedLanguage);
        };
        
        $scope.translate();

        $scope.logOut = function() {
            authService.logOut();
            $location.path("/home");
        };

        $scope.changeTheme = function() {
            if ($scope.currentTheme == 'cyborg') {
                $scope.currentTheme = 'lumen';
            } else {
                $scope.currentTheme = 'cyborg';
            }
        };

        $scope.search = function() {
            console.log($scope.pattern);
            searchService.setSearchPattern($scope.pattern);
            $location.path('/search');
        };  
        
        $scope.setEN = function() {
            $scope.selectedLanguage = 'en';
            $scope.translate();
        };

        $scope.setRU = function () {
            $scope.selectedLanguage = 'ru';
            $scope.translate();
        };

    }]);