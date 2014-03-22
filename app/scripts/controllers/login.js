/**
 * Created by danny_000 on 3/21/14.
 */
'use strict'

angular.module('pspApp')
.controller('LoginCtrl', function($scope, $rootScope, AUTH_EVENTS, AuthService){
       $scope.credentials = {
           email: '',
           password: ''
       };
        $scope.login = function(credentials){
            AuthService.login(credentials).then(function(){
                $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
            }, function(){
                $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
            });
        };

    });