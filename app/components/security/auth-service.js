/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'

angular.module('service.auth', [])
.factory('AuthService', function($http, $rootScope, $location, APP_SETTINGS){
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
                        if(_currentuser !== "null"){
                            $rootScope.$broadcast('userLoggedIn', {user: _currentuser});
                            $location.path('/');
                        }
                        else
                            console.log('Invalid email or password');
                    });
            },
            isAuthenticated: function(){
               if(_currentuser === null || _currentuser === undefined || _currentuser.email === ''){
                   return false;
               }
                return true;
            },
            isAuthorized: function(authorizedRoles){
                if(!angular.isArray(authorizedRoles)){
                    authorizedRoles = [authorizedRoles];
                }
                return true;//test against session
            },
            logout: function(){
                _currentuser = null;
                $rootScope.$broadcast('userLoggedIn', {user: _currentuser});
                $location.path('/login');
            },
            currentUser: function(){
                return _currentuser;
            }
        };
    });