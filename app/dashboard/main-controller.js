/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

app.controller('MainCtrl', function($scope, $location, $rootScope, AuthService){
    if(!AuthService.isAuthenticated()){
        $location.path('/login');
    } else {
        console.log(AuthService.currentUser().email )
    }
    console.log('Inside of main controller');
    console.log(AuthService.currentUser().first)
});