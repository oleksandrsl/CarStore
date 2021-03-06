(function () {
    'use strict'

    angular.module('app')
        .factory('CarManagementService', CarManagementService);

    CarManagementService.$inject = ['$http', 'adminKey']

    function CarManagementService($http, adminKey) {

        function newCar(car) {
            return $http({ method: 'post', url: '/api/car', data: car, params: { key: adminKey } }).
                then(function success(response) {
                    return response.data;
                }, function error(response) {
                    return response.error;
                });
        }

        function editCar(car) {
            return $http({ method: 'put', url: '/api/car/' + car.carId, data: car, params: { key: adminKey } }).
                then(function success(response) {
                    return response.data;
                }, function error(response) {
                    return response.error;
                });
        }

        function deleteCar(carId) {
            return $http({ method: 'delete', url: '/api/car/' + carId, params: { key: adminKey } }).
                then(function success(response) {
                    return response.data;
                }, function error(response) {
                    return response.error;
                });
        }

        return {
            newCar: newCar,
            editCat: editCar,
            deleteCar: deleteCar
        }
    }
})()
