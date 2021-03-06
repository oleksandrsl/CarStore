(function () {
    'use strict'

    angular.module('app')
        .controller('CarController', CarController)

    CarController.$inject = ['$scope', '$routeParams', '$window', '$location', 'CarService'];

    function CarController($scope, $routeParams, $window, $location, CarService) {
        getCar();

        $scope.back = function () {
            $window.history.back();
        }

        $scope.cancelOrder = function () {
            $scope.order = angular.copy({});
        }

        $scope.orderCar = function (order) {
            return CarService.orderCar($routeParams.id, order).then(function (data) {

            }, function(error){

            });
        }

        function getCar() {
            return CarService.getCar($routeParams.id).then(function (data) {
                $scope.car = data;
            }, function (error) {
                console.log(error);
            })
        }
    }
})()
