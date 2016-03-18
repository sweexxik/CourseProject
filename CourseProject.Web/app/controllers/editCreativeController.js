'use strict';
app.controller('editCreativeController', ['$scope','$routeParams','creativeService',
	function ($scope, $routeParams,creativeService) {
 		
 		$scope.creative = []; 	
 		$scope.chapters = []; 			
 		
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
		    return a;
	  
		}

		$scope.click = function(id){
        	 $scope.selectedchapter = $scope.creative.chapters[id];
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
	        console.log(item);
	        console.log(index);	        
	        return item;     
   		};
}]);