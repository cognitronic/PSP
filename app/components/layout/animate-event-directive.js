/**
 * Created by Danny Schreiber on 3/31/14.
 */

app.directive("animateEvent", function($animate){
   return function(scope, element, attrs){
       scope.$watch(attrs.animateEvent, function(newVal){
           if(newVal){
               $animate.removeClass(element, "fadeIn");
               $animate.addClass(element, "hideMe");
           } else {

               $animate.removeClass(element, "hideMe");
               $animate.addClass(element, "fadeIn");
           }
       });
   }
});

