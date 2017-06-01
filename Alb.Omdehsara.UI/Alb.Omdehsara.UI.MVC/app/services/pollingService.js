app.service('pollingService', ['$q', '$http', '$window', 'utilService', function ($q, $http, $window, utilService) {
    this.getPolling = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/polling/getPolling?Id="+Id;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.setPolling = function (data) {
        var url = tools.ajax.apiBaseUrl + "api/polling/setPolling";
        return utilService.post(url, data).then(function (data) {
            return data;
        });
    }
}]);