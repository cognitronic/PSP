/**
 * Created by Danny Schreiber on 4/10/14.
 */

'use strict'

angular.module('filters.app', [])
    .filter('convertToISODate', function(){
   return function(input){
       var filtered = []
       angular.forEach(input, function(item){
          item.birthdate = (new Date(item.birthdate).getMonth() + 1) + '/' + (new Date(item.birthdate).getDate());
          item.dateregistered = new Date(item.dateregistered).toLocaleDateString();
       });
       return input;
   }
});