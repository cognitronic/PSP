/**
 * Created by Danny Schreiber on 4/24/14.
 */


app.directive('ccConfirmDialog', [
    function(){

        function Link(scope, element, attr){
            var msg = attr.ccConfirmDialog || "Are you sure?";
            var header = attr.ccConfirmHeader || "Confirmation Header";
            console.log(attr.ccConfirmHeader);
            var clickAction = attr.confirmClicked;

            var factoryInstance = element.injector().get('$dialogs');


            element.bind('click', function(event){
                var dlg = factoryInstance.confirm(header, msg, 'static');
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
                confirmClicked : '&'
            },
            link: Link
        };
    }
]);

app.directive('ccNotifyDialog', [
    function(){
        function link(scope, element, attr){
            var msg = attr.ccNotifyDialog || 'Empty notification message';
            var header = attr.ccNotifyHeader || 'Notification';

            var dialogInstance = element.injector().get('$dialogs');
            element.bind('click', function(event){
                dialogInstance.notify(header, msg);
            })
        }
        return {
            link: link
        }
    }
]);

app.directive('ccErrorDialog', [
    function(){
        function link(scope, element, attr){
            var msg = attr.ccErrorDialog || 'Please, dont do that';
            var header = attr.ccErrorHeader || 'An Error Has Occured';

            var dialogInstance = element.injector().get('$dialogs');
            element.bind('click', function(event){
                dialogInstance.error(header, msg);
            })
        }
        return {
            link: link
        }
    }
]);

app.directive('ccWaitDialog', [
    function(){

        function Link(scope, element, attr){
            var msg = attr.ccWaitDialog || "Waiting on operation to complete. ";
            var header = attr.ccWaitHeader || "Please Wait";
            //var clickAction = attr.clickAction;
            var factoryInstance = element.injector().get('$dialogs');

            element.bind('click', function(event){
                var dlg = factoryInstance.wait(header, msg, 100);
                scope.$eval(scope.clickAction);
            });
        }
        return {
            scope: {
                clickAction: '&'
            },
            link: Link
        };
    }
]);