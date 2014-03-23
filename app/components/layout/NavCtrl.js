/**
 * Created by danny_000 on 3/23/14.
 */

app.controller('NavCtrl', function($scope, $rootScope, AuthService){
    $scope.isAuthenticated = AuthService.isAuthenticated();
});