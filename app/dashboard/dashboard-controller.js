/**
 * Created by danny_000 on 3/24/14.
 */
'use strict'

ramAngularApp.module.controller('DashboardCtrl', function($scope, $rootScope, AuthService){
    $scope.username = AuthService.currentUser().first;
});