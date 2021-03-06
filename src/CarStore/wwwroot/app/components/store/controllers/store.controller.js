(function () {
    'use strict';

    angular.module('app')
        .controller('StoreController', StoreController)

    StoreController.$inject = ["$scope", "$location", "CarService"];

    function StoreController($scope, $location, CarService) {

        var defaultFilter = {
            make: null,
            model: null,
            minPrice: null,
            maxPrice: null
        }

        getCars();
        getMakes();

        $scope.filterProps = angular.copy(defaultFilter);

        $scope.changeMake = function (make) {
            getModels(make.makeId);
        }

        $scope.clearFilter = function () {
            $scope.filterProps = angular.copy(defaultFilter);
            $scope.applyFilter($scope.filterProps);
        }

        $scope.applyFilter = function (params) {
            getCars({
                MakeId: params.make ? params.make.makeId : null,
                ModelId: params.model ? params.model.modelId : null,
                MinPrice: params.minPrice,
                MAxPrice: params.maxPrice
            });
        }

        $scope.showDeteils = function (car) {
            $location.path('/car/' + car.carId)
        }

        function getCars(params) {
            return CarService.getCars(params).then(function (data) {
                $scope.cars = data;
            })
        }

        function getMakes() {
            return CarService.getMakes().then(function (data) {
                $scope.makes = data;
            })
        }

        function getModels(makeId) {
            return CarService.getModels(makeId).then(function (data) {
                $scope.models = data;
            })
        }
    }
})()
