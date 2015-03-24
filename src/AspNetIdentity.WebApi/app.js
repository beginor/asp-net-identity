/// <reference path="D:\Projects\asp-net-identity\src\AspNetIdentity.WebApi\bower_components/angular/angular.js" />
/// <reference path="D:\Projects\asp-net-identity\src\AspNetIdentity.WebApi\bower_components/angular-route/angular-route.js" />

angular.module('demoApp', ['ngRoute'])
.controller('Mai nController', function ($scope) {
})

.controller('LoginController', function ($scope) {

})

.controller('RegisterController', function ($scope) {

})

.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/login', {
        templateUrl: 'login.html',
        controller: 'LoginController'
    })
    .when('/register', {
        templateUrl:
    })
});