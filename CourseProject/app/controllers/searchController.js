'use strict';
app.controller('searchController', ['$http','$scope', '$location','searchService',
    function ($http,$scope, $location, searchService) {
  
  	//todo save theme in coockies
    $scope.results = [];

   
    $scope.pattern = '';  

    $scope.search = function(){ 
	    initSearchModel();    
	    searchService.search($scope.searchModel).then(function(results){
	        $scope.results = results.data;
    	}); 
    };

   $scope.searchModel = {
        pattern:"",
        chapterName:false,
        chapterText:false,
        creativeName:false,
        creativeDescription:false,
        tagName:false,
        commentText:false,
        commentAuthor:false,
        creativeAuthor:false
    }; 

    $scope.conditionList = [
    { name: 'chapterName',    selected: true },
    { name: 'chapterText',   selected: true },
    { name: 'creativeName',     selected: true },
    { name: 'creativeDescription', selected: true },
    { name: 'tagName',    selected: true },
    { name: 'commentText',   selected: true },
    { name: 'commentAuthor',     selected: true },
    { name: 'creativeAuthor', selected: true }
  ];

    $scope.$watch('conditionList|filter:{selected:true}', function (nv) {
        console.log($scope.conditionList);       
      }, true);

    var initSearchModel = function(){
        $scope.searchModel.pattern = $scope.pattern;
        $scope.searchModel.chapterName = $scope.conditionList[0].selected;
        $scope.searchModel.chapterText = $scope.conditionList[1].selected;
        $scope.searchModel.creativeName = $scope.conditionList[2].selected;
        $scope.searchModel.creativeDescription = $scope.conditionList[3].selected;
        $scope.searchModel.tagName = $scope.conditionList[4].selected;
        $scope.searchModel.commentText = $scope.conditionList[5].selected;
        $scope.searchModel.commentAuthor = $scope.conditionList[6].selected;
        $scope.searchModel.creativeAuthor = $scope.conditionList[7].selected;

    };
    
   

}]);