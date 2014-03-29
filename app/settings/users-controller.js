/**
 * Created by danny_000 on 3/23/14.
 */
'use strict'

app.controller('settings.UsersCtrl', function($scope, $rootScope, AuthService, myService){
    $scope.username = AuthService.currentUser().first;
    myService.getUsers().then(function(data){
        $scope.users = data;
        console.log($scope.users[0]);
    });
});

app.factory('myService', function($http, $q, APP_SETTINGS){
   return {
       getUsers: function(){
           var deferred = $q.defer();
           $http.get(APP_SETTINGS.apiUrl + 'Users/GetAll')
               .success(function(data){
                   deferred.resolve(data);
               })
               .error(function(){
                   deferred.reject();
               });
           return deferred.promise;
       }
   }
});