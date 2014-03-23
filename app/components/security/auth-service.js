/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

angular.module('service.auth', [])
.factory('AuthService', function($http, APP_SETTINGS){
        
        return {
            login: function(credentials){
                console.log('hey there ' + credentials.email);
                return $http.post(APP_SETTINGS.apiUrl + 'Login/PostUser', credentials, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'}
                })
                    .then(function(res){
                        console.log('whoop...im logged in' + res);
                        console.log(res.config.data.email);
                    });
            },
            isAuthenticated: function(){
                return false; //check against session
            },
            isAuthorized: function(authorizedRoles){
                if(!angular.isArray(authorizedRoles)){
                    authorizedRoles = [authorizedRoles];
                }
                return true;//test against session
            }
        };
    });