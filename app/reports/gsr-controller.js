/**
 * Created by danny_000 on 3/25/14.
 */
'use strict'

app.controller('reports.GSRCtrl', function($scope, $rootScope, AuthService){
    $scope.username = AuthService.currentUser().first;
});