app.service('mainService', ['$q', '$http', 'utilService', function ($q, $http, utilService) {
    var cateogories = [];
    //create the empty model
    var productModel = {};
    initProductModel = function () {
        productModel = {};
        productModel.Product = {};
        productModel.ProductProperties = [];
        productModel.FromPrice = 0
        productModel.ToPrice = 1000000;
        productModel.Product.PriceTime = new Date();
    }
    initProductModel();
    var self = this;
    var productProperties = [];
    this.getCategories = function (parentID, level) {
        level = level || 0;
        cateogories.length = level + 1;
        if (cateogories.length < level) {
            return 'Load the top level of categories first.';
        }
        var categoryUrl = tools.ajax.apiBaseUrl + "api/product/getCategory";
        if (parentID) {
            categoryUrl += "?parentID=" + parentID;
        }
        return utilService.get(categoryUrl).then(function (data) {
            cateogories[level] = data;
            if (level > 0) {
                cateogories[level].CategoryProperties.extend(cateogories[level - 1].CategoryProperties);
                productModel.ProductProperties.length = cateogories[level].CategoryProperties.length;
                angular.forEach(cateogories[level].CategoryProperties, function (item, index) {
                    var p = getProperty(item.ID);
                    productModel.ProductProperties[index] =  p || {};
                    productModel.ProductProperties[index].SelectedConstants = item.SelectedConstants || [];
                    productModel.ProductProperties[index].PropertyID = item.ID;
                    productModel.ProductProperties[index].PropertyType = item.PropertyType;
                    productModel.ProductProperties[index].PropertyName = item.PropertyName;
                });
            }
            return cateogories;
        });
        function getProperty(propertyID) {
            var result = null;
            angular.forEach(productProperties, function (prop, index) {
                if (prop.PropertyID == propertyID) {
                    result = prop;
                }
            });
            return result;
        }
    }
    this.getProductType = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductType";
        var defer = $q.defer();
        if (Id) {
            url += "/" + Id;
            utilService.get(url).then(function (data) {
                productModel = data;
                productProperties = angular.copy(productModel.ProductProperties);
                defer.resolve(productModel);
            });
        } else {
            initProductModel();
            defer.resolve(productModel);
        }
        return defer.promise;
    }
    this.getSimpleProductType = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/getSimpleProductType/"+Id;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.add = function () {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/product/add";
        return utilService.post(url, productModel).then(function (data) {
            return data;;
        });
        return defer.promise;
    }
    this.update = function () {
        var url = tools.ajax.apiBaseUrl + "api/product/update";
        return utilService.post(url, productModel).then(function (data) {
            return data;
        });
    }

    this.searchProduct = function (searchModel) {
        var url = tools.ajax.apiBaseUrl + "api/product/search";
        return utilService.post(url, searchModel).then(function (data) {
            return data;
        });
    }

    this.deleteProductType = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/deleteProductType/" + Id;
        return utilService.post(url, Id).then(function (data) {
            return data;
        });
    }

    this.getBrands = function (categoryId) {
        var url = tools.ajax.apiBaseUrl + "api/product/getBrands?categoryId=" + categoryId;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getUnits = function () {
        var url = tools.ajax.apiBaseUrl + "api/product/getUnits";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getSizes = function (categoryId) {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/product/getSizes?categoryId=" + categoryId;
        return utilService.get(url).then(function (data) {
            return data;;
        });
        return defer.promise;
    }
    this.getColors = function (categoryId) {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/product/getColors?categoryId=" + categoryId;
        return utilService.get(url).then(function (data) {
            return data;;
        });
        return defer.promise;
    }
    this.getProductTypes = function (pageIndex) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductTypes";
        return utilService.post(url, { PageIndex: pageIndex }).then(function (data) {
            return data;
        });
    }
    this.getProducts = function (productTypeID, pageIndex) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProducts/" + productTypeID + "?pageIndex=" + pageIndex;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getProductLocations = function (searchOptions) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductLocations/";
        return utilService.post(url, searchOptions).then(function (data) {
            return data;
        });
    }
    
    //absolute
    this.addProduct = function (product) {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/product/addProduct";
        return utilService.post(url, product).then(function (data) {
            return data;;
        });
        return defer.promise;
    }
    //absolute
    this.updateProduct = function (product) {
        var defer = $q.defer();
        var url = tools.ajax.apiBaseUrl + "api/product/updateProduct";
        return utilService.post(url, product).then(function (data) {
            return data;;
        });
        return defer.promise;
    }

    this.storeProduct = function (product) {
        var url = tools.ajax.apiBaseUrl + "api/product/storeProduct";
        return utilService.post(url, product).then(function (data) {
            return data;;
        });
    }
    
    this.deleteProduct = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/deleteProduct/" + Id;
        return utilService.post(url).then(function (data) {
            return data;;
        });
    }
    this.disableProductType = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/disableProductType/" + Id;
        return utilService.post(url).then(function (data) {
            return data;;
        });
    }
    this.enableProductType = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/enableProductType/" + Id;
        return utilService.post(url).then(function (data) {
            return data;;
        });
    }
    this.getProductTypeAvailableColors = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductTypeAvailableColors/" + Id;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getProductTypeAvailableSizes = function (Id) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductTypeAvailableSizes/" + Id;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getProductAvailableColors = function (Id, sizeId) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductAvailableColors/?Id=" + Id+"&sizeId="+sizeId;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getProductByColorAndSize = function (Id, sizeId, colorId) {
        var url = tools.ajax.apiBaseUrl + "api/product/getProductByColorAndSize/?Id=" + Id + "&sizeId=" + sizeId + "&colorId=" + colorId;
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    

    this.addToCart = function (order) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/addtocart";
        order.ClientID = tools.getClientID();
        return utilService.post(url, order).then(function (data) {
            return data;
        });
    }
    var orderDetails = [];
    this.getClientOrderDetails = function () {
        var defer = $q.defer();
        if (orderDetails.length > 0) {
            defer.resolve(orderDetails);
        } else {
            var url = tools.ajax.apiBaseUrl + "api/shopping/getClientOrderDetails/" + tools.getClientID();
            utilService.post(url).then(function (data) {
                if (orderDetails.length == 0) {
                    //If this method is called more than one the mothed return one refrence
                    orderDetails = data;
                }
                defer.resolve(orderDetails);
            });
        }
        return defer.promise;
    }
    this.deleteOrderDetail = function (orderDetail) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/deleteOrderDetail";
        return utilService.post(url, orderDetail).then(function (data) {
            return data;
        });
    }
    this.changeOrderDetailQuantity = function (orderDetail) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/changeOrderDetailQuantity";
        return utilService.post(url, orderDetail).then(function (data) {
            return data;
        });
    }
    this.getDiscount = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getDiscount";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.submitAddress = function (address) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/setOrderAddress";
        return utilService.post(url, address).then(function (data) {
            return data;
        });
    }
    
    this.getCustomerAddress = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getCustomerAddress";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.setOrderCustomer = function (orderId) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/setOrderCustomer";
        return utilService.post(url, orderId).then(function (data) {
            return data;
        });
    }
    this.getOrderPrice = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getOrderPrice";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getOrderTransportTypes = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getOrderTransportTypes";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.setOrderTransPort = function (transportId) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/SetOrderTransportType";
        var data = { TransportTypeID : transportId}
        return utilService.post(url, data).then(function (data) {
            return data;
        });
    }
    this.getBanks = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getBanks";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.getUserCreditAcount = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getUserCreditAcount";
        return utilService.get(url).then(function (data) {
            return data;
        });
    }
    this.setCredit = function (payment) {
        var url = tools.ajax.apiBaseUrl + "api/shopping/setCredit";
        return utilService.post(url, payment).then(function (data) {
            tools.clearClientID();
            return data;
        });
    }
    this.getConstants = function () {
        var url = tools.ajax.apiBaseUrl + "api/shopping/getConstants";
        return utilService.get(url, 20).then(function (data) {
            self.constants = data;
            return data;
        });
    }
    this.getConstants();

}]);

