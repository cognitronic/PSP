/**
 * Created by danny_000 on 3/23/14.
 */
'use strict'

app.controller('settings.UsersCtrl', function($scope, $rootScope, $location, AuthService, UserService, Paginator, $timeout){

    $scope.pagination = Paginator.getNew(5);

    UserService.getUsers().then(function(data){
        $scope.users = data;
        $scope.pagination.numPages = Math.ceil($scope.users.length/$scope.pagination.perPage);
    });

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

    var _progress = 33;
    $scope._fakeWaitProgress = function(counter){
        console.log('fake wiat');
        $timeout(function(){
            if(counter < 100){
                counter += 20;
                $rootScope.$broadcast('dialogs.wait.progress',{'progress' : counter});
                $scope._fakeWaitProgress(counter);
            }else{
                $rootScope.$broadcast('dialogs.wait.complete');
            }
        },1000);
    };

    $scope.test = function(){
        console.log('is this working??');
    }
});
