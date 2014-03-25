/**
 * Created by danny_000 on 3/23/14.
 */
'use strict'

app.controller('settings.UsersCtrl', function($scope, $rootScope, AuthService){
    $scope.username = AuthService.currentUser().first;
});