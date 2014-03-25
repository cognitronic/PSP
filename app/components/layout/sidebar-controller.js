/**
 * Created by danny_000 on 3/25/14.
 */

app.controller('SidebarCtrl', function($scope){



    $scope.toggleSideNav = function(){
        console.log('clicked');
        return !$scope.isVisible;
        console.log($scope.isVisible);
    }
    $scope.isVisible = true;
});