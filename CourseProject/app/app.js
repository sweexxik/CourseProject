
var app = angular.module('AngularAuthApp',
    ['ui.bootstrap','ng-showdown','ngSanitize','ui.sortable','ngRoute','angular-markdown-editable',
     'ngTagsInput', 'LocalStorageModule', 'angular-loading-bar', 'dndLists','ngFileUpload']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/newCreative", {
        controller: "newCreativeController",
        templateUrl: "/app/views/newCreative.html"
    });

    $routeProvider.when("/main", {
        controller: "mainController",
        templateUrl: "/app/views/main.html"
    });    

    $routeProvider.when("/edit/:Id", {
        controller: "editCreativeController",
        templateUrl: "/app/views/editCreative.html"
    });

    $routeProvider.when("/show/:Id", {
        controller: "showCreativeController",
        templateUrl: "/app/views/showCreative.html"
    });

    $routeProvider.when("/comments/:creativeId", {
        controller: "commentsController",
        templateUrl: "/app/views/comments.html"
    });
    
    $routeProvider.when("/profile/", {
        controller: "profileController",
        templateUrl: "/app/views/profile.html"
    });

    $routeProvider.when("/admin", {
        controller: "adminController",
        templateUrl: "/app/views/admin.html"
    });

    $routeProvider.when("/chapters/:creativeId/:chapterId", {
        controller: "chaptersController",
        templateUrl: "/app/views/chapters.html"
    });

     $routeProvider.when("/search/", {
        controller: "searchController",
        templateUrl: "/app/views/search.html"
    });   

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = 'http://localhost:57507/';
//var serviceBase = 'http://courseprojectapi20160324093711.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


