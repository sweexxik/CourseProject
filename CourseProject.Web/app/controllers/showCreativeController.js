'use strict';
app.controller('showCreativeController', ['$scope', '$location','$timeout','$routeParams','authService','creativeService',
    function ($scope, $location,$timeout,$routeParams, authService,creativeService) {
    
    var creativeId = $routeParams.Id;   
    $scope.creative = [];   
    $scope.chapters = []; 

    creativeService.getCreative(creativeId).then(function (results) {
        $scope.creative = results.data;
        $scope.chapters = creativeService.sortChapters(results.data); 
        console.log($scope.creative);  
        console.log($scope.chapters);       
        }, function (error) {
            console.log(error);
        });

}]);