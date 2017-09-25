(function () {
    'use strict'
    angular.module('app')
        .directive('mainMenu', Menu);

    function Menu() {
        return {
            templateUrl: 'app/components/management/views/menu.view.html',
            restrict: 'EA'
        }
    }
})()
