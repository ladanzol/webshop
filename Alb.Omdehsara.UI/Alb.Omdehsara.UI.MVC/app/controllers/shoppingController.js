app.controller('ShoppingController', ['$rootScope', '$scope', '$location', '$q', '$timeout', '$routeParams', 'mainService', 'locService', 'utilService', function ($rootScope, $scope, $location, $q, $timeout, $routeParams, mainService, locService, utilService) {
    $scope.orderDetails = [];
    $scope.order = {};
    $scope.getClientOrderDetails = function () {
        mainService.getClientOrderDetails().then(function (data) {
            $scope.getDiscount().then(function () {
                if (data.length > 0) {
                    angular.forEach(data, function (item) {
                        item.OldQuantity = item.Quantity;
                    });

                    $scope.orderDetails = data;
                    $scope.calculateOrderDetailPrice();
                    $scope.OrderID = $scope.orderDetails[0].OrderID;
                    if ($location.path().indexOf('/cart') >-1  && !tools.utility.queryString["returnurl"]) {
                        mainService.setOrderCustomer($scope.OrderID);
                    }
                    //$scope.$apply();
                }
            });
        });
    }
    $scope.getQuantityRange = function (orderDetail) {
        return parseInt(orderDetail.ProductQuantity) + parseInt(orderDetail.Quantity);
    }
    $scope.OrderID = 0;
    if ($scope.orderDetails.length == 0) {
        $scope.getClientOrderDetails();
    }

    $scope.calculateOrderDetailPrice = function() {
        $scope.orderPrice = 0;
        $scope.orderCount = 0;
        angular.forEach($scope.orderDetails, function (item) {
            item.Price = item.Quantity * item.UnitPrice;
            $scope.orderPrice += item.Price;
            $scope.orderCount += item.Quantity;
        });
        $scope.levelDiscount = 0;
        $scope.orderDiscount = 0;
        if ($scope.orderPrice > $scope.discount.Level1Price) {
            if ($scope.orderPrice > $scope.discount.Level2Price) {
                if ($scope.orderPrice > $scope.discount.Level3Price) {
                    $scope.levelDiscount = $scope.discount.Level3Discount;
                } else {
                    $scope.levelDiscount = $scope.discount.Level2Discount;
                }
            } else {
                $scope.levelDiscount = $scope.discount.Level1Discount;
            }
        }
        if ($scope.discount.CountDiscount > 0 && $scope.orderCount > $scope.discount.CountDiscount) {
            $scope.orderDiscount = $scope.orderPrice * ($scope.discount.CountDiscountPercent + $scope.discount.UserDiscount + $scope.levelDiscount) / 100;
        }
        $scope.orderFinalPrice = $scope.orderPrice - $scope.orderDiscount;
        if ($scope.order.selectedTransport && $scope.order.selectedTransport.Price) {
            $scope.orderFinalPrice = $scope.orderFinalPrice + parseInt($scope.order.selectedTransport.Price);
        } else {
            $scope.orderFinalPrice = " لطفا نحوه ارسال را مشخص نمایید";
        }
    }

    $rootScope.$on('NewOrderSubmited', function (event, order) {
        var productIsOrderedBefore = false;
        angular.forEach($scope.orderDetails, function (item) {
            if (item.ProductTypeID == order.ProductTypeID && item.ColorID == order.ColorID && item.SizeID == order.SizeID) {
                productIsOrderedBefore = true;
                item.Quantity = parseInt(item.Quantity) + parseInt(order.Quantity);
                $scope.orderQuantityChanged(item);
            }
        });
        if (!productIsOrderedBefore) {
            if ($scope.OrderID > 0) {
                order.OrderID = $scope.OrderID;
            }
            mainService.addToCart(order).then(function (data) {
                $scope.OrderID = order.OrderID = data;
                order.ProductQuantity = parseInt(order.ProductQuantity) - parseInt(order.Quantity);
                order.OldQuantity = order.Quantity;
                $scope.calculateOrderDetailPrice();
                $scope.orderDetails.push(order);

                tools.hintMessage(order.ProductTitle + " به سبد خرید شما اضافه شد", "success");
                $scope.$apply();
            }, function (data) {
                //if (angular.isNumber(data)) {
                //    data.ProductQuantity = parseInt(data);
                //}
            });
        }
    });
    $scope.getDiscount = function () {
        if ($scope.discount) {
            return;
        } else {
            return mainService.getDiscount().then(function (data) {
                $scope.discount = data;
                return;
            });
        }
    }
    $scope.orderQuantityChanged = function (orderDetail) {
        mainService.changeOrderDetailQuantity(orderDetail).then(function (data) {
            orderDetail.ProductQuantity = parseInt(orderDetail.ProductQuantity) + parseInt(orderDetail.OldQuantity) - parseInt(orderDetail.Quantity);
            $scope.calculateOrderDetailPrice();
            orderDetail.OldQuantity = orderDetail.Quantity;
            //tools.hintMessage(orderDetail.ProductTitle + " به سبد خرید شما اضافه شد", "success");
            $scope.$apply();
        }, function (data) {
            orderDetail.Quantity = orderDetail.OldQuantity;
            $scope.$apply();
        });
    }
    $scope.removeOrderDetail = function (orderDetail) {
        mainService.deleteOrderDetail(orderDetail).then(function (data) {
            $scope.orderDetails.splice($scope.orderDetails.indexOf(orderDetail), 1);
            $rootScope.$apply();
        });
    }
    $scope.orderQuantity = function () {
        var orderQuantity = 0;
        angular.forEach($scope.orderDetails, function (item) {
            orderQuantity += parseInt(item.Quantity * item.UnitQuantity);
        });
        return orderQuantity;
    }

    $scope.orderProductCount = function () {
        var orderQuantity = 0;
        angular.forEach($scope.orderDetails, function (item) {
            orderQuantity += parseInt(item.Quantity);
        });
        return orderQuantity;
    }

    $scope.enoughOrder = function () {
        if (!mainService.constants) {
            return false;
        } else {
            return mainService.constants.MinCount <= $scope.orderQuantity();
        }
    }

    $scope.completeOrder = function () {
        var goAhead = true;
        angular.forEach($scope.orderDetails, function (item) {
            if (item.Quantity < item.MinOrderQuantity) {
                utilService.showMessage("تعداد سفارش " + item.ProductTitle + " نباید از " + item.MinOrderQuantity + " عدد کمتر باشد.");
                goAhead = false;
                return;
            }
        });
        if (goAhead) {
            if (mainService.constants.MinCount > $scope.orderQuantity()) {
                utilService.showMessage("تعداد کل سفارش نباید از " + mainService.constants.MinCount + " عدد کمتر باشد.");
            } else {
                window.location.href = tools.ajax.apiBaseUrl + "cart/#/cart/address";
            }
        }
    }
    $scope.setAddressOrder = function () {
        if (mainService.constants.MinCount > $scope.orderQuantity()) {
            utilService.showMessage("تعداد سفارش نباید از " + mainService.constants.MinCount + " عدد کمتر باشد.");
        } else {
            $scope.setOrderTransPort();
           // $location.path("/cart/1");
        }
    }
    if ($routeParams.fullpage) {
        $scope.transportTypes = [];
        mainService.getOrderTransportTypes().then(function (data) {
            console.log(data);
            $scope.transportTypes = data;
        });
        $scope.selectedTransport = {};
        $scope.setOrderTransPort = function () {
            mainService.setOrderTransPort($scope.order.selectedTransport.ID).then(function () {
                utilService.path('/payorder');
            });
        }
    }
}]);

