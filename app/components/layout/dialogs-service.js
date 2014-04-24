/**
 * Created by Danny Schreiber on 4/23/14.
 */

angular.module('dialogs.services',['ui.bootstrap.modal','dialogs.controllers'])

/**
 * Dialogs Service
 */
    .factory('$dialogs',['$modal','defaultStrings',function($modal,defaultStrings){
        return {
            error : function(header,msg,static){
                return $modal.open({
                    templateUrl : '/dialogs/error.html',
                    controller : 'errorDialogCtrl',
                    backdrop: (static ? 'static' : true),
                    keyboard: (static ? false: true),
                    resolve : {
                        header : function() { return angular.copy(header); },
                        msg : function() { return angular.copy(msg); }
                    }
                }); // end modal.open
            }, // end error

            wait : function(header,msg,progress,static){
                return $modal.open({
                    templateUrl : '/dialogs/wait.html',
                    controller : 'waitDialogCtrl',
                    backdrop: (static ? 'static' : true),
                    keyboard: (static ? false: true),
                    resolve : {
                        header : function() { return angular.copy(header); },
                        msg : function() { return angular.copy(msg); },
                        progress : function() { return angular.copy(progress); }
                    }
                }); // end modal.open
            }, // end wait

            notify : function(header,msg,static){
                return $modal.open({
                    templateUrl : '/dialogs/notify.html',
                    controller : 'notifyDialogCtrl',
                    backdrop: (static ? 'static' : true),
                    keyboard: (static ? false: true),
                    resolve : {
                        header : function() { return angular.copy(header); },
                        msg : function() { return angular.copy(msg); }
                    }
                }); // end modal.open
            }, // end notify

            confirm : function(header,msg,static){
                return $modal.open({
                    templateUrl : '/components/layout/dialog-confirm.html',
                    controller : 'confirmDialogCtrl',
                    backdrop: (static ? 'static' : true),
                    keyboard: (static ? false: true),
                    resolve : {
                        header : function() { return angular.copy(header); },
                        msg : function() { return angular.copy(msg); }
                    }
                }); // end modal.open
            }, // end confirm

            create : function(url,ctrlr,data,opts){
                opts = angular.isDefined(opts) ? opts : {};
                var k = (angular.isDefined(opts.keyboard)) ? opts.keyboard : true; // values: true,false
                var b = (angular.isDefined(opts.backdrop)) ? opts.backdrop : true; // values: 'static',true,false
                var w = (angular.isDefined(opts.windowClass)) ? opts.windowClass : 'dialogs-default'; // additional CSS class(es) to be added to a modal window
                return $modal.open({
                    templateUrl : url,
                    controller : ctrlr,
                    keyboard : k,
                    backdrop : b,
                    windowClass: w,
                    resolve : {
                        data : function() { return angular.copy(data); }
                    }
                }); // end modal.open
            }, // end create

            translate : function(newStrings){
                return angular.extend(defaultStrings,newStrings);
            } // end translate
        };
    }]); // end $dialogs / dialogs.services

angular.module("dialogs.services").value("defaultStrings",{
    error: "Error",
    errorMessage: "An unknown error has occurred.",
    close: "Close",
    pleaseWait: "Please Wait",
    pleaseWaitEllipsis: "Please Wait...",
    pleaseWaitMessage: "Waiting on operation to complete.",
    percentComplete: "% Complete",
    notification: "Notification",
    notificationMessage: "Unknown application notification.",
    confirmation: "Confirmation",
    confirmationMessage: "Confirmation required.",
    ok: "OK",
    yes: "Yes",
    no: "No"
});