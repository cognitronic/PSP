/**
 * Created by Danny Schreiber on 4/11/14.
 */
'use strict'

ramAngularApp.module.controller('reports.BirthdaysCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService, Paginator){
    var _pagination = Paginator.getNew(25);
    var _lastName = "";
    var _email = "";
    var _birthDate = new Date();
    var _birthdateOpened = false;
    var _clients = [];


    var _loadBirthdays = function(){
        var params = {
            lastname: $scope.birthdaysModel.lastName,
            email: $scope.birthdaysModel.email,
            birthdate: $scope.birthdaysModel.convertDateToBirthdate()
        };
        ReportsService.getBirthdays(params).then(function(data){
            $scope.birthdaysModel.clients = data;
            console.log($scope.birthdaysModel.clients);
            $scope.birthdaysModel.pagination.numPages = Math.ceil($scope.birthdaysModel.clients.length/$scope.birthdaysModel.pagination.perPage);
            console.log((new Date()).getDay());
        });
    };

    var _convertDateToBirthdate = function(){
        if(!isNaN($scope.birthdaysModel.birthDate))
            return (new Date($scope.birthdaysModel.birthDate).getMonth() + 1) + '/' + (new Date($scope.birthdaysModel.birthDate).getDate());
    }

    var _deleteClient = function(client){
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

    var _sendCoupon = function(client){
        var params = {
            coupon: 'birthday',
            clientId: client.Id,
            email: client.email
        };
        ReportsService.sendCoupon(params).then(function(data){
           client.birthdaycouponsent = true;
        });
    }

    var _today = function() {
        $scope.dt = new Date();
    };

    _today();

    var _open = function($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.birthdaysModel.birthdateOpened = true;
    };

    var _dateOptions = {
        'year-format': "'yyyy'",
        'starting-day': 1
    };

    var _formats = ['dd-MMMM-yyyy', 'MM/dd/yyyy', 'MM/dd'];
    var _format = _formats[1];
    var _birthdateFormat = _formats[2];

    var _init = function(){
        $scope.birthdaysModel.birthDate.setDate($scope.birthdaysModel.birthDate.getDate() + 1);
        $scope.birthdaysModel.loadBirthdays();
    };

    $scope.birthdaysModel = {
        init: _init,
        pagination: Paginator.getNew(25),
        lastName: _lastName,
        email: _email,
        birthDate: _birthDate,
        loadBirthdays: _loadBirthdays,
        convertDateToBirthdate: _convertDateToBirthdate,
        deleteClient: _deleteClient,
        sendCoupon: _sendCoupon,
        open: _open,
        birthdateOpened: _birthdateOpened,
        clients: _clients
    };
    $scope.birthdaysModel.init();
});