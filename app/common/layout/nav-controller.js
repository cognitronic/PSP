/**
 * Created by Danny Schreiber on 3/23/14.
 */

app.controller('NavCtrl', function($scope, $rootScope, AuthService, $location, CacheService){

    $scope.isAuthenticated = AuthService.isAuthenticated();

    $scope.$on('userLoggedIn', function (event, data){
        $scope.isAuthenticated = AuthService.isAuthenticated();
    });

    $scope.$on('userLoggedOut', function (event, data){
        $scope.isAuthenticated = AuthService.isAuthenticated();
    });


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
});