/**
 * Created by Danny Schreiber on 4/9/14.
 */
'use strict'

angular.module('service.reports', [])
    .factory('ReportsService', function($http, $rootScope,$q, $location, APP_SETTINGS){

        return{
            saveClient: function(client){
                if(client.Id !== undefined){
                    client.sid = client.Id;
                }
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Clients', client)
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
            deleteClient: function(client){
                if(client.Id !== undefined){
                    client.sid = client.Id;
                }
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.delete(APP_SETTINGS.apiUrl + 'Clients', client)
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
            getClientById: function(id){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.get(APP_SETTINGS.apiUrl + 'Clients/' + id)
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
            getClients: function(params){
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http.post(APP_SETTINGS.apiUrl + 'Clients/', params)
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
            getBirthdays: function(params){
                var deferred = $q.defer();
                $rootScope.loading = true;
                console.log(params);
                $http.post(APP_SETTINGS.apiUrl + 'clients/birthdays', params)
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
            getGSRBySiteDate: function(viewModel){
                console.log(viewModel);
                var deferred = $q.defer();
                $rootScope.loading = true;
                $http({
                    url: APP_SETTINGS.apiUrl + 'Gsr/' + viewModel.site + '/' + viewModel.gsrDate.replace('/', '-').replace('/', '-'),
                    method: 'GET'})
                    .success(function(data){
                        $rootScope.loading = false;
                        console.log('success: ' + data);
                        deferred.resolve(data);
                    })
                    .error(function(){
                        $rootScope.loading = false;
                        console.log('rejected: ');
                        deferred.reject();
                    });
                return deferred.promise;
            }
        }
    });