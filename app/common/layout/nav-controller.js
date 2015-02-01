/**
 * Created by Danny Schreiber on 3/23/14.
 */

ramAngularApp.module.controller('NavCtrl', function($scope, $rootScope, AuthService, $location, CacheService, UserService, toaster){

	var _currentPassword, _newPassword, _confirmPassword;

    $scope.isAuthenticated = AuthService.isAuthenticated();

    $scope.$on('userLoggedIn', function (event, data){
        $scope.isAuthenticated = AuthService.isAuthenticated();
    });

    $scope.$on('userLoggedOut', function (event, data){
        $scope.isAuthenticated = AuthService.isAuthenticated();
    });

	var _changePassword = function(){
		if($scope.model.newPassword == $scope.model.confirmPassword){
			if($scope.model.currentPassword == CacheService.getItem(CacheService.Items.UserInfo.currentUser).password){
				var user = CacheService.getItem(CacheService.Items.UserInfo.currentUser);
				user.password = $scope.model.newPassword;
				UserService.saveUser(user);
				toaster.pop('success', "Password Changed", "Your password has been updated successfully.");
				$scope.$dismiss();
			} else {
				toaster.pop('error', "Password Change Failed", "Invalid password combinations, please try again.");
			}
		}else {
			toaster.pop('error', "Password Change Failed", "Invalid password combinations, please try again.");
		}
	};




    $scope.logout = function(){
        AuthService.logout();
    }

    $scope.getLoggedOnUser = function(){
        if(CacheService.getItem(CacheService.Items.UserInfo.currentUser)){
            var user = CacheService.getItem(CacheService.Items.UserInfo.currentUser);
            return user.first + ' ' + user.last;
        }
    };

    //sets active nav link css, or parent nav link css if has subnav
    $scope.isActive = function(routes) {
        var retval;
        for(var i = 0, l = routes.length; i<l; i++){
            if(routes[i] === $location.path()){
                return true;
            }
            retval = false;
        }
        return retval;
    }

	$scope.model = {
		changePassword: _changePassword,
		currentPassword: _currentPassword,
		newPassword: _newPassword,
		confirmPassword: _confirmPassword
	}
});
