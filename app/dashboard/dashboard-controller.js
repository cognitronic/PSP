/**
 * Created by danny_000 on 3/24/14.
 */
'use strict'

ramAngularApp.module.controller('DashboardCtrl', function($scope, $rootScope, AuthService, $http){
    $scope.username = AuthService.currentUser().first;

	$http.get("http://www.myapifilms.com/taapi?count=1&format=JSON")
		.success(function(data){
			console.log(data);
		})
		.error(function(data){
			console.log(data);
		});
});
