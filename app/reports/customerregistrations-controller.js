/**
 * Created by Danny Schreiber on 4/9/14.
 */

(function(){
    'use strict';

    var CustomerController = function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService, Paginator){

        var _criteria_lastname = "";
        var _criteria_email = "";
        var _birthdate = undefined;
        var _dateRegistered = undefined;
        var _clients = [];

        var _loadClients = function(){
            $scope.model.clients = [];

            var params = {
                lastname: $scope.model.criteria_lastname,
                email: $scope.model.criteria_email,
                birthdate: $scope.model.convertDateToBirthdate(),
                dateregistered: _convertDateToISO()
            };
            ReportsService.getBirthdays(params).then(function(data){
                $scope.model.clients = data;
                console.log($scope.model.clients);
                $scope.model.pagination.numPages = Math.ceil($scope.model.clients.length/$scope.model.pagination.perPage);
                console.log(params);
            });
        };

        var _convertDateToBirthdate = function(){
            if(!isNaN($scope.model.birthdate))
                return (new Date($scope.model.birthdate).getMonth() + 1) + '/' + (new Date($scope.model.birthdate).getDate());
        }

        var _convertDateToISO = function(){
            if(!isNaN($scope.model.dateRegistered))
                return $scope.model.dateRegistered.toISOString();
            return null;
        }

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

        var _deleteClient = function(client, $index){
            client.sid = client.Id;
            ReportsService.deleteClient(client).then(function(data){
                $scope.model.clients.splice($index, 1);
            });
        };

        var _init = function(){
            //$scope.model.loadClients();
        };

        $scope.model = {
            init: _init,
            convertDateToBirthdate: _convertDateToBirthdate,
            birthdate: _birthdate,
            dateRegistered: _dateRegistered,
            clients: _clients,
            loadClients: _loadClients,
            pagination: Paginator.getNew(25),
            open: _open,
            criteria_lastname: _criteria_lastname,
            criteria_email: _criteria_email,
            deleteClient: _deleteClient
        };

        $scope.model.init();
    };

    ramAngularApp.module.controller('reports.CustomerRegistrationsCtrl', ['$scope', '$rootScope', '$routeParams', '$location', 'AuthService', 'ReportsService', 'Paginator', CustomerController]);
})();