/**
 * Created by Danny Schreiber on 3/22/14.
 */

'use strict'

angular.module('service.auth', [])
.factory('AuthService', function($http, $rootScope, $location, APP_SETTINGS, CacheService){
        var accessLevels = routingAccessConfig.accessLevels
            , userRoles = routingAccessConfig.userRoles
            , authMsg;

        return {
            login: function(credentials){
                $rootScope.loading = true;
                return $http.post(APP_SETTINGS.apiUrl + 'Login', credentials, {
                })
                    .success(function(data, status, headers, config){
                        $rootScope.loading = false;
                        //_currentuser = data;
                        if(data === "null"){
                            console.log('Invalid email or password');
                            CacheService.removeItem(CacheService.Items.UserInfo.currentUser);
							data.success = false;
                            return authMsg = 'Invalid email or password';
                        } else {
							data.success = true;
                            CacheService.setItem(CacheService.Items.UserInfo.currentUser, data);
                            if(CacheService.getItem(CacheService.Items.UserInfo.currentUser) !== null){
                                $rootScope.$broadcast('userLoggedIn', {user: data});
                                $location.path('/reports/gsr');
                            }
                        }
                    });
            },
            isAuthenticated: function(){
                return CacheService.getItem(CacheService.Items.UserInfo.currentUser);
            },
            authorize: function(accessLevel, role) {
                if(role === undefined) {
                    if(!this.currentUser().first){
                        role = userRoles["public"];
                    } else {
                        role = userRoles[this.currentUser().role];
                    }
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
                CacheService.removeItem(CacheService.Items.UserInfo.currentUser);
                $rootScope.$broadcast('userLoggedOut', {user: null});
                $location.path('/login');
            },
            authenitcationAttemptMessage: function(){
                return authMsg;
            },
            currentUser: function(){
                if(CacheService.getItem(CacheService.Items.UserInfo.currentUser) !== null){
                    return CacheService.getItem(CacheService.Items.UserInfo.currentUser);
                }
                return {};
            }
        };
    });
