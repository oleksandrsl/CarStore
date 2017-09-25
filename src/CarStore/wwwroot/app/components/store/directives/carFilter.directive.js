(function(){
    'use strict'

    angular.module('app')
    .directive('carFilter', carFilter);

    function carFilter () {
        return {
            templateUrl: 'app/components/store/views/carFilter.view.html',
            restrict: 'EA',
            transclude: true
        }
    }

})()
