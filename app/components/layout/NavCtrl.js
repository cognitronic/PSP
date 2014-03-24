/**
 * Created by danny_000 on 3/23/14.
 */

app.controller('NavCtrl', function($scope, $rootScope, AuthService){
    $scope.$on("userLoggedIn",function(event,args) {
        $scope.isAuthenticated = AuthService.isAuthenticated();
    });

    $scope.logout = function(){
        console.log('logout');
        AuthService.logout();
    }
});