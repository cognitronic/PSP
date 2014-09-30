/**
 * Created by Danny Schreiber on 3/29/14.
 */

'use strict'

ramAngularApp.module.controller('settings.UserCtrl', function($scope, $location,$timeout, $rootScope, $routeParams, UserService, $modalInstance, modalData, defaultStrings ){
    $scope.user = {};
    $scope.roles = routingAccessConfig.rolesList;
    $scope.role = $scope.roles[0];
    $scope.isHidden = true;
    $scope.showHideText = 'Show';
    $scope.passwordType = 'password';

    $scope.header = modalData.header;
    $scope.defaultStrings = defaultStrings;
    //$scope.editing = data.editingUser;
console.log(modalData);
    if(modalData.editingUser){
        $scope.user = modalData.editingUser;
    }

    $scope.cancel = function(){
        console.log('hey there');
        $modalInstance.dismiss('Canceled');
    }; // end cancel

    $scope.save = function(){
        $modalInstance.close($scope.user.name);
    }; // end save

//    if($routeParams.id !== "new"){
//        UserService.getUserById($routeParams.id)
//            .then(function(data){
//                $scope.user = data;
//            });
//    } else {
//
//    }
    $scope.togglePassword = function(){
        if($scope.showHideText === 'Show'){
            $scope.showHideText = 'Hide';
            $scope.passwordType = 'text';
        } else {
            $scope.showHideText = 'Show';
            $scope.passwordType = 'password';
        }
    }

    $scope.saveProfile = function(){
        UserService.saveUser($scope.user)
            .then(function(data){
                $location.path('/settings/users');
                $modalInstance.close();

            });
    }

    $scope.deleteUser = function(){
        if($routeParams.id !== "new"){
            UserService.deleteUser($routeParams.id)
                .then(function(data){
                    $location.path('/settings/users');
                });
        }
    }

    $scope.returnToList = function(){
        $location.path('/settings/users');
    }




    //$scope.dtDateRegistered =data.dtDateRegistered;
    $scope.dp = {
        isOpen: false,
        dtDateRegistered: new Date()
    };

    $scope.open = function($event) {
        $event.preventDefault();
        $event.stopPropagation();
        console.log(data.burble);
        $scope.dp.isOpen = true;
        console.log(data.registeredOpened);
    };


    $scope.dateOptions = {
        'year-format': "'yyyy'",
        'starting-day': 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'MM/dd/yyyy', 'MM/dd'];
    $scope.format = $scope.formats[1];


});