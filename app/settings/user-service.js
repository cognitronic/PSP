/**
 * Created by Danny Schreiber on 3/30/14.
 */
'use strict'

angular.module('service.user', [])
    .factory('UserService', function($http, $rootScope,$q, $location, APP_SETTINGS){

        return{
            saveUser: function(user){
                if(user.Id !== undefined){
                    user.sid = user.Id;
                }
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Users', user)
                    .success(function(data){
                        $rootScope.loading = false;
                        deferred.resolve(data);
                    })
                    .error(function(){
                        $rootScope.loading = false;
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getUser: function(usr){

            },
            getUserByEmail: function(email){

            },
            getUserById: function(id){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Users/' + id)
                    .success(function(data){
                        $rootScope.loading = false;
                        deferred.resolve(data);
                    })
                    .error(function(){
                        $rootScope.loading = false;
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getUsers: function(){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Users')
                    .success(function(data){
                        $rootScope.loading = false;
                        deferred.resolve(data);
                    })
                    .error(function(){
                        $rootScope.loading = false;
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getUsersByRole: function(){

            },
            deleteUser: function(usr){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.delete(APP_SETTINGS.apiUrl + 'Users', usr)
                    .success(function(data){
                        $rootScope.loading = false;
                        deferred.resolve(data);
                    })
                    .error(function(){
                        $rootScope.loading = false;
                        deferred.reject();
                    });
                return deferred.promise;
            }
        }
    });