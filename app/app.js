/**
 * Created by danny_000 on 3/22/14.
 */



var app = angular.module('psp', ['ngResource', 'ngSanitize', 'ngRoute', 'service.auth'])

.config(function($routeProvider, $httpProvider){

    //This transformRequest is a global override for $http.post that transforms the body to the same param format used by  jQuery's $.post call
    $httpProvider.defaults.transformRequest = function(data){
        if (data === undefined) {
            return data;
        }
        return $.param(data);
    }

    //sets the content type header globally for $http calls
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';

    $routeProvider
        .when('/', {
            templateUrl: 'dashboard/main.html',
            controller: 'MainCtrl'
        })
        .when('/login', {
            templateUrl: 'components/security/login.html',
            controller: 'LoginCtrl'
        })
        .when('/dashboard', {
            templateUrl: 'dashboard/dashboard.html',
            controller: 'DashboardCtrl'
        })
        .when('/reports', {
            templateUrl: 'reports/reports-index.html',
            controller: 'ReportsCtrl'
        })
        .when('/gsrreport', {
            templateUrl: 'reports/gsr.html',
            controller: 'reports.GSRCtrl'
        })
        .when('/forms', {
            templateUrl: 'forms/forms-index.html',
            controller: 'FormsCtrl'
        })
        .when('/claims', {
            templateUrl: 'forms/claims.html',
            controller: 'forms.ClaimsCtrl'
        })
        .when('/users', {
            templateUrl: 'settings/users.html',
            controller: 'settings.UsersCtrl'
        })
        .when('/sites', {
            templateUrl: 'settings/sites.html',
            controller: 'settings.SitesCtrl'
        })
        .when('/notifications', {
            templateUrl: 'settings/notifications.html',
            controller: 'settings.NotificationsCtrl'
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
.run(function($rootScope, $location, AuthService){

        $rootScope.isVisible = true;

    var routesThatDontRequireAuth = ['/login'];

    var routeClean = function(route){
        return _.find(routesThatDontRequireAuth,
        function(noAuthRoute){
            return (route == noAuthRoute);
        });
    };

    $rootScope.$on('$locationChangeStart', function(event, next, current){
        if(!routeClean($location.url()) && !AuthService.isAuthenticated()){
            $location.path('/login');
        }
    });

});