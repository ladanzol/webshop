app.controller('ProductTypeController', ['$injector', '$scope', '$routeParams', '$location', '$q', '$timeout', 'mainService', 'locService','$controller',
    function ($injector, $scope, $routeParams, $location, $q, $timeout, mainService, locService, $controller) {
        $controller('ProductBaseController', {
            $injector: $injector,
            $scope: $scope,
            $routeParams: $routeParams,
            $location: $location,
            $q: $q,
            $timeout: $timeout,
            mainService: mainService,
            locService: locService,
            $controller: $controller
        });
    $scope.selectedCategories = [];
    $scope.productList = [];
    $scope.loadingProcess = false;
    $scope.pageIndex = 1;
    $scope.endSearchProduct = false;
    $scope.tinymceOptions = {
        height: 500,
        handle_event_callback: function (e) {
            // put logic here for keypress
        },
        directionality: "rtl",
        plugins: [
    "advlist autolink lists link image charmap print preview anchor",
    "searchreplace visualblocks code fullscreen",
    "insertdatetime media table contextmenu paste "
           ],
           toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
    };
    $scope.getProductType($routeParams.productTypeId);
    $scope.getUnits();

    $scope.submitProductType = function () {
        if ($scope.productModel.Product.ID > 0) {
            $scope.update();
        } else {
            $scope.add();
        }
    }
    $scope.add = function () {
        mainService.add().then(function (result) {
            $scope.productModel.Product.ID = result;
                alert("محصول شما ذخیره شد");
                //$location.path("/addproduct/"+$scope.productModel.Product.ID)
                 tools.utility.redirect('product/image/index?productid=' + $scope.productModel.Product.ID);
        }, function () {
            $scope.productModel.Product.SelectedCityArea = [$scope.productModel.Product.SelectedCityArea];
        });
    }
    $scope.update = function () {
        mainService.update().then(function (result) {
                alert("محصول شما ذخیره شد");
                tools.utility.redirect('product/image/index?productid=' + $scope.productModel.Product.ID);
        }, function () {
            $scope.productModel.Product.SelectedCityArea = [$scope.productModel.Product.SelectedCityArea];
        });
    }
    $scope.deleteProduct = function (product) {
        mainService.deleteProduct(product.Id).then(function () {
            alert("حذف انجام شد");
        });
    }
    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 6
    };
    $scope.openPersianFrom = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.persianIsOpenFrom = true;
    };
    $scope.hstep = 1;
    $scope.mstep = 15;
    $scope.ismeridian = true;
}]);

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

