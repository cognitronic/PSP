/**
 * Created by danny_000 on 3/22/14.
 */

'use strict'


ramAngularApp.module.controller('LoginCtrl', function($scope, $rootScope, AUTH_EVENTS, AuthService){
    console.log('Inside of login controller');
    $scope.credentials = {
        email: '',
        password: ''
    };
    $scope.login = function(credentials){
        AuthService.login(credentials).then(function(data){
			$scope.loginmessage = "Invalid email/password.  Please try again.";
		});
    };

});
