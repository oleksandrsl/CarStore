(function () {
    'use strict'

    angular.module('app')
        .directive('statistic', Statistic)

    function Statistic() {
        return {
            templateUrl: 'app/components/management/views/statistic.view.html',
            restrict: 'EA',
            transclude: true,
            controller: 'StatisticController'
        }
    }
})()
