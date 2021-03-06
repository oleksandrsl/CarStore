(function () {
    'use strict'

    angular.module('app')
        .controller('StatisticController', StatisticController);

    StatisticController.$inject = ['$scope', 'StatisticService'];

    function StatisticController($scope, StatisticService) {
        const currentYear = new Date().getFullYear();
        const topCarsCount = 5;
        $scope.salesQuantity = new Array(12).fill(0);
        $scope.salesSum = 0;
        $scope.topCars = [];
        $scope.carsCounts = [];
        $scope.months = ["Январь", "Февраль", "Март",
            "Апрель", "Май", "Июнь", "Июль",
            "Август", "Сентябрь", "Октябрь",
            "Ноябрь", "Декабрь"];

        getSalesStatistic(currentYear);
        getPopularCars(topCarsCount);


        function getSalesStatistic(year) {
            StatisticService.getSalesStatistic(year).then(function (success) {
                var salesStatistic = success
                preapereQuantity(salesStatistic);
            });
        }

        function getPopularCars(count) {
            StatisticService.getPopularCars(count).then(function (success) {
                preapereTopCars(success)
            });
        }

        function preapereTopCars(cars) {
            cars.forEach(function (c) {
                $scope.topCars.push(c.make + ' ' + c.model);
                $scope.carsCounts.push(c.sales)
            }, this);
        }

        function preapereQuantity(statistic) {
            statistic.forEach(function (s) {
                $scope.salesQuantity[s.month - 1] = s.quantity;
                $scope.salesSum += s.sum;
            }, this);
        }
    }

})()
