/**
 * Created by Danny Schreiber on 3/30/14.
 */
'use strict'

angular.module('service.user', [])
    .factory('UserService', function($http, $rootScope, $location, APP_SETTINGS){

        return{
            createUser: function(){

            },
            getUser: function(usr){

            },
            getUserByEmail: function(email){

            },
            getUserById: function(id){

            },
            getAllUsers: function(){

            },
            getUsersByRole: function(){

            },
            deleteUser: function(usr){

            }
        }
    });