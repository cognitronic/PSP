/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

angular.module('service.auth', [])
.factory('AuthService', function($http, $rootScope, $location, APP_SETTINGS){

        return {
            login: function(credentials){
                console.log('hey there ' + credentials.email);
                return $http.post(APP_SETTINGS.apiUrl + 'Login/PostUser', credentials, {
                })
                    .success(function(data, status, headers, config){
                        //_currentuser = data;
                        sessionStorage.setItem('psp.currentUser', JSON.stringify(data));
                        if(sessionStorage.getItem('psp.currentUser') !== null){
                            $rootScope.$broadcast('userLoggedIn', {user: data});
                            $location.path('/');
                        }
                        else
                            console.log('Invalid email or password');
                    });
            },
            isAuthenticated: function(){
                if(sessionStorage.getItem('psp.currentUser') !== null){
                    return true;
                }
                return false;
            },
            authorize: function(accessLevel, role) {
                if(role === undefined) {
                    role = currentUser.role;
                }

                return accessLevel.bitMask & role.bitMask;
            },
            isAuthorized: function(authorizedRoles){
                if(!angular.isArray(authorizedRoles)){
                    authorizedRoles = [authorizedRoles];
                }
                return true;//test against session
            },
            logout: function(){
               sessionStorage.removeItem('psp.currentUser');
                $rootScope.$broadcast('userLoggedOut', {user: null});
                $location.path('/login');
            },
            currentUser: function(){
                if(sessionStorage.getItem('psp.currentUser') !== null){
                    return (JSON.parse(sessionStorage.getItem('psp.currentUser')));
                }
                return null;
            }
        };
    });