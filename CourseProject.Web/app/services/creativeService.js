'use strict';
app.factory('creativeService', ['$http', 'ngAuthSettings','localStorageService', function ($http, ngAuthSettings,localStorageService) {

 	var serviceBase = ngAuthSettings.apiServiceBaseUri;
   
    var creativesServiceFactory = {};

    var _getCreatives = function () {
    	var currentUserName = localStorageService.get('authorizationData').userName;
        return $http.get(serviceBase + 'api/creatives/getall/' + currentUserName)
            .then(function (results) { return results; });
    };

    var _getAllCreatives = function () {      
        return $http.get(serviceBase + 'api/creatives/getall')
            .then(function (results) { return results; });
    };

    var _getCreative = function (id) {        
        return $http.get(serviceBase + 'api/creatives/' +id)
            .then(function (results) { return results; });
    };

    var _getCategories = function () {
        return $http.get(serviceBase + 'api/categories')
            .then(function (results) { return results; });
    };

    var _getAllTags = function () {
        return $http.get(serviceBase + 'api/tags')
            .then(function (results) { return results; });
    };

    var _getCreativeTags = function (creativeId) {
        return $http.get(serviceBase + 'api/tags/' + creativeId )
            .then(function (results) { return results; });
    };


    var _getComments = function(id){
        return $http.get(serviceBase + 'api/comments/' + id)
        .then(function(results){ return results;});
    };

    var _createCreative = function(data) {
        return $http.post(serviceBase + '/api/creatives', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

     var _createComment = function(data) {       
        return $http.post(serviceBase + '/api/comments', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _createLike = function(data) {       
        return $http.post(serviceBase + '/api/likes', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _createRating = function(data) {       
        return $http.post(serviceBase + '/api/rating', JSON.stringify(data), {
            headers: { contentType: 'application/json; charset=utf-8', dataType: "json" } })
            .then(function (results) { return results; });
    };

    var _setTags = function(id, data){
        return $http.post(serviceBase + '/api/tags/'+ id, JSON.stringify(data),{
        headers: { contentType: 'application/json; charset=utf-8', dataType: "json" }    
        }).then(function(results){
            return results;
        });
    };


    var _deleteCreative = function (id) {   
        return $http.post(serviceBase + 'api/creatives/delete/' + id)
            .then(function (results) { return results; });
    };

    var _deleteComment = function (id) {   
        return $http.post(serviceBase + 'api/comments/delete/' + id)
            .then(function (results) { return results; });
    };

    var _sortChapters = function(creatives) {
            var sorted = {},
            a = [];     
            for (var chapter in creatives.chapters) {               
                if (creatives.chapters.hasOwnProperty(chapter)) {
                    a.push(creatives.chapters[chapter]);            
                }
            }          
            a.sort(function(x,y){               
                return x.number - y.number;
            });   
            return a;}

    creativesServiceFactory.getAllCreatives = _getAllCreatives
    creativesServiceFactory.getCreatives = _getCreatives;
    creativesServiceFactory.getCreative = _getCreative;
    creativesServiceFactory.getCategories = _getCategories;
    creativesServiceFactory.getComments = _getComments;
    creativesServiceFactory.getAllTags = _getAllTags;
    creativesServiceFactory.getCreativeTags = _getCreativeTags

    creativesServiceFactory.setTags = _setTags;

    creativesServiceFactory.createCreative = _createCreative;
    creativesServiceFactory.createComment = _createComment;
    creativesServiceFactory.createLike = _createLike;
    creativesServiceFactory.createRating = _createRating;

    creativesServiceFactory.deleteCreative = _deleteCreative;
    creativesServiceFactory.deleteComment = _deleteComment;
        
    creativesServiceFactory.sortChapters = _sortChapters;

    return creativesServiceFactory;

}]);