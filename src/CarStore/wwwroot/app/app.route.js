(function () {
    'use strict';

    angular.module('app').config(configure);

    function configure($routeProvider, $locationProvider) {
        $routeProvider
            .when('/management', {
                templateUrl: '/app/components/management/views/management.view.html',
                controller: 'ManagementController'
            })
            .when('/', {
                templateUrl: '/app/components/store/views/store.view.html',
                controller: 'StoreController'
            })
            .when('/car/:id', {
                templateUrl: '/app/components/store/views/car.view.html',
                controller: 'CarController'
            })
            .when('/management/orders', {
                templateUrl: '/app/components/management/views/orders.view.html',
                controller: 'OrdersController'
            })
            .when('/management/cars', {
                templateUrl: '/app/components/management/views/cars.view.html',
                controller: 'CarsManagementController'
            })
            .otherwise({
                redirectTo: '/'
            });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

    }
})()
