/**
 * Created by danny_000 on 3/25/14.
 */
app.controller('settings.SitesCtrl', function($scope, $rootScope, $location, AuthService, SiteService, Paginator){

    $scope.pagination = Paginator.getNew(5);

    SiteService.getSites().then(function(data){
        $scope.sites = data;
        $scope.pagination.numPages = Math.ceil($scope.sites.length/$scope.pagination.perPage);
    });

    $scope.AddSite = function(){
        $location.path('/settings/sites/new');
    }

    $scope.deleteSite = function(site){
        var deleteSite = confirm('Are you sure you want to delete site, this cannot be undone?');
        if(deleteSite){
            site.sid = site.Id;
            SiteService.deleteSite(site).then(function(data){
                SiteService.getSites().then(function(data){
                    $scope.sites = data;
                });
            });
        }
    }
});
