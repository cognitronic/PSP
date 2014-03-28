/**
 * Created by danny_000 on 3/23/14.
 */

app.controller('NavCtrl', function($scope, $rootScope, AuthService, $location){

    $scope.isAuthenticated = AuthService.isAuthenticated();

    $scope.logout = function(){
        console.log('logout');
        AuthService.logout();
    }

    $scope.isActive = function(routes) {
        var retval;
        for(var i = 0, l = routes.length; i<l; i++){
            console.log(routes[i]);
            if(routes[i] === $location.path()){
                return true;
            }
            retval = false;
        }
        return retval;
    }
});

app.directive('activeLink', ['$location', function(location) {
    return {
        restrict: 'A',
        link: function(scope, element, attrs, controller) {
            var clazz = attrs.activeLink;
            var path = attrs.href;
            path = path.substring(1); //hack because path does not return including hashbang
            scope.location = location;
            scope.$watch('location.path()', function(newPath) {
                if (path === newPath) {
                    element.addClass(clazz);
                } else {
                    element.removeClass(clazz);
                }
            });
        }
    };
}]);