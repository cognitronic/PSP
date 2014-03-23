/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

angular.module('service.auth', [])
.factory('AuthService', function($http, $location, APP_SETTINGS){
        var _currentuser = {
            email:'',
            first: '',
            last: '',
            password: '',
            id: ''
        };

        return {
            login: function(credentials){
                console.log('hey there ' + credentials.email);
                return $http.post(APP_SETTINGS.apiUrl + 'Login/PostUser', credentials, {
                })
                    .success(function(data, status, headers, config){
                        console.log( data);
                        _currentuser = data;
                        console.log(_currentuser.first);
                        if(_currentuser !== "null")
                            $location.path('/');
                        else
                            console.log('Invalid email or password');
                    });
            },
            isAuthenticated: function(){
               if(_currentuser !== '' && _currentuser !== undefined){
                   return true;
               }
                return false;
            },
            isAuthorized: function(authorizedRoles){
                if(!angular.isArray(authorizedRoles)){
                    authorizedRoles = [authorizedRoles];
                }
                return true;//test against session
            },
            logout: function(){
                _currentuser = {};
                $location.path('/login');
            },
            currentUser: function(){
                return _currentuser;
            }
        };
    });