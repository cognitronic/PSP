/**
 * Created by Danny Schreiber on 3/29/14.
 */
'use strict'

app.controller('security.NoAccessCtrl', function($scope, $rootScope){
    $scope.message = "You are not authorized to view this page";
});