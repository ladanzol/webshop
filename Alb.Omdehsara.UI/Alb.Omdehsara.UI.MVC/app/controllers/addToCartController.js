app.controller('AddToCartController', ['$injector', '$scope', '$rootScope', '$location', '$q', '$http', 'mainService', 'utilService', '$modalInstance', 'product', '$controller',
    function ($injector, $scope, $rootScope, $location, $q, $http, mainService, utilService , $modalInstance, product, $controller) {
        $controller('ControllerBase', {
        $scope: $scope,
        $location: $location,
        $q: $q
    });
    $scope.orderDetail = {};
    $scope.orderDetail.ProductTypeID = product.ID;
    $scope.orderDetail.ProductTitle = product.ProductTitle;
    $scope.orderDetail.UnitPrice = parseInt(product.Price);
    $scope.orderDetail.UnitQuantity = parseInt(product.UnitQuantity);
    $scope.orderDetail.UnitName = product.UnitName;
    $scope.orderDetail.MinOrderQuantity = parseInt(product.MinOrderQuantity);

    $scope.product = product;
    $scope.getProductTypeAvailableColors = function () {
        mainService.getProductTypeAvailableColors(product.ID).then(function (data) {
            $scope.colors = data;
        });
    }
    $scope.sizeSelected = function () {
        $scope.orderDetail.ColorID = null;
        $scope.orderDetail.ProductQuantity = 0;
        mainService.getProductAvailableColors($scope.orderDetail.ProductTypeID, $scope.orderDetail.SizeID).then(function (data) {
            $scope.colors = data;
        });
    }
    $scope.colorSelected = function () {
        $scope.orderDetail.ProductQuantity = 0;
        mainService.getProductByColorAndSize($scope.orderDetail.ProductTypeID, $scope.orderDetail.SizeID, $scope.orderDetail.ColorID).then(function (data) {
            $scope.orderDetail.ProductQuantity = parseInt(data.Quantity);
            $scope.orderDetail.ProductID = data.ID;
        });
    }
    $scope.getProductTypeAvailableSizes = function () {
        mainService.getProductTypeAvailableSizes(product.ID).then(function (data) {
            $scope.sizes = data;
        });
    }
    $scope.getProductTypeAvailableSizes();

    $scope.ok = function () {
        angular.forEach($scope.colors, function (item) {
            if ($scope.orderDetail.ColorID == item.ID) {
                $scope.orderDetail.ColorName = item.ColorName;
            }
        });
        angular.forEach($scope.sizes, function (item) {
            if ($scope.orderDetail.SizeID == item.ID) {
                $scope.orderDetail.SizeName = item.SizeName;
            }
        });
        $scope.orderDetail.Price = $scope.orderDetail.UnitPrice * $scope.orderDetail.Quantity;
        if (!$scope.orderDetail.Quantity ) {
            utilService.showMessage("لطفا تعداد مورد نظر خود را مشخص نمایید.");
        } else {
            $rootScope.$broadcast("NewOrderSubmited", $scope.orderDetail);
            $modalInstance.close($scope.orderDetail);
        }
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}]);
