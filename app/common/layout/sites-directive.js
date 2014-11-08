/**
 * Created by Danny Schreiber on 10/19/2014.
 */
(function(){
    'use strict';

    var link = function(scope, element, attr){

        scope.selectLabel = scope.defaultText;
        scope.cancelClose = function($event){
            $event.stopPropagation();
        };

        scope.setLabel = function() {
            if (typeof (scope.ngModel) =="undefined" || !scope.ngModel || scope.ngModel.length < 1) {

                scope.selectLabel = attr.defaultText;

            } else {
                var allItemsString = '';
                var selectedItemsCount=scope.ngModel.length;
                if(selectedItemsCount<3){
                    angular.forEach(scope.ngModel, function (item) {
                        allItemsString += item[scope.textField].toString() + ', ';
                    });
                }else{
                    allItemsString=selectedItemsCount+" selected!";
                }
                scope.selectLabel = allItemsString.replace(/[,]\s$/,'');
            }
        };
        scope.setLabel();

        scope.setAllCheckboxes = function(){
            console.log(scope.ngModel);
            angular.forEach(scope.ngModel, function (item) {
                console.log(item);
                scope.setCheckboxChecked(item);
            });
        };

        scope.setCheckboxChecked = function (_item) {
            var found = false;
            angular.forEach(scope.ngModel, function (item) {
                if (!found) {
                    if (_item[scope.valueField].toString() === item[scope.valueField].toString()) {
                        found=true;
                    }
                }
            });
            return found;
        };

        scope.siteSelected = function (_item) {
            var found = false;
            if (typeof(scope.ngModel) != "undefined" && scope.ngModel) { //make sure there is a model
                _item.ticked = !_item.ticked;
                if(_item.name == 'All'){
                    scope.ngModel = []; //if all sites clicked then clear the model
                    if(_item.ticked){ //if all sites is checked then iterate through and add all items to array
                        for(var i = 0, l = scope.items.length; i < l; i++){
                            scope.items[i].ticked = true;
                            scope.ngModel.push(scope.items[i]);
                        }
                    } else { // else remove the check, and the model is already set to an empty array
                        for(var i = 0, l = scope.items.length; i < l; i++){
                            scope.items[i].ticked = false;
                        }
                    }
                } else {
                    if(scope.ngModel.length > 0){
                        for(var i = scope.ngModel.length; i--;) {
                            found = false;
                            if (_item[scope.valueField].toString() === scope.ngModel[i][scope.valueField].toString()) {
                                found = true; //if selected item is in the model already set to true
                                if (!_item.ticked) { // if selected item is not checked, remove from model
                                    var index = scope.ngModel.indexOf(scope.ngModel[i]);
                                    scope.ngModel.splice(index, 1);
                                }
                            }
                        }
                    }
                }
            } else {
                scope.ngModel = [];
            }
            if (!found && _item.name != 'All' && _item.ticked) { // if the item was not already found in model, and it's
                scope.ngModel.push(_item);                       // its checked, and not 'select all', then add it
            }
            scope.setLabel();
        };
    };

    var pspSitesSelect = function(){
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
            template: '<div class="btn-group" dropdown><button class="btn btn-info button-label" style="width: 190px;">{{selectLabel}}</button><button class="btn btn-info dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></button>'
                + '<ul class="dropdown-menu drop-down-menu" role="menu" style="max-height:350px;overflow-y:scroll">' +
                '<li ng-repeat="item in items" ng-click="cancelClose($event)">' +
                '<div class="input-group"><input type="checkbox" ng-checked="item.ticked"  ng-click="siteSelected(item,$index)"><b> {{item[textField]}}</b></div>' +
                '</li></ul>'
        };
    };

    ramAngularApp.module.directive('pspSitesSelect', pspSitesSelect);
})();