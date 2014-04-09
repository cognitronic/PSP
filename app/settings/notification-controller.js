/**
 * Created by Danny Schreiber on 4/6/14.
 */

app.controller('settings.NotificationCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, NotificationService){
    $scope.currentNotification = {};
    $scope.isHidden = true;
    $scope.recipients = [];
    $scope.bccemails = [];
    $scope.newRecipient = '';
    $scope.newBccRecipient = '';

    $scope.saveNotification = function(){
        $scope.currentNotification.recipients = $scope.recipients;
        NotificationService.saveNotification($scope.currentNotification)
            .then(function(data){
                $location.path('/notifications/' + data.Id);
                $scope.isHidden = !$scope.isHidden;
            });
    }

    $scope.runNotification = function(){
        console.log($scope.currentNotification.Id + ", " + $scope.currentNotification.Id);
        var notificationParam = {
            notificationId: $scope.currentNotification.Id,
            reportDate: $scope.dt.toLocaleDateString()
        };
        NotificationService.runNotification(notificationParam)
            .then(function(data){
               console.log(data);
            });
    }

    if($routeParams.id !== "new"){
        NotificationService.getNotificationById($routeParams.id)
            .then(function(data){
                $scope.currentNotification = data;
                $scope.recipients = $scope.currentNotification.recipients;
                $scope.bccemails = $scope.currentNotification.bccemails;
            });
    }

    $scope.removeRecipient = function(idx){
        console.log(idx);
        $scope.recipients.splice(idx, 1);
        console.log($scope.recipients.join(','));
        $scope.saveNotification();
    }

    $scope.addRecipient = function(){
        console.log($scope.newRecipient);
        if($scope.newRecipient !== ''){
            $scope.recipients.push($scope.newRecipient);
            $scope.saveNotification();
            $scope.newRecipient = '';
        }
    }


    $scope.removeBccRecipient = function(idx){
        console.log(idx);
        $scope.bccemails.splice(idx, 1);
        console.log($scope.bccemails.join(','));
        $scope.saveNotification();
    }

    $scope.addBccRecipient = function(){
        console.log($scope.newBccRecipient);
        if($scope.newBccRecipient !== ''){
            $scope.bccemails.push($scope.newBccRecipient);
            $scope.saveNotification();
            $scope.newBccRecipient = '';
        }
    }


    $scope.returnToList = function(){
        $location.path('notifications');
    }

    $scope.toggleMax = function() {
        $scope.maxDate = ( $scope.maxDate ) ? null : new Date();
    };
    $scope.toggleMax();



    $scope.today = function() {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.showWeeks = true;
    $scope.toggleWeeks = function () {
        $scope.showWeeks = ! $scope.showWeeks;
    };

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function(date, mode) {
        return ( mode === 'day' && ( date.getDay() === 0 || date.getDay() === 6 ) );
    };

    $scope.toggleMin = function() {
        $scope.minDate = ( $scope.minDate ) ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        'year-format': "'yyyy'",
        'starting-day': 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'MM/dd/yyyy', 'shortDate'];
    $scope.format = $scope.formats[1];
});