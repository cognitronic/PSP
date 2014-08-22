/**
 * Created by Danny Schreiber on 5/22/14.
 */

app.directive('psFormatMoney', function($filter){

    function link(scope, element, attr){
        scope.$watch('dollarAmt', function(newValue, oldValue){
           if(scope.dollarAmt !== undefined){
               if(scope.dollarAmt < 0){
                   element.html('<b class="error">' + $filter('currency')(scope.dollarAmt) + '</b>');
               }
               else{
                   element.html($filter('currency')(scope.dollarAmt));
               }
           }
        });
    }

    return {
        link: link,
        restrict: 'A',
        scope: {
            dollarAmt: '='
        }
    }
});