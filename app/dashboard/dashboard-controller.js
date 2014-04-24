/**
 * Created by danny_000 on 3/24/14.
 */
'use strict'

app.controller('DashboardCtrl', function($scope, $rootScope, AuthService){
    $scope.username = AuthService.currentUser().first;
});