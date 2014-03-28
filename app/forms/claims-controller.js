/**
 * Created by danny_000 on 3/27/14.
 */
'use strict'

app.controller('forms.ClaimsCtrl', function($scope, $rootScope, AuthService, myService){
    $scope.username = AuthService.currentUser().first;

});

app.factory('myService', function($http, $q, APP_SETTINGS){
    return {
        getUsers: function(){
            var deferred = $q.defer();
            $http.get(APP_SETTINGS.apiUrl + 'Claims/GetAll')
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