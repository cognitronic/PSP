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
                $http.post(APP_SETTINGS.apiUrl + 'Users/Post', user)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
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
                $http.get(APP_SETTINGS.apiUrl + 'Users/Get/' + id)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getUsers: function(){
                var deferred = $q.defer();
                $http.get(APP_SETTINGS.apiUrl + 'Users/GetAll')
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getUsersByRole: function(){

            },
            deleteUser: function(usr){
                var deferred = $q.defer();
                $http.post(APP_SETTINGS.apiUrl + 'Users/RemoveUser/', usr)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            }
        }
    });