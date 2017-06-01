app.controller('ProductViewController', ['$injector', '$scope', '$location', '$q', '$http', 'mainService', 'utilService', 'locService', '$controller', '$routeParams', '$modal',
    function ($injector, $scope, $location, $q, $http, mainService, utilService, locService, $controller, $routeParams, $modal) {
        //$controller('ControllerBase', {
        //    $scope: $scope,
        //    $location: $location,
        //    $q: $q
        //});
        mainService.getSimpleProductType($("#Id").val()).then(function (data) {
            $scope.product = data;
            console.log(data);
        });
        $scope.addToCart = function () {
            var modalInstance = $modal.open({
                templateUrl: tools.ajax.apiBaseUrl+'app/partials/addToCartModal.html?v=1',
                controller: 'AddToCartController',
                size: 'sm',
                resolve: {
                    product: function () {
                        return $scope.product;
                    }
                }
            });
            modalInstance.result.then(function (orderDetail) {
            }, function () {
            });
        }
    }]);
