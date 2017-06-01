app.controller('PayOrderController', ['$injector', '$scope', '$location', '$q', '$http', 'mainService', 'utilService', 'locService', '$controller', '$routeParams',
    function ($injector, $scope, $location, $q, $http, mainService, utilService, locService, $controller, $routeParams) {
        $scope.payment = {};
        mainService.getBanks().then(function (data) {
            $scope.banks = data;
        });
        mainService.getOrderPrice().then(function (data) {
            $scope.payment.Price = data;
        });
        mainService.getUserCreditAcount().then(function (data) {
            $scope.userCreditAcount = data;
        });
        $scope.setCredit = function () {
            if ($scope.payment.PaymentTypeID == 4 && parseInt($scope.userCreditAcount) < parseInt($scope.payment.Price)) {
                utilService.showMessage("موجودی شما کمتر از مبلغ سفارش می باشد. لطفاابتدا موجودی خود را سایت افزایش دهید.")
            } else {
                mainService.setCredit($scope.payment).then(function () {
                    window.location.href = tools.ajax.apiBaseUrl + "order/list";
                });
            }
        }
    }]);
