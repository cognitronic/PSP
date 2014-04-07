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
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Site/Post', site)
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
            getSite: function(site){

            },
            getSiteByName: function(name){

            },
            getSiteById: function(id){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Site/Get/' + id)
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
            getSites: function(){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Site/GetAll')
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
            deleteSite: function(site){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Site/RemoveSite/', site)
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