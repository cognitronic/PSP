/**
 * Created by Danny Schreiber on 4/24/14.
 */
angular.module('dialogs.controllers',['ui.bootstrap.modal'])

/**
 * Error Dialog Controller
 */
    .controller('errorDialogCtrl',['$scope','$modalInstance','header','msg','defaultStrings',function($scope,$modalInstance,header,msg,defaultStrings){
        //-- Variables -----//

        $scope.header = (angular.isDefined(header)) ? header : defaultStrings.error;
        $scope.msg = (angular.isDefined(msg)) ? msg : defaultStrings.errorMessage;
        $scope.defaultStrings = defaultStrings;

        //-- Methods -----//

        $scope.close = function(){
            $modalInstance.close();
            $scope.$destroy();
        }; // end close
    }]) // end ErrorDialogCtrl

/**
 * Wait Dialog Controller
 */
    .controller('waitDialogCtrl',['$scope','$modalInstance','$timeout','header','msg','progress','defaultStrings',function($scope,$modalInstance,$timeout,header,msg,progress,defaultStrings){
        //-- Variables -----//

        $scope.header = (angular.isDefined(header)) ? header : defaultStrings.pleaseWaitEllipsis;
        $scope.msg = (angular.isDefined(msg)) ? msg : defaultStrings.pleaseWaitMessage;
        $scope.progress = (angular.isDefined(progress)) ? progress : 100;
        $scope.defaultStrings = defaultStrings;

        //-- Listeners -----//

        // Note: used $timeout instead of $scope.$apply() because I was getting a $$nextSibling error

        // close wait dialog
        $scope.$on('dialogs.wait.complete',function(){
            $timeout(function(){ $modalInstance.close(); $scope.$destroy(); });
        }); // end on(dialogs.wait.complete)

        // update the dialog's message
        $scope.$on('dialogs.wait.message',function(evt,args){
            $scope.msg = (angular.isDefined(args.msg)) ? args.msg : $scope.msg;
        }); // end on(dialogs.wait.message)

        // update the dialog's progress (bar) and/or message
        $scope.$on('dialogs.wait.progress',function(evt,args){
            $scope.msg = (angular.isDefined(args.msg)) ? args.msg : $scope.msg;
            $scope.progress = (angular.isDefined(args.progress)) ? args.progress : $scope.progress;
        }); // end on(dialogs.wait.progress)

        //-- Methods -----//

        $scope.getProgress = function(){
            return {'width': $scope.progress + '%'};
        }; // end getProgress
    }]) // end WaitDialogCtrl

/**
 * Notify Dialog Controller
 */
    .controller('notifyDialogCtrl',['$scope','$modalInstance','header','msg','defaultStrings',function($scope,$modalInstance,header,msg,defaultStrings){
        //-- Variables -----//

        $scope.header = (angular.isDefined(header)) ? header : defaultStrings.notification;
        $scope.msg = (angular.isDefined(msg)) ? msg : defaultString.notificationMessage;
        $scope.defaultStrings = defaultStrings;

        //-- Methods -----//

        $scope.close = function(){
            $modalInstance.close();
            $scope.$destroy();
        }; // end close
    }]) // end WaitDialogCtrl

/**
 * Confirm Dialog Controller
 */
    .controller('confirmDialogCtrl',['$scope','$modalInstance','header','msg','defaultStrings',function($scope,$modalInstance,header,msg,defaultStrings){
        //-- Variables -----//

        $scope.header = (angular.isDefined(header)) ? header : defaultStrings.confirmation;
        $scope.msg = (angular.isDefined(msg)) ? msg : defaultStrings.confirmationMessage;
        $scope.defaultStrings = defaultStrings;

        //-- Methods -----//

        $scope.no = function(){
            $modalInstance.dismiss('no');
        }; // end close

        $scope.yes = function(){
            $modalInstance.close('yes');
        }; // end yes
    }]); // end ConfirmDialogCtrl / dialogs.controllers