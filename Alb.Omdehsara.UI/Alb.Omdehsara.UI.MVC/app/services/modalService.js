app.controller('ModalServiceController', ['$scope', '$modalInstance', 'modalService', function ($scope, $modalInstance, modalService) {
    $scope.modalOptions = modalService.tempModalOptions;
    $scope.modalOptions.ok = function (result) {
        $modalInstance.close(result);
    };
    $scope.modalOptions.close = function (result) {
        $modalInstance.dismiss('cancel');
    };
}]);
app.service('modalService', ['$modal', function ($modal) {

        var modalDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            templateUrl: window.applicationBaseUrl +'app/partials/modal.html'
        };

        var modalOptions = {
            showCancelButton: true,
            cancelButtonText: 'انصراف',
            actionButtonText: 'تایید',
            headerText: 'ادامه می دهید?',
            bodyText: ''
        };
        var self = this;
        this.showModal = function (customModalDefaults, customModalOptions) {
            if (!customModalDefaults) customModalDefaults = {};
            customModalDefaults.backdrop = 'static';
            return this.show(customModalDefaults, customModalOptions);
        };

        this.show = function (customModalDefaults, customModalOptions) {
            //Create temp objects to work with since we're in a singleton service
            var tempModalDefaults = {};
            self.tempModalOptions = {};

            //Map angular-ui modal custom defaults to modal defaults defined in service
            angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

            //Map modal.html $scope custom properties to defaults defined in service
            angular.extend(self.tempModalOptions, modalOptions, customModalOptions);


            if (!tempModalDefaults.controller) {
                tempModalDefaults.controller = 'ModalServiceController';
            }

            return $modal.open(tempModalDefaults).result;
        };

    }]);
