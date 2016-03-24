'use strict';
app.controller('mainController', ['$scope','creativeService', function ($scope,creativeService) {

    $scope.creatives = [];
    creativeService.getAllCreatives().then(function (results) {
            $scope.creatives = results.data;
             for(var i = 0; i< $scope.creatives.length; i++) {             
                $scope.creatives[i].chapters = creativeService.sortChapters($scope.creatives[i]); 
            }   
            console.log(results.data);       
        }, function (error) {
            console.log(error);
        });

}]);