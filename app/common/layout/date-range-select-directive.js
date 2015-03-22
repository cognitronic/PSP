/**
 * Created by Danny Schreiber on 10/13/2014.
 */
(function(){
    'use strict';

    var link = function(scope, element, attr){

        scope.selectLabel = scope.defaultText;
        scope.cancelClose = function($event){
            $event.stopPropagation();
        };

        scope.dateSelected = function (_item) {
            if (typeof(scope.ngModel) != "undefined" && scope.ngModel) {
                scope.ngModel = _item;
                scope.selectLabel = scope.ngModel.name;
            }
        };
    };

    var pspDateRangeSelect = function(){
        return {
            restrict: 'EA',
            link:link,
            require:'^ngModel',
            scope: {
                items: '=',
                textField: '@',
                valueField: '@',
                ngModel: '=',
                showToday: '=',
                defaultText: '@'
            },
            template: '<div class="btn-group" dropdown><button class="btn btn-info button-label" style="width: 140px;">{{selectLabel}}</button><button class="btn btn-info dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></button>'
                + '<ul class="dropdown-menu drop-down-menu" role="menu" style="max-height:300px;overflow-y:scroll"><li ng-repeat="item in items"  ng-click="dateSelected(item)">' +
                '<div class="input-group"><b>{{item[textField]}}</b></div>' +
                '</li></ul>'
        };
    };

    ramAngularApp.module.directive('pspDateRangeSelect', pspDateRangeSelect);
})();
