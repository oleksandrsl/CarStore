(function () {
    'use strict';

    angular.module('app')
        .controller('ManagementController', ManagementController)
        .controller('CarsManagementController', CarsManagementController)

    CarsManagementController.$inject = ['$scope', '$rootScope', 'CarService', 'CarManagementService'];

    function ManagementController() {

    }

    function CarsManagementController($scope, $rootScope, CarService, CarManagementService) {
        $scope.newCar = {}
        $scope.carParams = {}

        getCars();
        getMakes();
        getBodyTypes();

        $scope.changeMake = function (make) {
            getModels(make.makeId);
        }

        $scope.addCar = function (car) {
            return CarManagementService.newCar(normalizedCar(car)).then(function (success) {
                $scope.newCar = {};
                getCars();
            }, function (error) {
                console.log(error);
            })
        }

        $scope.deleteCar = function (carId) {
            return CarManagementService.deleteCar(carId).then(function (success) {
                getCars();
            }, function (error) {
                console.log(error);
            });
        }

        function getCars() {
            return CarService.getCars().then(function (data) {
                $scope.cars = data;
            }, function (error) {
                console.log(error);
            });
        }

        function getMakes() {
            return CarService.getMakes().then(function (data) {
                $scope.carParams.makes = data;
            }, function(error) {
                console.log(error);
            });
        }

        function getModels(makeId) {
            return CarService.getModels(makeId).then(function (data) {
                $scope.carParams.models = data;
            }, function(error) {
                console.log(error);
            });
        }

        function getBodyTypes() {
            return CarService.getBodyTypes().then(function (data) {
                $scope.carParams.bodyTypes = data;
            }, function (error) {
                console.log(error);
            });
        }

        function normalizedCar(car) {
            return {
                modelId: car.model.modelId,
                bodyTypeId: car.bodyType.bodyTypeId,
                price: car.price,
                engine: car.engine,
                year: car.year
            }
        }
    }

})()
