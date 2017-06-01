app.controller('StoreController', ['$injector', '$scope', '$location', '$q', '$timeout', 'mainService', 'locService', 'storeService', 'utilService', '$modal','$controller',
    StoreController]);

function StoreController($injector, $scope, $location, $q, $timeout, mainService, locService, storeService, utilService, $modal, $controller) {
    $controller('ControllerBase', {
        $scope: $scope,
        $location: $location,
        $q: $q
    });
    $scope.stores = [];
    $scope.productLocation = {};
    $scope.productModel = {};
    $scope.productModel.Product = {};
    $scope.selectedCategories = [];
    $scope.pageIndex = 1;
    $scope.itemPerPage = 200;

    $scope.getCategories = function (Id, level, setId) {
        if (!level || Id) {
            level = level || 0;
            if (setId) {
                $scope.productModel.Product.CategoryID = Id;
                $scope.productModel.CategoryID = Id;//this line is for search model
                $scope.selectedCategories.length = $scope.categories.length = level + 1;
            }
            return mainService.getCategories(Id, level).then(function (categories) {
                $scope.categories = categories;
                if ($scope.productModel.Product.CategoryID && !setId) {
                    angular.forEach($scope.categories[level].Categories, function (cat, index) {
                        if ($scope.productModel.Product.CategoryID.toString().startsWith(cat.ID.toString())) {
                            $scope.selectedCategories[level] = cat;
                        }
                    });
                }
                if ($scope.selectedCategories[level] && !setId) {
                    $scope.getCategories($scope.selectedCategories[level].ID, level + 1);
                }
                $scope.getBrands($scope.productModel.Product.CategoryID);
            });

        } else if (level) {
            $scope.selectedCategories.length = level;
            $scope.categories.length = level;
            if (!Id) {
                if (level > 1) {
                    $scope.productModel.Product.CategoryID = $scope.selectedCategories[level - 2].ID;
                    $scope.productModel.CategoryID = Id;//this line is for search model
                } else {
                    $scope.productModel.Product.CategoryID = null;
                    $scope.productModel.CategoryID = Id;//this line is for search model
                }
            }
        }
    }

    $scope.getCategories().then(function () {
        if (tools.utility.queryString["productid"]) {
            $scope.submitSearchProductLocationForm();
        }
    });

    $scope.submitSearchProductLocationForm = function () {
        if ($scope.productModel.Product && $scope.productModel.Product.Shelf) {
            $scope.productModel.Product.ShelfID = $scope.productModel.Product.Shelf.ID;
        }
        if (tools.utility.queryString["productid"]) {
            $scope.productModel.Product.ProductID = tools.utility.queryString["productid"];
        }
        var defer = $q.defer();
        storeService.getSearchProductType($scope.productModel.Product).then(function (data) {
            $scope.productTypes = data.Data;
            
            defer.resolve();
        });
        return defer.promise;
    }
  

    $scope.submitProductLocation = function () {
        if ($scope.productLocation && $scope.productLocation.Shelf) {
            $scope.productLocation.ShelfID = $scope.productLocation.Shelf.ID;
        }
        var defer = $q.defer();
        storeService.UpdateProductLocation($scope.productLocation).then(function (data) {
            defer.resolve(data);
            utilService.showMessage("ثبت انجام شد");
        });
        return defer.promise;
    }
    
    $scope.setCurrentProductLocation = function (product) {
        $scope.product = angular.copy( product);
        $scope.getShelves($scope.product.StoreID).then(function () {
            angular.forEach($scope.shelves, function (item) {
                if (item.ID == product.ShelfID) {
                    $scope.product.Shelf = item;
                }
            });
            $scope.product.Row = product.Row;
            $scope.product.Column = product.Column;

            var modalInstance = $modal.open({
                templateUrl:'productLocationModal.html',
                controller: 'productLocationController',
                size: 'sm',
                resolve: {
                    productLocation: function () {
                        return $scope.product;
                    },
                    shelves: function () {
                        return $scope.shelves;
                    }
                }
            });
            modalInstance.result.then(function (productLocation) {
                $scope.productLocation = productLocation;
                $scope.submitProductLocation().then(function () {
                    $scope.submitSearchProductLocationForm();
                });
            }, function () {
                //$log.info('Modal dismissed at: ' + new Date());
            });
        });
    }
}

app.controller('productLocationController', ['$injector', '$scope', '$location', '$q', '$http', '$modalInstance', 'productLocation', 'shelves','$controller',
    function ($injector, $scope, $location, $q, $http, $modalInstance, productLocation, shelves, $controller) {
        $controller('ControllerBase', {
        $scope: $scope,
        $location: $location,
        $q: $q
    });
    $scope.productLocation = productLocation;
    $scope.shelves = shelves;
    $scope.ok = function () {
        $modalInstance.close(productLocation);
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
    }]);
