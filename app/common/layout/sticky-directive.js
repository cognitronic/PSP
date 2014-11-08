/**
 * Created by Danny Schreiber on 6/2/14.
 */
ramAngularApp.module.directive('sticky', function(){
    return {
        restrict: 'A',
        link: function($scope, element, $attrs){

            var $window = angular.element(window);
            var pos = element.position();
            $window.scroll(function() {
                var windowpos = $window.scrollTop();
                //element.html("Distance from top:" + pos.top + "<br />Scroll position: " + windowpos);
                if (windowpos >= pos.top - 15) {
                    element.addClass("stick");
                } else {
                    element.removeClass("stick");
                }
            });
        }
    }

});
