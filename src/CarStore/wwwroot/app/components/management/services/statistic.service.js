(function () {
    'use strict'

    angular.module('app')
        .factory('StatisticService', StatisticService);

    StatisticService.$inject = ['$http'];

    function StatisticService($http) {

        function getSalesStatistic(year) {
            return $http({ method: 'get', url: '/api/statistic/sales/' + year }).
                then(function success(response) {
                    return response.data;
                }, function error(response) {
                    return response.error;
                });
        }

        function getPopularCars(count) {
            return $http({ method: 'get', url: '/api/statistic/sales/top/' + count }).
                then(function success(response) {
                    return response.data;
                }, function error(response) {
                    return response.error;
                });
        }

        return {
            getSalesStatistic: getSalesStatistic,
            getPopularCars: getPopularCars
        }
    }
})()
