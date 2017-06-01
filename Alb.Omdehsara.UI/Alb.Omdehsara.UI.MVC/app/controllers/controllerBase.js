function ControllerBase($scope, $location, $q, storeService, mainService) {
    $scope.baseUrl = tools.ajax.apiBaseUrl;
    $scope.getStores = function () {
        if (storeService) {
            storeService.getStores().then(function (data) {
                $scope.stores = data;
                console.log($scope.stores);
            });
        }
    }
    $scope.getStores();
    $scope.getShelves = function (store) {
        var defer = $q.defer();
        if (!store) {
            store = $scope.productLocation.StoreID;
        }
        storeService.getShelves(store).then(function (data) {
            $scope.shelves = data;
            defer.resolve();
            console.log($scope.shelves);
        });
        return defer.promise;
    }
    $scope.getBrands = function () {
        var defer = $q.defer();
        mainService.getBrands($scope.productModel.Product.CategoryID).then(function (data) {
            $scope.brands = data;
            defer.resolve();
        });
        return defer.promise;
    }
    $scope.getUnits = function () {
        var defer = $q.defer();
        mainService.getUnits().then(function (data) {
            $scope.units = data;
            defer.resolve();
        });
        return defer.promise;
    }
    $scope.getColors = function (categoryId) {
        var defer = $q.defer();
        mainService.getColors(categoryId).then(function (data) {
            $scope.colors = data;
            defer.resolve();
        });
        return defer.promise;
    }
    $scope.getSizes = function (categoryId) {
        var defer = $q.defer();
        mainService.getSizes(categoryId).then(function (data) {
            $scope.sizes = data;
            defer.resolve();
        });
        return defer.promise;
    }
}
app.controller('ControllerBase', ['$scope', '$location', '$q', 'storeService','mainService', ControllerBase]);
