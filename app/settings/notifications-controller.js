/**
 * Created by danny_000 on 3/25/14.
 */
app.controller('settings.NotificationsCtrl', function($scope, $rootScope, $routeParams, $location, AuthService, NotificationService){

    $scope.currentNotification = {};

    NotificationService.getNotifications().then(function(data){
        $scope.notifications = data;
    });

    $scope.SaveNotification = function(){

    }

//    $scope.deleteSite = function(site){
//        var deleteSite = confirm('Are you sure you want to delete site, this cannot be undone?');
//        if(deleteSite){
//            site.sid = site.Id;
//            SiteService.deleteSite(site).then(function(data){
//                SiteService.getSites().then(function(data){
//                    $scope.sites = data;
//                });
//            });
//        }
//    }

    if($routeParams.id !== "new"){
        NotificationService.getNotificationById($routeParams.id)
            .then(function(data){
                $scope.currentNotification = data;
            });
    }



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