/**
 * Created by danny_000 on 3/23/14.
 */
ramAngularApp.module.directive('requestVerificationToken', ['$http', function($http){
    return function(scope, element, attrs){
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.requestVerificationToken || "no request verification token";
    };
}]);