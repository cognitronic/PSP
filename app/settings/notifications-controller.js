/**
 * Created by danny_000 on 3/25/14.
 */
ramAngularApp.module.controller('settings.NotificationsCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, NotificationService){

    NotificationService.getNotifications().then(function(data){
        $scope.notifications = data;
    });

    $scope.editNotification = function(id){

        $location.path('/settings/notifications/' + id);
    }
});