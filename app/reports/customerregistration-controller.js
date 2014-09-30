/**
 * Created by Danny Schreiber on 4/9/14.
 */

(function(){
    'use strict';

    var CustomerDetailsController = function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService, modalData){

        var _selectedClient = {};
        var _birthdateOpened = false;
        var _birthdate = new Date();

        var _saveClient = function(){
            if($scope.model.selectedClient.dateregistered === undefined){
                $scope.model.selectedClient.dateregistered = new Date().toISOString();
            }
            $scope.model.selectedClient.birthdate = _convertDateToBirthdate();
            ReportsService.saveClient($scope.model.selectedClient)
                .then(function(data){
                    $scope.$close();
                    $location.path('/reports/customerregistrations');
                });
        };

        var _convertDateToBirthdate = function(){
            if(!isNaN($scope.model.selectedClient.birthdate))
                return (new Date($scope.model.selectedClient.birthdate).getMonth() + 1) + '/' + (new Date($scope.model.selectedClient.birthdate).getDate());
        }

        var _deleteClient = function(){
            var deleteSite = confirm('Are you sure you want to delete this client, it cannot be undone?');
            if(deleteSite){
                $scope.model.selectedClient.sid = $scope.model.selectedClient.Id;
                ReportsService.deleteClient($scope.model.selectedClient).then(function(data){
                    $scope.$close();
                });
            }
        };

        var _returnToList = function(){
            $location.path('/reports/customerregistrations');
        };

        var _open = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.model.birthdateOpened = true;
        };

        var _init = function(){
            $scope.model.modalData = modalData;
            if(modalData.editingClient){
                $scope.model.selectedClient = modalData.editingClient;
                $scope.model.selectedClient.fullname = $scope.model.selectedClient.firstname + ' ' + $scope.model.selectedClient.lastname;

            }
        };

        $scope.model = {
            init: _init,
            selectedClient: _selectedClient,
            saveClient: _saveClient,
            deleteClient: _deleteClient,
            returnToList: _returnToList,
            open: _open,
            birthdateOpened: _birthdateOpened,
            birthdate: _birthdate
        };

        $scope.model.init();
    };

    ramAngularApp.module.controller('CustomerDetailsController', ['$scope', '$rootScope', '$routeParams', '$location', 'AuthService', 'ReportsService', 'modalData', CustomerDetailsController]);
})();