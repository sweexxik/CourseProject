
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'dndLists']);

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

    // $routeProvider.when("/refresh", {
    //     controller: "refreshController",
    //     templateUrl: "/app/views/refresh.html"
    // });

    // $routeprovider.when("/drag", {
    //     controller: "homeController",
    //     templateurl: "/app/views/drag.html"
    // });

    $routeProvider.when("/drag", {
        controller: "dragController",
        templateUrl: "/app/views/drag.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

//var serviceBase = 'http://localhost:26264/';
var serviceBase = 'http://localhost:57507/';
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


