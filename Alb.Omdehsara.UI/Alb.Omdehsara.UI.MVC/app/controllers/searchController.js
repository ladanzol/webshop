app.controller('SearchController', ['$injector', '$scope', '$routeParams', '$location', '$q', '$timeout', 'mainService', 'locService','$controller',
    function ($injector, $scope, $routeParams, $location, $q, $timeout, mainService, locService, $controller) {
        $controller('ProductBaseController', {
        $injector: $injector,
        $scope: $scope,
        $routeParams :$routeParams,
        $location: $location,
        $q: $q,
        $timeout: $timeout,
        mainService: mainService,
        locService: locService,
        $controller: $controller,
        });
       
    $scope.priceSlider = {
        min: 1000,
        max: 100000,
        ceil: 1000000,
        floor: 0,
        step: 10000
    };


    $scope.translate = function (value) {
        return ' تومان ' + value;
    }

    $scope.selectedCategories = [];
    $scope.productList = [];
    $scope.loadingProcess = false;
    $scope.pageIndex = 1;
    $scope.endSearchProduct = false;

    $scope.userSearches = false;//when user start searching the initial product should be disapeared.
    var searchedFromPrice = -1;
    var searchedToPrice = -1;
    $scope.searchProduct = function () {
        var defer = $q.defer();
        if (!$scope.endSearchProduct) {
            $scope.productModel.PageIndex = $scope.pageIndex;
            $scope.productModel.CategoryID = $scope.productModel.Product.CategoryID;
            $scope.userSearches = true;
            $scope.loadingProcess = true;
            searchedFromPrice = $scope.productModel.FromPrice;
            searchedToPrice = $scope.productModel.ToPrice;
            mainService.searchProduct($scope.productModel).then(function (data) {
                if (data.length > 0) {
                    $scope.productList.append(data);
                } else {
                    $scope.endSearchProduct = true;
                }
                $scope.pageIndex++;
                if ((searchedFromPrice > -1 && $scope.productModel.FromPrice != searchedFromPrice)
                    || (searchedToPrice > -1 && $scope.productModel.ToPrice != searchedToPrice)) {
                    $scope.pageIndex = 1;
                    $scope.productList.length = 0;
                    $scope.searchProduct().then(function () {
                        searchedFromPrice = -1;
                        searchedToPrice = -1;
                    });
                } else {
                    searchedFromPrice = -1;
                    searchedToPrice = -1;
                }
                defer.resolve();
                $timeout(function () {
                    $scope.loadingProcess = false;
                }, 2000);
            });
        } else {
            defer.resolve();
        }
        return defer.promise;
    }
    $scope.loadNextProducts = function () {
        console.log('pp ' + $scope.pageIndex)
        if (!$scope.loadingProcess && !$scope.endSearchProduct) {
            if ($scope.pageIndex > 1) {
                $scope.searchProduct();
            }
        }
    }
    $scope.$on('LastElemInRepeat', function (event) {
        console.log('LastElemInRepeat');
        $timeout(function () {
            $(".divFooter").css("top", parseInt($('.divProducts').height()) +10);
        }, 300);
    });
    $scope.getProductType().then(function () {
        $scope.searchProduct().then(function () {
           
        });
    });
    var init = true;
    $scope.$watch('productModel.FromPrice', function (newValue) {
        if (init) {
            $timeout(function () { init = false; });
        } else {
            if (searchedFromPrice ==-1) {
                $scope.pageIndex = 1;
                $scope.productList.length = 0;
                $scope.endSearchProduct = false;
                $scope.searchProduct().then(function () {
                    console.log("search")
                });
            }
        }
    });

    $scope.$watch('productModel.ToPrice', function (newValue) {
        if (init) {
            $timeout(function () { init = false; });
        } else {
            if (searchedToPrice == -1) {
                $scope.pageIndex = 1;
                $scope.endSearchProduct = false;
                $scope.productList.length = 0;
                $scope.searchProduct().then(function () {
                });
            }
        }
    });
    $scope.searchChanged = function () {
        $timeout(function () {
            $scope.pageIndex = 1;
            $scope.endSearchProduct = false;
            $scope.productList.length = 0;
            $scope.searchProduct();
          //  $scope.setUserFilter();
        }, 10);
    }

    if ($routeParams.categoryId) {
        $scope.getColors($routeParams.categoryId);
        $scope.getSizes($routeParams.categoryId);
    }
}]);


