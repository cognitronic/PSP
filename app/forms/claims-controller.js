/**
 * Created by danny_000 on 3/27/14.
 */
'use strict'

ramAngularApp.module.controller('forms.ClaimsCtrl', function($scope, $rootScope, AuthService, myService){
    $scope.username = AuthService.currentUser().first;

    $scope.mytime = new Date();

    $scope.hstep = 1;
    $scope.mstep = 15;

    $scope.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };

    $scope.ismeridian = true;
    $scope.toggleMode = function() {
        $scope.ismeridian = ! $scope.ismeridian;
    };

    $scope.update = function() {
        var d = new Date();
        d.setHours( 14 );
        d.setMinutes( 0 );
        $scope.mytime = d;
    };

    $scope.changed = function () {
        console.log('Time changed to: ' + $scope.mytime);
    };

    $scope.clear = function() {
        $scope.mytime = null;
    };

});

ramAngularApp.module.factory('myService', function($http, $q, APP_SETTINGS){
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