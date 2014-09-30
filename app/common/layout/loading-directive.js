/**
 * Created by Danny Schreiber on 4/7/14.
 */

ramAngularApp.module.directive('loading', function () {
    return {
        restrict: 'E',
        replace:true,
        template: '<div class="loadingContainer"><div class="loading"><img src="../../assets/img/spinner-blue.gif" width="60" height="60" /> LOADING...</div></div>',
        link: function (scope, element, attr) {
            scope.$watch('loading', function (val) {
                if (val)
                    $(element).show();
                else
                    $(element).hide();
            });
        }
    }
})