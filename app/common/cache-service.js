/**
 * Created by dschreiber on 8/12/2014.
 */

(function(){

    var CacheService = function(){

        var _cacheItems = {
            UserInfo: {
                userData: 'userData',
                userId: 'userId'
            },
            Reports: {
                selectedGsr: 'selectedGsr',
                selectedVolue: 'selectedVolume'
            },
            Profile: {
                loadedProfile: 'loadedProfile',
                allClnServices: 'clnServices',
                allNclnServices: 'nclnServices'
            }
        };

        var _setItem = function(key, val) {
            sessionStorage.setItem(key, JSON.stringify(val));
        };

        var _getItem = function(item) {
            return angular.fromJson(sessionStorage.getItem(item));
        };

        var _removeItem = function(item) {
            sessionStorage.removeItem(item);
        };


        return {
            setItem: _setItem,
            getItem: _getItem,
            removeItem: _removeItem,
            Items: _cacheItems
        }
    };

    angular.module('psp').factory('CacheService', [CacheService]);
})();
