/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

ramAngularApp.module.controller('MainCtrl', function($scope, $location, $rootScope, AuthService){

    if(!AuthService.isAuthenticated()){
        $location.path('/login');
    }
});