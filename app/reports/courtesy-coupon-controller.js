/**
 * Created by Danny Schreiber on 5/31/2015.
 */

'use strict';

ramAngularApp.module.controller('CourtesyCouponController', function($scope, $rootScope, $routeParams, $location, AuthService, ReportsService, Paginator) {

	var _pagination = Paginator.getNew(25);
	var _dateSent = "";
	var _emails = "";
	var _sentBy = "";
	var _sentCoupons = [];

	var _loadCoupons = function(){
		var params = {};
		ReportsService.getSentCourtesyCoupons(params).then(function(data){
			$scope.courtesyModel.sentCoupons = data;
			$scope.courtesyModel.pagination.numPages = Math.ceil($scope.courtesyModel.sentCoupons.length/$scope.courtesyModel.pagination.perPage);
			console.log((new Date()).getDay());
		});
	};

	var _sendCoupons = function() {
		var params = {
			coupon: 'courtesy',
			clientId: '',
			email: $scope.courtesyModel.emails,
			sentBy: AuthService.currentUser().first + ' ' + AuthService.currentUser().last

		};
		ReportsService.sendCoupon(params).then(function(data){
			console.log(data);
			console.log('repsonse de courtesy');
			$scope.courtesyModel.init();
		});
	};

	var _init = function() {
		$scope.courtesyModel.loadCoupons();
	};

	$scope.courtesyModel = {
		sentCoupons: _sentCoupons,
		init: _init,
		pagination: Paginator.getNew(25),
		loadCoupons: _loadCoupons,
		sendCoupons: _sendCoupons,
		emails: _emails
	};

	$scope.courtesyModel.init();
});
