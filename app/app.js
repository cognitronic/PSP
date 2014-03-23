/**
 * Created by danny_000 on 3/22/14.
 */
var app = angular.module('psp', ['ngResource', 'ngSanitize', 'ngRoute', 'service.auth'])

.config(function($routeProvider, $httpProvider){
    $routeProvider
        .when('/', {
            templateUrl: 'dashboard/main.html',
            controller: 'MainCtrl'
        })
        .when('/login', {
            templateUrl: 'components/security/login.html',
            controller: 'LoginCtrl'
        })
        .otherwise({
            redirectTo: '/'
    });
    $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];

})

.constant('AUTH_EVENTS', {
    loginSuccess: 'auth-login-success',
    loginFailed: 'auth-login-failed',
    logoutSuccess: 'auth-logout-success',
    sessionTimeout: 'auth-session-timeout',
    notAuthenticated: 'auth-not-authenticated',
    notAuthorized: 'auth-not-authorized'
})
 .constant('USER_ROLES',{
    admin: 'admin',
    executive: 'executive',
    sitemanager: 'sitemanager',
    office: 'office'
})
.constant('APP_SETTINGS', {
        apiUrl: 'http://pspapi.localhost/'
    })