/**
 * Created by Danny Schreiber on 6/2/14.
 */
ramAngularApp.module.directive('sticky', function(){
    return {
        restrict: 'A',
		scope: {
			ramTop: '='
		},
        link: function($scope, element, $attrs){

            var $window = angular.element(window);
			var top = $scope.ramTop || 0;
            var pos = element.position();
            $window.scroll(function() {
                var windowpos = $window.scrollTop();
               // element.append("Distance from top:" + pos.top + "<br />Scroll position: " + windowpos);
                if (windowpos >= pos.top) {
                    element.addClass("stick");

                } else {
                    element.removeClass("stick");
                }
            });
        }
    }

});
