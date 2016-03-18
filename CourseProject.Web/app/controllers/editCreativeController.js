'use strict';
app.controller('editCreativeController', ['$http','$scope','$routeParams','creativeService',
	function ($http, $scope, $routeParams,creativeService) {
 		
 		$scope.creative = []; 	
 		$scope.chapters = []; 
 		$scope.stub = []; 	
 		$scope.chapterModel = {
	    	body:"",
	        creativeId:0,    	
	    	id:0,
	        name:"",
	        number:0  
   		};
        

		$scope.click = function(item){			
        	 $scope.selectedchapter = item;
        	 console.log($scope.selectedchapter);  
        }

 		var creativeId = $routeParams.Id;

 		creativeService.getCreative(creativeId).then(function (results) {
            $scope.creative = results.data;
            $scope.chapters = sortObject(results.data); 
      		console.log(results.data);       
        }, function (error) {
            console.log(error);
        });

        $scope.dropCallback = function(event, index, item, external, type, allowedType) {  
        	$scope.selectedchapter = undefined; 
	       	console.log(index);	 
	       	var startIndex;
	        for (var chapter in $scope.chapters){
	        	if ($scope.chapters[chapter].id === item.id){
	        		console.log("URA");
	        		startIndex = $scope.chapters[chapter].number;
	        		$scope.chapters[chapter].number = index + 1;	
	        	//	$scope.selectedchapter = $scope.chapters[chapter];
	        		item.number = index + 1;   		
	        	}
	        }
	         for (var i = index; i < startIndex; i++) {
	        		console.log(i);
	        		$scope.chapters[i].number++;
	        }
        	        
	        return item;     
   		};
		$scope.newChapter = function(){
			 $scope.selectedchapter = {};
		}

   		$scope.saveChapter = function(){
    
	        initModel();

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


	    var initModel = function(){
	    	$scope.chapterModel.body = $scope.selectedchapter.body;
	        $scope.chapterModel.number = $scope.selectedchapter.number;
	        $scope.chapterModel.id = $scope.selectedchapter.id;
	        $scope.chapterModel.creativeId = $scope.selectedchapter.creativeId;
	        $scope.chapterModel.name = $scope.selectedchapter.name;
	    };

   		var sortObject = function(o) {
		    var sorted = {},
		    a = [];		
		    for (var chapter in o.chapters) {		    	
		        if (o.chapters.hasOwnProperty(chapter)) {
		            a.push(o.chapters[chapter]);	        
		        }
		    }		   
		    a.sort(function(x,y){		    	
		    	return x.number - y.number;
		    });	  
		    return a;}
}]);