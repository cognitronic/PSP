/**
 * Created by Danny Schreiber on 3/28/14.
 */
'use strict';

app.directive('accessLevel', ['AuthService', '$rootScope', '$location', function(AuthService, $rootScope, $location) {
    return {
        restrict: 'A',
        link: function($scope, element, attrs) {
            var prevDisp = element.css('display');
            var accessLevels = routingAccessConfig.accessLevels;

            $scope.$watch('isAuthenticated', function(value){
                updateCSS(accessLevels[attrs.accessLevel]);
            });

            attrs.$observe('accessLevel', function(al) {
                if(al){
                    updateCSS(accessLevels[al]);
                }
            });

            function updateCSS(accessLevel) {
                if(AuthService.currentUser().first !== undefined){
                    if(!AuthService.authorize(accessLevel)){
                        element.css('display', 'none');
                    } else {
                        element.css('display', prevDisp);
                    }
                } else {

                    element.css('display', 'none');
                }
            }
        }
    };
}]);