'use strict';
app.controller('ordersController', ['$scope', 'ordersService', function ($scope, ordersService) {

    $scope.orders = [];

    ordersService.getOrders().then(function (results) {

        $scope.orders = results.data;
  console.log(results.data);
    }, function (error) {
        console.log(error);
    });

}]);