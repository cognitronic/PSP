/**
 * Created by Danny Schreiber on 10/3/14.
 */

(function(){

    'use strict';

    var VolumeReportController = function($scope, ReportsService, CacheService, SiteService){

        var _opened = false;
        var _selectedSite = {};
        var _selectedDate = '';
        var _sites = [];
        var _dt = undefined;
        var _byDateChartConfig = {};
        var _byWashesChartConfig = {};

        var _open = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.model.opened = true;
        };

        var _init = function(){
            $scope.model.dt = new Date();
            $scope.model.getSites();
        };

        var _getSites = function(){
            SiteService.getSites().then(function(data){
                $scope.model.sites = data;
                $scope.model.selectedSite = $scope.model.sites[0];
            });
        };

        var _getVolume = function(){
            $scope.model.getVolumeData();
            $scope.model.getVolumeByWashes();
        };

        var _getVolumeData = function(){
            ReportsService.getTotalVolume($scope.model.dt.toLocaleDateString().replace('/','-').replace('/','-')).then(function(data){
                console.log(data);
               $scope.model.byDateChartConfig = {
                   options: {
                       chart: {
                           type: 'line',
                           zoomType: 'y'
                       }
                   },
                   plotOptions: {
                       area: {
                           fillOpacity: 0.5
                       }
                   },
                   series: data.data,
                   title: {
                       text: 'Total Volume By Site For: ' + $scope.model.dt.toDateString()
                   },
                   xAxis: {
                       title: {
                           text: 'Locations'
                       },
                       categories: data.categories
                   },
                   yAxis: {
                       currentMin: 0,
                       currentMax: 1000,
                       minRange: 100,
                       title: {
                           text: 'Car Count'
                       }
                   }
               };
            });
        };

        var _getVolumeByWashes = function(){
            ReportsService.getVolumeByWashes($scope.model.dt.toLocaleDateString().replace('/','-').replace('/','-')).then(function(data){
                console.log(data);
                $scope.model.byWashesChartConfig = {
                    options: {
                        chart: {
                            type: 'column',
                            zoomType: 'x'
                        }
                    },
                    plotOptions: {
                        area: {
                            fillOpacity: 0.5
                        }
                    },
                    series: data.data,
                    title: {
                        text: 'Wash Volume By Site For: ' + $scope.model.dt.toDateString()
                    },
                    xAxis: {
                        title: {
                            text: 'Locations'
                        },
                        categories: data.categories
                    },
                    yAxis: {
                        currentMin: 0,
                        currentMax: 1000,
                        minRange: 100,
                        title: {
                            text: 'Car Count'
                        }
                    }
                };
            });
        };

        var _formats = ['dd-MMMM-yyyy', 'MM-dd-yyyy', 'MM/dd'];
        var _format = _formats[1];


        $scope.model = {
            open: _open,
            init: _init,
            opened: _opened,
            getSites: _getSites,
            dt: _dt,
            formats: _formats,
            format: _format,
            byDateChartConfig: _byDateChartConfig,
            byWashesChartConfig: _byWashesChartConfig,
            getVolume: _getVolume,
            getVolumeData: _getVolumeData,
            getVolumeByWashes: _getVolumeByWashes
        };

        $scope.model.init();
    };

    ramAngularApp.module.controller('VolumeReportController', ['$scope', 'ReportsService', 'CacheService', 'SiteService', VolumeReportController]);
})();