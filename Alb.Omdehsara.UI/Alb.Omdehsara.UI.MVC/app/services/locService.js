app.service('locService', ['$q', '$http', '$window', 'utilService', function ($q, $http, $window, utilService) {
    this.getProvinces = function () {
        var url = tools.ajax.apiBaseUrl + "api/location/getprovinces";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getCities = function (provinceId) {
        var url = tools.ajax.apiBaseUrl + "api/location/getcities?Id=" + provinceId;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
}]);