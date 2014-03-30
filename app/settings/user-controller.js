/**
 * Created by Danny Schreiber on 3/29/14.
 */

'use strict'

app.controller('settings.UserCtrl', function($scope, $rootScope, $routeParams, UserService){
    if($routeParams.id !== "new"){
        UserService.getUserById($routeParams.id)
            .then(function(data){
                $scope.user = data;
            });
    } else {

    }
});