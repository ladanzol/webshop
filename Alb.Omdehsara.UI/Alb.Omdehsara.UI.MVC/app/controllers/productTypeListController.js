app.controller('ProductTypeListController', ['$scope', '$location', '$q', '$timeout', 'mainService', 'locService', 'storeService', '$controller',
    function ($scope, $location, $q, $timeout, mainService, locService, storeService, $controller) {
    $controller('ControllerBase', {
        $scope: $scope,
        $location: $location,
        $q: $q
    });
    $scope.productModel = {};
    $scope.productModel.Product = {};
    $scope.selectedCategories = [];

    $scope.productTypes = [];
    $scope.pageIndex = 1;
    $scope.itemPerPage = 20;

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
            $scope.totalRecords = data.TotalRecords;
            defer.resolve();
        });
        return defer.promise;
    }

    $scope.getProductTypes = function () {
        mainService.getProductTypes($scope.pageIndex).then(function (data) {
            $scope.productTypes = data.Data;
            $scope.totalRecords = data.TotalRecords;
            $scope.$apply();
          //  console.log($scope.productTypes);
        });
    }
    $scope.getProductTypes();
    $scope.disableProduct = function(product){
        mainService.disableProductType(product.ID).then(function () {
            product.Enabled = 0;
            $scope.$apply();
        });
    }
    $scope.enableProduct = function (product) {
        mainService.enableProductType(product.ID).then(function () {
            product.Enabled = 1;
            $scope.$apply();
        });
    }
}]);

