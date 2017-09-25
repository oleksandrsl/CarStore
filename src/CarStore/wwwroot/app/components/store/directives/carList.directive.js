(function () {
    'use strict'
    angular.module('app')
        .directive('carList', carList);

    function carList() {
        return {
            templateUrl: 'app/components/store/views/carList.view.html',
            restrict: 'EA',
            transclude: true
        };
    }
})()
