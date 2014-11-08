/**
 * Created by Danny Schreiber on 9/18/14.
 */

(function(){
    'use strict';

    var UtilityService = function(){

        var _toISODate = function(datestring){
            if(datestring){
                var date = new Date(datestring);
                return date.toISOString();
            } else{
                return '';
            }
        };

        var _formatDateNoTime = function(d) {
            if (!(d instanceof Date)) {
                console.log("Invalid date: " + d);
                return "";
            }
            return (d.getMonth()+1) + "/" + d.getDate() + "/" + d.getFullYear();
        };

        var _formatPaddedDateNoTime = function(d) {
            if (!(d instanceof Date)) {
                console.log("Invalid date: " + d);
                return "";
            }
            var _month = d.getMonth() + 1;
            var _day = d.getDate();

            if (_month < 10)
                _month = "0" + _month;

            if (_day < 10)
                _day = "0" + _day;

            return (_month + "/" + _day + "/" + d.getFullYear());
        };


        return {
            toISODate: _toISODate,
            formatDateNoTime: _formatDateNoTime,
            formatPaddedDateNoTime: _formatPaddedDateNoTime
        }
    };

    ramAngularApp.module.factory('UtilityService', [UtilityService]);
})();