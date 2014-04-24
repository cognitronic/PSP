/**
 * Created by Danny Schreiber on 4/11/14.
 */
'use strict'

app.controller('reports.BirthdaysCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService, Paginator){
    $scope.pagination = Paginator.getNew(25);
    $scope.criteria_lastname = "";
    $scope.criteria_email = "";
    $scope.dtBirthdate = new Date();
    $scope.dtBirthdate.setDate($scope.dtBirthdate.getDate() + 1);

    ReportsService.getClients().then(function(data){
        $scope.clients = data;
        $scope.pagination.numPages = Math.ceil($scope.clients.length/$scope.pagination.perPage);
        console.log((new Date()).getDay());
    });

    $scope.convertDateToBirthdate = function(){
        if(!isNaN($scope.dtBirthdate))
            return (new Date($scope.dtBirthdate).getMonth() + 1) + '/' + (new Date($scope.dtBirthdate).getDate());
    }

    $scope.deleteClient = function(client){
        var deleteItem = confirm('Are you sure you want to delete record?');
        if(deleteItem){
            client.sid = client.Id.toString();
            ReportsService.deleteClient(client)
                .then(function(data){
                    ReportsService.getClients().then(function(data){
                        $scope.clients = data;
                    });
                });
        }
    }

    $scope.sendCoupon = function(client){

    }

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
    $scope.disabled = function(date, mode) {
        return ( mode === 'day' && ( date.getDay() === 0 || date.getDay() === 6 ) );
    };

    $scope.toggleMin = function() {
        $scope.minDate = ( $scope.minDate ) ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.birthdateOpened = true;
    };


    $scope.dateOptions = {
        'year-format': "'yyyy'",
        'starting-day': 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'MM/dd/yyyy', 'MM/dd'];
    $scope.format = $scope.formats[1];
    $scope.birthdateFormat = $scope.formats[2];

});