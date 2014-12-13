/**
 * Created by Danny Schreiber on 10/15/2014.
 */

(function(){
    'use strict';

    var ReportEngineController = function($scope, Constants, SiteService, CacheService, ReportsService){
        var _dateRanges = [];
        var _selectedSites = [];
        var _selectedDates = {};
        var _sites = [];

        var _init = function(){
            $scope.model.dateRanges = Constants.DATE_RANGES;

            SiteService.getSites().then(function(data){
                $scope.model.sites = data;
                $scope.model.sites.unshift({'location': 'All Sites Selected', 'name': 'All'});
                for(var i = 0, l = $scope.model.sites.length; i < l; i++){
                    $scope.model.sites[i].ticked = false;
                }
            });
        };

        var _runReport = function(){
//            var params = {
//                dateRange: $scope.model.selectedDates.val,
//                sites: []
//            };
//            for(var i = 0, l = $scope.model.selectedSites.length; i < l; i++){
//                params.sites.push($scope.model.selectedSites[i].location)
//            }
//
//            ReportsService.exportGSRBySiteDateRange(params);
        };

        var _exportReport = function(){
            var params = {
                dateRange: $scope.model.selectedDates.val,
                sites: []
            };
            for(var i = 0, l = $scope.model.selectedSites.length; i < l; i++){
                params.sites.push($scope.model.selectedSites[i].location)
            }

            ReportsService.exportGSRBySiteDateRange(params);
        };

        $scope.model = {
            init: _init,
            dateRanges: _dateRanges,
            selectedSites: _selectedSites,
            selectedDates: _selectedDates,
            sites: _sites,
            runReport: _runReport,
            exportReport: _exportReport
        }
        $scope.model.init();
    };

    ramAngularApp.module.controller('ReportEngineController', ['$scope', 'Constants', 'SiteService', 'CacheService', 'ReportsService', ReportEngineController]);
})();