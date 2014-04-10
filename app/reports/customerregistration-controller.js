/**
 * Created by Danny Schreiber on 4/9/14.
 */

app.controller('reports.CustomerRegistrationCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService){

    $scope.client = {};

    if($routeParams.id !== "new"){
        ReportsService.getClientById($routeParams.id)
            .then(function(data){
                $scope.client = data;
            });
    }

    $scope.saveClient = function(){

    }

    $scope.deleteClient = function(){

    }

    $scope.returnToList = function(){
        $location.path('/customerregistrations');
    }



    $scope.toggleMax = function() {
        $scope.maxDate = ( $scope.maxDate ) ? null : new Date();
    };
    $scope.toggleMax();

    $scope.today = function() {
        $scope.dt = new Date();
    };
//    $scope.dtBirthdate = new Date();
//    $scope.dtDateRegistered = new Date();
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
    $scope.birthdateFormat = $scope.formats[2];

});