/**
 * Created by Danny Schreiber on 4/2/14.
 */

'use strict'

angular.module('service.site', [])
    .factory('SiteService', function($http, $rootScope,$q, $location, APP_SETTINGS){

        return{
            saveSite: function(site){
                if(site.Id !== undefined){
                    site.sid = site.Id;
                }
                var deferred = $q.defer();
                $http.post(APP_SETTINGS.apiUrl + 'Site/Post', site)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getSite: function(site){

            },
            getSiteByName: function(name){

            },
            getSiteById: function(id){
                var deferred = $q.defer();
                $http.get(APP_SETTINGS.apiUrl + 'Site/Get/' + id)
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            getSites: function(){
                var deferred = $q.defer();
                $http.get(APP_SETTINGS.apiUrl + 'Site/GetAll')
                    .success(function(data){
                        deferred.resolve(data);
                    })
                    .error(function(){
                        deferred.reject();
                    });
                return deferred.promise;
            },
            deleteSite: function(site){
                var deferred = $q.defer();
                $http.post(APP_SETTINGS.apiUrl + 'Site/RemoveSite/', site)
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