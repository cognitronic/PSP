/**
 * Created by danny_000 on 3/23/14.
 */
'use strict'

app.controller('settings.UsersCtrl', function($scope, $rootScope, $location, AuthService, UserService, Paginator, $timeout){

    $scope.pagination = Paginator.getNew(5);

    $scope.loadUsers = function(){
        console.log('users loaded...');
        UserService.getUsers().then(function(data){
            $scope.users = data;
            $scope.pagination.numPages = Math.ceil($scope.users.length/$scope.pagination.perPage);
        });
    }

    $scope.loadUsers();

    $scope.AddUser = function(){
        $location.path('/users/new');
    }

    $scope.deleteUser = function(usr){
        usr.sid = usr.Id;
        UserService.deleteUser(usr).then(function(data){
            UserService.getUsers().then(function(data){
                $scope.users = data;
            });
        });
        console.log(usr);
        $scope.confirmed = 'from the directive';
    }
});
