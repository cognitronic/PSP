/**
 * Created by danny_000 on 3/23/14.
 */
'use strict'

app.controller('settings.UsersCtrl', function($scope, $rootScope, $location, AuthService, UserService){

    UserService.getUsers().then(function(data){
        $scope.users = data;
    });

    $scope.AddUser = function(){
        $location.path('/users/new');
    }
});
