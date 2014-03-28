/**
 * Created by danny_000 on 3/25/14.
 */
app.controller('settings.NotificationsCtrl', function($scope, $rootScope, AuthService){
    $scope.username = AuthService.currentUser().first;
});