'use strict';
app.controller('homeController',
 ['$http','$scope','creativeService', 'localStorageService', '$location','authService',
  function ($http,$scope,creativeService, localStorageService, $location, authService) {

 	$scope.authentication = authService.authentication;
    $scope.creatives = [];
    $scope.show = false;
    $scope.showAddForm = false;
    $scope.chapterModel = {
    	body:"",
        creativeId:0,    	
    	id:0,
        name:"",
        number:0  
    };

    $scope.creativeId = "";


    if(authService.authentication.isAuth){
    	 creativeService.getCreatives().then(function (results) {

            $scope.creatives = results.data;
      		console.log(results.data);
       
        }, function (error) {
            console.log(error);
        });
    }
   

    $scope.getThing = function(id,index){
    	$scope.creativeId = id;
    	console.log(index);
    	var selectedchapter = $scope.creatives[id];     	
    	$scope.selectedchapter = selectedchapter.chapters[index];
    	console.log($scope.selectedchapter);    
    		$scope.show = true;
    }

    $scope.newCreative = function(){
    	$location.path('/newCreative');
    }

    $scope.saveChapter = function(){
    
        $scope.chapterModel.body = $scope.selectedchapter.body;
        $scope.chapterModel.number = $scope.selectedchapter.number;
        $scope.chapterModel.id = $scope.selectedchapter.id;
        $scope.chapterModel.creativeId = $scope.selectedchapter.creativeId;
        $scope.chapterModel.name = $scope.selectedchapter.name;

        var data = $scope.chapterModel;
        console.log(data);
        $http.post('http://localhost:57507/api/chapters', JSON.stringify(data), {
             headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } }).success(function (response) {
                    console.log(response);
                    if (response.status == 200) $scope.show = false;
            }).error(function (err, status) {
            console.log(err);
            console.log(status);
            });
    };


     $scope.dragoverCallback = function(event, index, external, type) {
        $scope.logListEvent('dragged over', event, index, external, type);
        // Disallow dropping in the third row. Could also be done with dnd-disable-if.
        return index < 10;
    };

    $scope.dropCallback = function(event, index, item, external, type, allowedType) {
        $scope.logListEvent('dropped at', event, index, external, type);
        if (external) {
            if (allowedType === 'itemType' && !item.label) return false;
            if (allowedType === 'containerType' && !angular.isArray(item)) return false;
        }
        return item;
    };

    $scope.logEvent = function(message, event) {
        console.log(message, '(triggered by the following', event.type, 'event)');
        console.log(event);
    };

    $scope.logListEvent = function(action, event, index, external, type) {
        var message = external ? 'External ' : '';
        message += type + ' element is ' + action + ' position ' + index;
        $scope.logEvent(message, event);
    };

    $scope.model = [];

    // Initialize model
    var id = 10;
    for (var i = 0; i < 3; ++i) {
        $scope.model.push([]);
        for (var j = 0; j < 2; ++j) {
            $scope.model[i].push([]);
            for (var k = 0; k < 7; ++k) {
                $scope.model[i][j].push({label: 'Item ' + id++});
            }
        }
    }

    $scope.$watch('model', function(model) {
        $scope.modelAsJson = angular.toJson(model, true);
    }, true);

    $scope.containers = { "one":"one","one":"one","one":"one","one":"one","one":"one","one":"one","one":"one" }


}]);