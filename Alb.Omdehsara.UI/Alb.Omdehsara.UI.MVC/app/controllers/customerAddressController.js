app.controller('CustomerAddressController', ['$injector', '$scope', '$location', '$q', '$http', 'mainService', 'utilService', 'locService', '$controller', '$routeParams' ,'$timeout',
    function ($injector, $scope, $location, $q, $http, mainService, utilService, locService, $controller, $routeParams,$timeout) {
        //$controller('ControllerBase', {
        //    $scope: $scope,
        //    $location: $location,
        //    $q: $q
        //});
        $scope.address = {};
        mainService.getCustomerAddress().then(function (data) {
            if (data) {
                $scope.address = data;
                if (data.ProvinceID) {
                    $scope.getCities();
                }
            }
        });

        locService.getProvinces().then(function (data) {
            $scope.provinces = data;
        });


        $scope.getCities = function () {
            locService.getCities($scope.address.ProvinceID).then(function (data) {
                $scope.cities = data;
            });
        }
        $scope.submitAddress = function () {
            mainService.submitAddress($scope.address).then(function () {
                //utilService.path('/payorder');
                utilService.path('/cart/1');
            });
        }


    }]);
