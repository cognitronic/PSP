/**
 * Created by Danny Schreiber on 3/25/14.
 */
'use strict'

app.controller('reports.GSRCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService, SiteService){

    $scope.gsr = {};
    $scope.runGSR = function(){
        console.log($scope.site);
        $scope.viewModel = {
            site: $scope.site.name,
            gsrDate: $scope.dt.toLocaleDateString()
        }
        ReportsService.getGSRBySiteDate($scope.viewModel).then(function(data){
           console.log(data);
            $scope.gsr = data;
        });
    }

    SiteService.getSites().then(function(data){
       $scope.sites = data;
        $scope.site = $scope.sites[0];
        console.log($scope.sites);
    });

//    ReportsService.getGSRBySiteDate().then(function(data){
//        $scope.gsr = data;
//    });


    $scope.toggleMax = function() {
        $scope.maxDate = ( $scope.maxDate ) ? null : new Date();
    };
    $scope.toggleMax();



    $scope.today = function() {
        $scope.dt = new Date();
    };

    $scope.today();

    $scope.showWeeks = true;
    $scope.toggleWeeks = function () {
        $scope.showWeeks = ! $scope.showWeeks;
    };

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
//    $scope.disabled = function(date, mode) {
//        return ( mode === 'day' && ( date.getDay() === 0 || date.getDay() === 6 ) );
//    };
//
//    $scope.toggleMin = function() {
//        $scope.minDate = ( $scope.minDate ) ? null : new Date();
//    };
//    $scope.toggleMin();

    $scope.open = function($event) {
        $event.preventDefault();
        $event.stopPropagation();
        if($event.currentTarget.id === 'btnBirthdate'){
            $scope.birthdateOpened = true;
            $scope.registeredOpened = false;
        } else {
            $scope.birthdateOpened = false;
            $scope.registeredOpened = true;
        }
    };


    $scope.dateOptions = {
        'year-format': "'yyyy'",
        'starting-day': 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'MM/dd/yyyy', 'MM/dd'];
    $scope.format = $scope.formats[1];
    $scope.birthdateFormat = $scope.formats[2];
});