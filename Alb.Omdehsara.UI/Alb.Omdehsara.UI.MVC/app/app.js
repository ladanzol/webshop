var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ui.bootstrap.persian.datepicker', 'ui.bootstrap.datepicker', 'number', 'ui.tinymce', 'checklist-model', 'rzModule', 'infinite-scroll']);
app.constant('version', '10');
app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/producttypelist', {
            templateUrl: '../../../app/partials/producttypelist.html?v=3',
            controller: 'ProductTypeListController'
        }).
        when('/producttype/', {
            templateUrl: '../../../app/partials/producttype.html?v=3',
            controller: 'ProductTypeController'
        }).
        when('/producttype/:productTypeId', {
            templateUrl: '../../../app/partials/producttype.html?v=3',
            controller: 'ProductTypeController'
        }).
        when('/addproduct/:categoryId/:productTypeId/:productId', {
            templateUrl: '../../../app/partials/addproductToStore.html?v=3',
            controller: 'ProductController'
        }).
        when('/addproduct/:categoryId/:productTypeId', {
            templateUrl: '../../../app/partials/addproductToStore.html?v=3',
            controller: 'ProductController'
        }).when('/show/:categoryId', {
            templateUrl: '../../app/partials/productSearch.html?v=6',
            controller: 'SearchController'
        }).
        when('/cart/address', {
            templateUrl: '../app/partials/customerAddress.html?v=5',
            controller: 'CustomerAddressController'
        }).
        when('/cart/transport', {
            templateUrl: '../app/partials/orderTransportType.html?v=3',
            controller: 'OrderTransportController'
        }).
        when('/cart/:fullpage', {
            templateUrl: '../app/partials/shoppingCartFullPage.html?v=5',
            controller: 'ShoppingController',
         }).
        when('/payorder', {
            templateUrl: '../app/partials/payorder.html?v=3',
            controller: 'PayOrderController',
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);

app.filter('range', function () {
    return function (input, total) {
        total = parseInt(total);
        for (var i = 0; i < total; i++)
            input.push(i);
        return input;
    };
});
app.run(['$rootScope', '$location', function ($rootScope, $location) {
    $rootScope.shoppingCartHtmlUrl = tools.ajax.apiBaseUrl + 'app/partials/shoppingCart.html';
    $rootScope.baseUrl = tools.ajax.apiBaseUrl;
    $rootScope.$on("$locationChangeStart",function(event, next, current){
        tools.utility.scrollTop();
        //if(!$rootScope.isFormValid()){
        //    event.preventDefault();
        //}
    });
}]);

