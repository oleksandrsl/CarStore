(function () {
    'use strict';

    angular.module('app')
        .factory('CarService', CarService);

    CarService.$inject = ['$http'];

    function CarService($http) {

        function getCars(filterParams) {
            return $http({ method: 'GET', url: '/api/car', params: filterParams }).
                then(function success(response) {
                    return response.data;
                });
        };

        function getCar(id) {
            return $http({ method: 'GET', url: '/api/car/' + id }).
                then(function success(response) {
                    return response.data;
                });
        }

        function getMakes() {
            return $http({ method: 'GET', url: '/api/makes' }).
                then(function success(response) {
                    return response.data;
                });
        }

        function getModels(makeId) {
            return $http({ method: 'GET', url: '/api/makes/' + makeId + '/models' }).
                then(function success(response) {
                    return response.data;
                });
        }

        function orderCar(id, order) {
            order.carId = id;
            return $http({ method: 'post', url: '/api/order', data: order }).
                then(function success(response) {
                    return response.data;
                });
        }

        function getBodyTypes() {
            return $http({ method: 'GET', url: '/api/body' }).
            then(function success(response) {
                return response.data;
            });
        }

        return {
            getCars: getCars,
            getCar: getCar,
            getMakes: getMakes,
            getModels: getModels,
            orderCar: orderCar,
            getBodyTypes: getBodyTypes
        }
    }
})()
