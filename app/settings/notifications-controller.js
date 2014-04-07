/**
 * Created by danny_000 on 3/25/14.
 */
app.controller('settings.NotificationsCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, NotificationService){

    NotificationService.getNotifications().then(function(data){
        $scope.notifications = data;
    });

    $scope.editNotification = function(id){

        $location.path('notifications/' + id);
    }
});