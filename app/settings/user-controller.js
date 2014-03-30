/**
 * Created by Danny Schreiber on 3/29/14.
 */

'use strict'

app.controller('settings.UserCtrl', function($scope, $rootScope, AuthService){
    $scope.user = AuthService.currentUser();
});