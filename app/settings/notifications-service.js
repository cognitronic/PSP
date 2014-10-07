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
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Notifications', notification)
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
            getNotification: function(site){

            },
            getNotificationByName: function(name){

            },
            getNotificationById: function(id){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Notifications/' + id)
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
            getNotifications: function(){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Notifications')
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
            deleteRecipient: function(notification){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Notifications/DeleteRecipient/', notification)
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
            addRecipient: function(notification){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Notifications', notification)
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
            runNotification: function(noteparam){
                var deferred = $q.defer();
                $rootScope.loading = true;
                var url = '';
                console.log(noteparam.name);
                if(noteparam.name == 'Volume_Report'){
                    url = 'reports/volume/run-notification/' + noteparam.date;
                } else if(noteparam.name == 'GSR_Audit'){
                    url = 'gsr/run-notification/' + noteparam.date;
                } else if(noteparam.name == 'Rewash_Alert'){
                    url = 'reports/rewash/run-notification/' + noteparam.date
                }
                $http.get(APP_SETTINGS.apiUrl + url)
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