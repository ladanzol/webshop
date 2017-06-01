app.directive('singleProductType', ['$modal', '$rootScope', 'mainService', function ($modal, $rootScope, mainService) {
    return {
        restrict: 'EA',
        templateUrl: tools.ajax.apiBaseUrl + 'app/partials/singleProductType.html?v=5',
        replace: true,
        scope: {
            model: '='
        },
        link: function (scope, element, attrs) {
            scope.addToCart = function (product) {
                scope.product = product;
                var modalInstance = $modal.open({
                    templateUrl: tools.ajax.apiBaseUrl + 'app/partials/addToCartModal.html?v=1',
                    controller: 'AddToCartController',
                    size: 'sm',
                    resolve: {
                        product: function () {
                            return scope.product;
                        }
                    }
                });
                modalInstance.result.then(function (orderDetail) {
                }, function () {
                });
            }
        }
    };
}]);

app.directive('polling', ['pollingService', 'version', function (pollingService, version) {
    return {
        restrict: 'E',
        templateUrl: tools.ajax.apiBaseUrl + 'app/partials/polling.html?v=' + version,
        replace: true,
        scope: {
            id: '@'
        },
        controller: ['$scope', function ($scope) {
            function init() {
                $scope.question = {};
                $scope.options = [];
                pollingService.getPolling($scope.id).then(function (data) {
                    $scope.question = data.Question;
                    $scope.options = data.Options;
                });
            }
            init();
        }],
        link: function (scope, element, attrs) {
            scope.answer = {};
            if (tools.cookie.getCookie("polling_" + scope.id) !== "") {
                $(element).html("با تشکر از نظر شما");
            }
            scope.submitPolling = function () {
                pollingService.setPolling({ ClientID: tools.getClientID(), AnswerID: scope.answer.value }).then(function () {
                    tools.cookie.setCookie("polling_" + scope.id, "ok", 1000);
                    $(element).html("با تشکر. نظر شما ثبت گردید");
                });
            }
        }
    };
}]);

app.directive('detectLastItem', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            scope.$emit('LastElemInRepeat');
        }
    };
});

