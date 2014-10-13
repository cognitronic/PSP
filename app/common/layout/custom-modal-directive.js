/**
 * Created by Danny Schreiber on 9/26/14.
 */
(function(){
    'use strict';

    var link = function(scope, element, attrs){
        var dialogProvider = element.injector().get('dialogs');
        var templateFile = attrs.ramCustomModal;
        var controller = attrs.ramModalController;
        var title = attrs.ramModalTitle || 'Prime Shine';
        scope.modalData = {
            header:   title
        };
        element.bind('click', function(event){
            if(scope.ramOnLoadModalAction)
                scope.$eval(scope.ramOnLoadModalAction);
            var dialog = dialogProvider.create(templateFile, controller, scope.modalData);
            dialog.result.then(function(){
                if(scope.ramPostSubmitAction){
                    scope.$eval(scope.ramPostSubmitAction);
                }
            }, function(){
                if(scope.ramPostCancelAction){
                    scope.$eval(scope.ramPostCancelAction);
                }
            });
        });
    };

    var ramCustomModal = function(){
        return {
            restrict: 'A',
            link:link,
            scope: {
                ramPostSubmitAction: '&',
                ramPostCancelAction: '&',
                ramOnLoadModalAction: '&'
            }
        }
    };

    ramAngularApp.module.directive('ramCustomModal', ramCustomModal);
})();