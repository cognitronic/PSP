/**
 * Created by Danny Schreiber on 3/22/14.
 */

var ramAngularApp = ramAngularApp || {};
ramAngularApp.module = angular.module('psp', [
        'ngResource',
        'ngSanitize',
        'ngRoute',
        'ngAnimate',
        'service.auth',
        'service.user',
		'toaster',
        'service.site',
        'ui.bootstrap',
        'service.notifications',
        'service.reports',
        'filters.app',
        'dialogs.services',
        'highcharts-ng',
        'cc.widgets.position']);

ramAngularApp.module.config(function($routeProvider, $httpProvider, dialogsProvider){

        var access = routingAccessConfig.accessLevels;
    //This transformRequest is a global override for $http.post that transforms the body to the same param format used by  jQuery's $.post call
    $httpProvider.defaults.transformRequest = function(data){
        if (data === undefined) {
            return data;
        }
        return $.param(data);
    }

        dialogsProvider.useCopy(false);

    //sets the content type header globally for $http calls
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
    $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
    $httpProvider.defaults.headers['delete'] = {'Content-Type': 'application/json; charset=UTF-8'};

    $routeProvider
        .when('/', {
            templateUrl: 'dashboard/main.html',
            controller: 'MainCtrl',
            access: access.anon
        })
        .when('/login', {
            templateUrl: 'common/security/login.html',
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
        .when('/reports/gsr', {
            templateUrl: 'reports/gsr.html',
            controller: 'GSRReportController',
            access: access.manager
        })
        .when('/reports/volume', {
            templateUrl: 'reports/volume.html',
            controller: 'VolumeReportController',
            access: access.manager
        })
        .when('/reports/engine', {
            templateUrl: 'reports/engine.html',
            controller: 'ReportEngineController',
            access: access.executive
        })
        .when('/reports/customerregistrations', {
            templateUrl: 'reports/customerregistrations.html',
            controller: 'reports.CustomerRegistrationsCtrl',
            access: access.office
        })
        .when('/reports/customerregistrations/:id', {
            templateUrl: 'reports/customerregistration.html',
            controller: 'CustomerDetailsController',
            access: access.office
        })
        .when('/reports/birthdays', {
            templateUrl: 'reports/birthdays.html',
            controller: 'reports.BirthdaysCtrl',
            access: access.office
        })
		.when('/reports/courtesy-coupon', {
			templateUrl: 'reports/courtesy-coupon.html',
			controller: 'CourtesyCouponController',
			access: access.office
		})
        .when('/forms', {
            templateUrl: 'forms/forms-index.html',
            controller: 'FormsCtrl',
            access: access.anon
        })
        .when('/forms/claims', {
            templateUrl: 'forms/claims.html',
            controller: 'forms.ClaimsCtrl',
            access: access.anon
        })
        .when('/settings/users', {
            templateUrl: 'settings/users.html',
            controller: 'settings.UsersCtrl',
            access: access.admin
        })
        .when('/settings/users/:id', {
            templateUrl: 'settings/user.html',
            controller: 'settings.UserCtrl',
            access: access.admin
        })
        .when('/settings/sites', {
            templateUrl: 'settings/sites.html',
            controller: 'SitesController',
            access: access.admin
        })
        .when('/settings/sites/:id', {
            templateUrl: 'settings/site.html',
            controller: 'SiteController',
            access: access.admin
        })
        .when('/settings/notifications', {
            templateUrl: 'settings/notifications.html',
            controller: 'settings.NotificationsCtrl',
            access: access.admin
        })
        .when('/settings/notifications/:id', {
            templateUrl: 'settings/notification.html',
            controller: 'settings.NotificationCtrl',
            access: access.admin
        })
        .when('/noaccess', {
            templateUrl: 'common/security/no-access.html',
            controller: 'security.NoAccessCtrl',
            access: access.anon
        })
        .otherwise({
            redirectTo: '/reports/gsr',
            access: access.anon
    });


});

ramAngularApp.module.constant('AUTH_EVENTS', {
    loginSuccess: 'auth-login-success',
    loginFailed: 'auth-login-failed',
    logoutSuccess: 'auth-logout-success',
    sessionTimeout: 'auth-session-timeout',
    notAuthenticated: 'auth-not-authenticated',
    notAuthorized: 'auth-not-authorized'
});

ramAngularApp.module.constant('APP_SETTINGS', {
        apiUrl: 'http://pspapi.localhost/api/'
    });

ramAngularApp.module.run(function($rootScope, $location, AuthService){

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
