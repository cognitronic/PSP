/**
 * Created by Danny Schreiber on 4/24/14.
 */


app.directive('ngConfirmClick', [
    function(){

        function Link(scope, element, attr){
            var msg = attr.ngConfirmClick || "Are you sure?";
            var clickAction = attr.confirmClicked;

            var factoryInstance = element.injector().get(scope.factoryName);


            element.bind('click', function(event){
                var dlg = factoryInstance.confirm('Confirmation Header', msg, 'static');
                dlg.result.then(function(btn){
                    console.log(scope);
                    scope.$eval(scope.confirmClicked);
                },function(btn){
                    console.log('cancelled click');
                });
            });
        }
        return {
            scope: {
                factoryName : '@',
                confirmClicked : '&'
            },
            link: Link
        };
    }
]);