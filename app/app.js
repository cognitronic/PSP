/**
 * Created by Danny Schreiber on 3/22/14.
 */

var app = angular.module('psp', [
        'ngResource',
        'ngSanitize',
        'ngRoute',
        'ngAnimate',
        'service.auth',
        'service.user',
        'service.site',
        'ui.bootstrap',
        'service.notifications',
        'service.reports'])

.config(function($routeProvider, $httpProvider){

        var access = routingAccessConfig.accessLevels;
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
            controller: 'MainCtrl',
            access: access.anon
        })
        .when('/login', {
            templateUrl: 'components/security/login.html',
            controller: 'LoginCtrl',
            access: access.anon
        })
        .when('/dashboard', {
            templateUrl: 'dashboard/dashboard.html',
            controller: 'DashboardCtrl',
            access: access.anon
        })
        .when('/reports', {
            templateUrl: 'reports/reports-index.html',
            controller: 'ReportsCtrl',
            access: access.anon
        })
        .when('/gsrreport', {
            templateUrl: 'reports/gsr.html',
            controller: 'reports.GSRCtrl',
            access: access.executive
        })
        .when('/customerregistrations', {
            templateUrl: 'reports/customerregistrations.html',
            controller: 'reports.CustomerRegistrationsCtrl',
            access: access.office
        })
        .when('/customerregistrations/:id', {
            templateUrl: 'reports/customerregistration.html',
            controller: 'reports.CustomerRegistrationCtrl',
            access: access.office
        })
        .when('/birthdays', {
            templateUrl: 'reports/birthdays.html',
            controller: 'reports.BirthdaysCtrl',
            access: access.office
        })
        .when('/forms', {
            templateUrl: 'forms/forms-index.html',
            controller: 'FormsCtrl',
            access: access.anon
        })
        .when('/claims', {
            templateUrl: 'forms/claims.html',
            controller: 'forms.ClaimsCtrl',
            access: access.anon
        })
        .when('/users', {
            templateUrl: 'settings/users.html',
            controller: 'settings.UsersCtrl',
            access: access.admin
        })
        .when('/users/:id', {
            templateUrl: 'settings/user.html',
            controller: 'settings.UserCtrl',
            access: access.admin
        })
        .when('/sites', {
            templateUrl: 'settings/sites.html',
            controller: 'settings.SitesCtrl',
            access: access.admin
        })
        .when('/sites/:id', {
            templateUrl: 'settings/site.html',
            controller: 'settings.SiteCtrl',
            access: access.admin
        })
        .when('/notifications', {
            templateUrl: 'settings/notifications.html',
            controller: 'settings.NotificationsCtrl',
            access: access.admin
        })
        .when('/notifications/:id', {
            templateUrl: 'settings/notification.html',
            controller: 'settings.NotificationCtrl',
            access: access.admin
        })
        .when('/noaccess', {
            templateUrl: 'components/security/no-access.html',
            controller: 'security.NoAccessCtrl',
            access: access.anon
        })
        .otherwise({
            redirectTo: '/',
            access: access.anon
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
.constant('APP_SETTINGS', {
        apiUrl: 'http://pspapi.dannyschreiber.net/'
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

    $rootScope.$on('$routeChangeStart', function(event, currRoute, prevRoute){
        if(!AuthService.authorize(currRoute.access)){
            $location.path('/noaccess');
            console.log('unauthorized');
        }
    });

});