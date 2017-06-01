app.controller('ProductController', ['$scope', '$injector', '$routeParams', '$location', '$q', '$timeout', 'mainService', 'storeService', 'locService', '$controller', 'utilService',
function ($scope, $injector, $routeParams, $location, $q, $timeout, mainService, storeService, locService, $controller, utilService) {
    $controller('ControllerBase',{
        $scope: $scope,
        $location: $location,
        $q: $q,
        storeService: storeService
    });
    $scope.pageIndex = 1;
    $scope.itemPerPage = 20;
    $scope.showSearhForm = true;
    $scope.productLocation = {};
    if ($routeParams.productId) {
        $scope.showSearhForm = false;
    }
    $scope.getProducts = function () {
        var searhParams = { ProductTypeID: $routeParams.productTypeId, PageIndex: $scope.pageIndex, ProductID: $routeParams.productId };
        return storeService.searchProductLocation(searhParams).then(function (data) {
            $scope.products = data.Data;
            $scope.totalRecords = data.TotalRecords;
            return;
        });
    }

    if ($routeParams.productTypeId) {
        $scope.getProducts().then(function () {
            $scope.getColors($routeParams.categoryId);
            $scope.getSizes($routeParams.categoryId);
        });
    }

    $scope.colors = [];
    $scope.getColors = function () {
        var defer = $q.defer();
        mainService.getColors($routeParams.categoryId).then(function (data) {
            $scope.colors = data;
            defer.resolve();
        });
        return defer.promise;
    }
    $scope.sizes = [];
    $scope.getSizes = function () {
        var defer = $q.defer();
        mainService.getSizes($routeParams.categoryId).then(function (data) {
            $scope.sizes = data;
            defer.resolve();
        });
        return defer.promise;
    }

    $scope.submitProduct = function () {
        $scope.Product.ProductTypeID = $routeParams.productTypeId;
        $scope.storeProduct();
    }

    $scope.storeProduct = function () {
        $scope.productLocation.ShelfID = $scope.productLocation.Shelf.ID;
        $scope.productLocation.ProductTypeID = $routeParams.productTypeId;
        mainService.storeProduct($scope.productLocation).then(function (result) {
            $scope.getProducts();
            utilService.showMessage("عملیات انجام شد");
        });
    }

    $scope.setProduct = function (product) {
        $scope.Product = product;
    }
    //$scope.updateProduct = function () {
    //    mainService.updateProduct($scope.Product).then(function (result) {
    //        $scope.products[$scope.products.indexOf($scope.Product)] = result.product;
    //        $scope.Product = result.product;
    //        utilService.showMessage("محصول شما ذخیره شد");
    //    });
    //}
    $scope.deleteProduct = function (product) {
        if (confirm("آیا مطمئن هستید؟")) {
            mainService.deleteProduct(product.ID).then(function (result) {
                    $scope.products.splice($scope.products.indexOf(product), 1);
                    utilService.showMessage("محصول حذف شد");
                    $scope.$apply();
            });
        }
    }
}]);

