/**
 * Created by Danny Schreiber on 4/24/14.
 */


app.directive('ccConfirmDialog', [
    function(){

        function Link(scope, element, attr){
            var msg = attr.ccConfirmDialog || "Are you sure?";
            var header = attr.ccConfirmHeader || "Confirmation Header";
            var clickAction = attr.confirmClicked;

            var factoryInstance = element.injector().get('dialogs');


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

            var dialogInstance = element.injector().get('dialogs');
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

            var dialogInstance = element.injector().get('dialogs');
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

app.directive('ccAddUser', [ function(){
    function link(scope, element, attrs){
        var dialogProvider = element.injector().get('dialogs');
        scope.modalData = {
            header: 'Add New User',
            dtDateRegistered: new Date(),
            registeredOpened: false,
            burble: 'ly gurgle'
        }
        element.bind('click', function(event){
            if(scope.ccEditable)
                scope.modalData.editingUser = scope.$eval(scope.ccEditable);
            var dlg = dialogProvider.create('/settings/modal-add-new-user.html','settings.UserCtrl', scope.modalData);
            dlg.result.then(function(){
                if(scope.ccPostSubmitAction){
                    scope.$eval(scope.ccPostSubmitAction);
                }
            },function(){
                if(scope.postCancelAction){
                    scope.$eval(scope.ccPostCancelAction);
                }
            });
        })
    }
    return {
        link: link,
        scope: {
            ccPostSubmitAction: '&',
            ccPostCancelAction: '&',
            ccEditable: '@'

        }
    }
}]);

app.directive('ccWhatsNew', function($compile, ReportsService){

});