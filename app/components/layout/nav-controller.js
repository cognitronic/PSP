/**
 * Created by danny_000 on 3/23/14.
 */

app.controller('NavCtrl', function($scope, $rootScope, AuthService, $location){

    $scope.isAuthenticated = AuthService.isAuthenticated();

    $scope.logout = function(){
        console.log('logout');
        AuthService.logout();
    }

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
});