/**
 * Created by danny_000 on 3/23/14.
 */
'use strict'

app.controller('settings.UsersCtrl', function($scope, $rootScope, $location, AuthService, UserService, Paginator){

    $scope.pagination = Paginator.getNew(2);

    UserService.getUsers().then(function(data){
        $scope.users = data;
        $scope.pagination.numPages = Math.ceil($scope.users.length/$scope.pagination.perPage);
    });

    $scope.AddUser = function(){
        $location.path('/users/new');
    }

    $scope.deleteUser = function(usr){
        var deleteUser = confirm('Are you sure you want to delete user?');
        if(deleteUser){
            usr.sid = usr.Id;
            UserService.deleteUser(usr).then(function(data){
                UserService.getUsers().then(function(data){
                    $scope.users = data;
                });
            });
        }
    }
});
