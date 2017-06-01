app.service('storeService', ['$q', '$http', 'utilService', function ($q, $http, utilService) {


    this.getStores = function () {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/store/getStores";
        $http.get(url).then(function (data) {
            defer.resolve(data.data);
        });
        return defer.promise;
    }
    this.getShelves = function (store) {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/store/getShelves?store=" + store;
        $http.get(url).then(function (data) {
            defer.resolve(data.data);
        });
        return defer.promise;
    }

    this.updateProductLocation = function (productLocation) {
        var url = tools.ajax.apiBaseUrl + "api/store/updateProductLocation";
        return utilService.post(url, productLocation).then(function (data) {
            return data;
        });
    }
    this.searchProductLocation = function (productLocationOption) {
        var url = tools.ajax.apiBaseUrl + "api/store/searchProductLocation";
        return utilService.post(url, productLocationOption).then(function (data) {
           return data;
        });
    }
    this.getSearchProductType = function (productLocationOption) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductTypes";
        return utilService.post(url, productLocationOption).then(function (data) {
            return  data;
        });
    }
    
    this.UpdateProductLocation = function (productLocation) {
        var url = tools.ajax.apiBaseUrl + "api/store/UpdateProductLocation";
        utilService.post(url, productLocation).then(function (data) {
            return data;
        });
    }
    
}]);