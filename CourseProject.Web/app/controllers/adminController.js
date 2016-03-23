'use strict';
app.controller('adminController', ['$http','$scope', '$location', 'authService', function ($http, $scope, $location, authService) {

    $scope.authentication = authService.authentication;
    $scope.tags = [
    { text: 'Tag1' },
    { text: 'Tag2' },
    { text: 'Tag3' }
  ];
   
  $scope.loadTags = function(query) {
    return $http.get('tags.json');
  };




}]);