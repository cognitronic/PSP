/**
 * Created by Danny Schreiber on 4/6/14.
 */

'use strict'

angular.module('service.notifications', [])
    .factory('NotificationService', function($http, $rootScope,$q, $location, APP_SETTINGS){

        return{
            saveNotification: function(notification){
                if(notification.Id !== undefined){
                    notification.sid = notification.Id;
                }
                var deferred = $q.defer();
                $http.post(APP_SETTINGS.apiUrl + 'Notification/Post', notification)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getNotification: function(site){

            },
            getNotificationByName: function(name){

            },
            getNotificationById: function(id){
                var deferred = $q.defer();
                $http.get(APP_SETTINGS.apiUrl + 'Notification/Get/' + id)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getNotifications: function(){
                var deferred = $q.defer();
                $http.get(APP_SETTINGS.apiUrl + 'Notification/GetAll')
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            deleteRecipient: function(notification){
                var deferred = $q.defer();
                $http.post(APP_SETTINGS.apiUrl + 'Notification/DeleteRecipient/', notification)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            addRecipient: function(notification){
                var deferred = $q.defer();
                $http.post(APP_SETTINGS.apiUrl + 'Notification/AddRecipient', notification)
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