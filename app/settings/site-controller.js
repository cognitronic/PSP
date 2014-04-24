/**
 * Created by Danny Schreiber on 4/2/14.
 */

'use strict'

app.controller('settings.SiteCtrl', function($scope, $location,$timeout, $rootScope, $routeParams, SiteService){
    $scope.site = {};
    $scope.dbtypes = ['firebird','mysql','sql2005', 'sql2008'];
    $scope.dbtype = $scope.dbtypes[0];
    $scope.isHidden = true;


    if($routeParams.id !== "new"){
        SiteService.getSiteById($routeParams.id)
            .then(function(data){
                $scope.site = data;
            });
    }

    $scope.saveSite = function(){
        SiteService.saveSite($scope.site)
            .then(function(data){
                $location.path('/sites/' + data.Id);
                $scope.isHidden = !$scope.isHidden;
            });
    }

    $scope.deleteSite = function(){
        if($routeParams.id !== "new"){
            SiteService.deleteSite($routeParams.id)
                .then(function(data){
                    $location.path('/sites');
                });
        }
    }

    $scope.returnToList = function(){
        $location.path('/sites');
    }
});