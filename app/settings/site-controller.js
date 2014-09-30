/**
 * Created by Danny Schreiber on 4/2/14.
 */

(function(){
    'use strict';

    var SiteController = function($scope, $location,$timeout, $rootScope, $routeParams, SiteService, Paginator, modalData){
        var _site = {};
        var _dbTypes = ['firebird','mysql','sql2005', 'sql2008'];
        var _dbType = null;
        var _isHidden = true;
        var _selectedSite = {};

        var _init = function(){
            $scope.model.modalData = modalData;
            if(modalData.editingSite){
                $scope.model.selectedSite = modalData.editingSite;
                $scope.model.dbType = $scope.model.dbTypes[0];
            }
        };

        var _deleteSite = function(){
            var deleteSite = confirm('Are you sure you want to delete site, this cannot be undone?');
            if(deleteSite){
                $scope.model.selectedSite.sid = $scope.model.selectedSite.Id;
                SiteService.deleteSite($scope.model.selectedSite).then(function(data){
                    SiteService.getSites().then(function(data){
                        $scope.$close();
                    });
                });
            }
        }

        var _editSite = function(site){
            $scope.model.selectedSite = site;
            SiteService.getSiteById(site.id)
                .then(function(data){
                    $scope.model.site = data;
                });
        };

        var _saveSite = function(){
            SiteService.saveSite($scope.model.selectedSite)
                .then(function(data){
                    $location.path('/settings/sites');
                    $scope.$close();
                });
        };

        var _returnToList = function(){
            $location.path('/settings/sites');
        };

        $scope.model = {
            site: _site,
            dbTypes: _dbTypes,
            dbType: _dbType,
            isHidden: _isHidden,
            init: _init,
            editSite: _editSite,
            saveSite: _saveSite,
            deleteSite: _deleteSite,
            returnToList: _returnToList,
            selectedSite: _selectedSite
        };

        $scope.model.init();
    };

    ramAngularApp.module.controller('SiteController', ['$scope', '$location', '$timeout', '$rootScope', '$routeParams', 'SiteService', 'Paginator', 'modalData', SiteController]);
})();