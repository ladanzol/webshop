function ProductBaseController($injector, $scope, $routeParams, $location, $q, $timeout, mainService, locService, $controller) {
    $controller('ControllerBase', {
        $scope: $scope,
        $location: $location,
        $q: $q,
        storeService: null
    });
    $scope.selectedCategories = [];
    $scope.productList = [];
    $scope.loadingProcess = false;
    $scope.pageIndex = 1;

    $scope.getCategories = function (Id, level, setId) {
      //  if (!level || Id) {
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
                    return $scope.getCategories($scope.selectedCategories[level].ID, level + 1);
                }
                $scope.getBrands($scope.productModel.Product.CategoryID);
                return;
            });

        //} else if (level) {
        //    $scope.selectedCategories.length = level;
        //    $scope.categories.length = level;
        //    if (!Id) {
        //        if (level > 1) {
        //            $scope.productModel.Product.CategoryID = $scope.selectedCategories[level - 2].ID;
        //            $scope.productModel.CategoryID = Id;//this line is for search model
        //        } else {
        //            $scope.productModel.Product.CategoryID = null;
        //            $scope.productModel.CategoryID = Id;//this line is for search model
        //        }
        //    }
        //}
    }

    $scope.getProductType = function (Id) {
        //If there is no Id an empty product is return from service
        $scope.selectedCategories.length = 0;
        return mainService.getProductType(Id).then(function (product) {
            $scope.productModel = product;
            if ($routeParams.categoryId) {
                //for when we have a default category in search product
                $scope.productModel.Product.CategoryID = $routeParams.categoryId;
            }
            return $scope.getCategories();
        });
    }
    $scope.userFilter = [];
    $scope.setUserFilter = function () {
        $scope.userFilter.length = 0;
        angular.forEach($scope.productModel.ProductProperties, function (prop) {
            if (prop.SelectedConstants.length > 0) {
                angular.forEach(prop.SelectedConstants, function (c) {
                    var f = {};
                    f.PropertyType = prop.PropertyType;
                    f.PropertyID = prop.PropertyID;
                    f.Text = c.Text;
                    $scope.userFilter.push(f);
                });
            } else
                if (prop.PropertyType == 'TextBox' && prop.PropertyValue && prop.PropertyValue.length > 0) {
                    var f = {};
                    f.PropertyType = prop.PropertyType;
                    f.PropertyID = prop.PropertyID;
                    $scope.userFilter.push(f);
                    f.Text = prop.PropertyValue;
                } else if (prop.PropertyType == 'CheckBox' && prop.PropertyValue == '1') {
                    var f = {};
                    f.PropertyType = prop.PropertyType;
                    f.PropertyID = prop.PropertyID;
                    f.Text = prop.PropertyName;
                    $scope.userFilter.push(f);
                }
        });
    }
    $scope.deleteFilter = function (filter) {
        $scope.userFilter.splice($scope.userFilter.indexOf(filter), 1);
        angular.forEach($scope.productModel.ProductProperties, function (prop) {
            if (filter.PropertyType == 'ListBox' && prop.PropertyID == filter.PropertyID) {
                var index = 0;
                angular.forEach(prop.SelectedConstants, function (item) {
                    if (item.Text == filter.Text) {
                        prop.SelectedConstants.splice(index, 1);
                        return;
                    }
                    index++;
                });
            }
            if (filter.PropertyType == 'CheckBox' && prop.PropertyID == filter.PropertyID) {
                prop.PropertyValue = '0';
            }
            if (filter.PropertyType == 'TextBox' && prop.PropertyID == filter.PropertyID) {
                prop.PropertyValue = '';
            }
        });
    }
}

app.controller('ProductBaseController', ['$injector', '$scope', '$routeParams', '$location', '$q', '$timeout', 'mainService', 'locService','$controller', ProductBaseController]);

app.filter('sampleFilter', function () {
    return function (input, input) {
        var output = [];
        angular.forEach(input, function (item) {
            if (item.Id.toString().substring(0, 2) == input) {
                output.push(item);
            }
        });
        return output;
    };
});

