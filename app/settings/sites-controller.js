/**
 * Created by danny_000 on 3/25/14.
 */

(function(){

    'use strict';

    var SitesController = function($scope, $rootScope, $location, AuthService, SiteService, Paginator){

        var _sites = [];

        var _loadSites = function(){
            SiteService.getSites().then(function(data){
                $scope.model.sites = data;
                $scope.model.pagination.numPages = Math.ceil($scope.model.sites.length/$scope.model.pagination.perPage);
            });
        };

        var _deleteSite = function(site){
            var deleteSite = confirm('Are you sure you want to delete site, this cannot be undone?');
            if(deleteSite){
                site.sid = site.Id;
                SiteService.deleteSite(site).then(function(data){
                    SiteService.getSites().then(function(data){
                        $scope.model.sites = data;
                    });
                });
            }
        }

        var _init = function(){
            $scope.model.loadSites();
        };

        $scope.model = {
            init: _init,
            loadSites: _loadSites,
            sites: _sites,
            pagination: Paginator.getNew(5),
            deleteSite: _deleteSite
        };
        $scope.model.init();
    };

    ramAngularApp.module.controller('SitesController', ['$scope', '$rootScope', '$location', 'AuthService', 'SiteService', 'Paginator', SitesController]);
})();
