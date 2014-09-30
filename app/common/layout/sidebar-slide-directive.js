/**
 * Created by danny_000 on 3/25/14.
 */

ramAngularApp.module.directive("btnToggleSidebar", function(){

    function link($scope, element, attributes){
        alert('hi');
        var expression = attributes.btnToggleSidebar;

        var duration = (attributes.slideShowDuration || "fast");

        var btn = angular.element($('#toggleSideNav'));
        btn.bind('click', toggle);
        opened = true;
        function toggle(){
            opened = !opened;
            alert(opened);
        }

    }
    return ({
        link: link,
        restrict: "A"
    });
});