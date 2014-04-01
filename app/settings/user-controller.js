/**
 * Created by Danny Schreiber on 3/29/14.
 */

'use strict'

app.controller('settings.UserCtrl', function($scope, $location,$timeout, $rootScope, $routeParams, UserService){
    $scope.user = {};
    $scope.roles = routingAccessConfig.rolesList;
    $scope.role = $scope.roles[0];
    $scope.isHidden = true;


    if($routeParams.id !== "new"){
        UserService.getUserById($routeParams.id)
            .then(function(data){
                $scope.user = data;
            });
    } else {

    }

    $scope.saveProfile = function(){
        UserService.saveUser($scope.user)
            .then(function(data){
                $location.path('/users/' + data.Id);
                $scope.isHidden = !$scope.isHidden;

            });
    }

    $scope.deleteUser = function(){
        if($routeParams.id !== "new"){
            UserService.deleteUser($routeParams.id)
                .then(function(data){
                    $location.path('/users');
                });
        }
    }

    $scope.returnToList = function(){
        $location.path('/users');
    }
});