/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

angular.module('service.auth', [])
.factory('AuthService', function($http, APP_SETTINGS){
        return {
            login: function(credentials){
                console.log(credentials);
                return $http.post(APP_SETTINGS.apiUrl + 'User/login', credentials)
                    .then(function(res){
                        console.log('whoop...im logged in');
                    });
            },
            isAuthenticated: function(){
                return true; //check against session
            },
            isAuthorized: function(authorizedRoles){
                if(!angular.isArray(authorizedRoles)){
                    authorizedRoles = [authorizedRoles];
                }
                return true;//test against session
            }
        };
    });