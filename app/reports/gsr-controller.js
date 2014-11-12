/**
 * Created by Danny Schreiber on 3/25/14.
 */

(function(){
    'use strict';
    var GSRReportController = function($scope, $rootScope, CacheService, ReportsService, SiteService, $q){

        var _selectedSite = {};
        var _selectedDate = '';
        var _reportParams = {};
        var _gsr = undefined;
        var _maxDate = '';
        var _dt = undefined;
        var _showWeeks = true;
        var _dateOptions = {
            'year-format': "'yyyy'",
            'starting-day': 1
        };

        var _formats =['dd-MMMM-yyyy', 'MM/dd/yyyy', 'MM/dd'];
        var _format = undefined;
        var _birthdateFormat = undefined;
        var _sites = [];


        $scope.$on('$routeChangeStart', function(next, current) {
            CacheService.removeItem(CacheService.Items.Reports.selectedGsr);
        });

        var _init = function(){
            $scope.model.toggleMax();
            $scope.model.today();
            $scope.model.format = $scope.model.formats[1];
			$scope.model.birthdateFormat = $scope.model.formats[2];

            $scope.model.getSites().then(function(data){
				if(CacheService.getItem(CacheService.Items.Reports.selectedGsr)){
					$scope.model.gsr = CacheService.getItem(CacheService.Items.Reports.selectedGsr);
					$scope.model.getGsr();
				} else {
					$scope.model.getGsr();
				}

			});
        };

        var _getGsr = function(){
            CacheService.removeItem(CacheService.Items.Reports.selectedGsr);
            console.log($scope.model.selectedSite);
            if($scope.model.selectedSite.Id){

                $scope.model.reportParams = {
                    site: $scope.model.selectedSite.Id,
                    gsrDate: $scope.model.dt.toLocaleDateString()
                }
                ReportsService.getGSRBySiteDate($scope.model.reportParams).then(function(data){
                    CacheService.setItem(CacheService.Items.Reports.selectedGsr, data);
                    $scope.model.gsr = data;
                    console.log(data);
                });
            }
        }

        var _getSites = function(){
			var defered = $q.defer();
            SiteService.getSites().then(function(data){
                 $scope.model.sites = data;
                $scope.model.selectedSite = $scope.model.sites[0];
				defered.resolve(data);
            });
			return defered.promise;
        };

        var _toggleMax = function() {
            $scope.model.maxDate = ( $scope.model.maxDate ) ? null : new Date();
        };

        var _today = function() {
            $scope.model.dt = new Date();
        };

        var _toggleWeeks = function () {
            $scope.model.showWeeks = ! $scope.model.showWeeks;
        };

        var _clear = function () {
            $scope.model.dt = null;
        };

        var _open = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            if($event.currentTarget.id === 'btnBirthdate'){
                $scope.model.birthdateOpened = true;
                $scope.model.registeredOpened = false;
            } else {
                $scope.model.birthdateOpened = false;
                $scope.model.registeredOpened = true;
            }
        };

        var _exportGSR = function(){
            ReportsService.exportGSRReport($scope.model.selectedSite.location, 'CUSTOM_' +
                $scope.model.dt.toLocaleDateString().replace('/','-').replace('/','-') + '_' + $scope.model.dt.toLocaleDateString().replace('/','-').replace('/','-'));
        };

        $scope.model = {
            init: _init,
            selectedSite: _selectedSite,
            selectedDate: _selectedDate,
            reportParams: _reportParams,
            getSites: _getSites,
            sites: _sites,
            gsr: _gsr,
            getGsr: _getGsr,
            toggleMax: _toggleMax,
            today: _today,
            showWeeks: _showWeeks,
            maxDate: _maxDate,
            dt: _dt,
            toggleWeeks: _toggleWeeks,
            clear: _clear,
            birthdateOpened: true,
            registeredOpened: false,
            dateOptions: _dateOptions,
            open: _open,
            formats: _formats,
            format: _format,
            birthdateFormat: _birthdateFormat,
            exportGSR: _exportGSR
        }

        $scope.model.init();
    };

    ramAngularApp.module.controller('GSRReportController',['$scope', '$rootScope', 'CacheService', 'ReportsService', 'SiteService', '$q', GSRReportController]);
})();
