app.controller('OrderTransportController', ['$injector', '$scope', '$location', '$q', '$http', 'mainService', 'utilService', 'locService', '$controller', '$routeParams', '$timeout',
    function ($injector, $scope, $location, $q, $http, mainService, utilService, locService, $controller, $routeParams, $timeout) {
        $scope.transportTypes = [];
        mainService.getOrderTransportTypes().then(function (data) {
            console.log(data);
            $scope.transportTypes = data;
        });
        $scope.selectedTransport = {};
        $scope.setOrderTransPort = function () {
            mainService.setOrderTransPort($scope.selectedTransport.ID).then(function () {
                utilService.path('/payorder');
            });
        }
    }]);
