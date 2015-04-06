/// <reference path="D:\Projects\asp-net-identity\src\AspNetIdentity.WebApi\bower_components/angular/angular.js" />
/// <reference path="D:\Projects\asp-net-identity\src\AspNetIdentity.WebApi\bower_components/angular-route/angular-route.js" />

angular.module('demoApp', ['ngRoute'/*,'ngAnimate'*/])
.controller('WelcomeController', function ($scope) {
})

.controller('LoginController', function ($scope) {
    $scope.loginParams = {
        username: undefined,
        password: undefined
    };
})

.controller('RegisterController', function ($scope) {

})

.config(function ($routeProvider, $locationProvider) {
    $routeProvider
    .when('/login', {
        templateUrl: 'login.html',
        controller: 'LoginController'
    })
    .when('/register', {
        templateUrl: 'register.html',
        controller: 'RegisterController'
    })
    .when('/welcome', {
        templateUrl: 'welcome.html',
        controller: 'WelcomeController'
    })
    .otherwise({ redirectTo: '/welcome' });
});