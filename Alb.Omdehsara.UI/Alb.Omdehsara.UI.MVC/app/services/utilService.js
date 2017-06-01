app.service('utilService', ['$q', '$http', '$window', '$timeout', '$location', 'modalService', function ($q, $http, $window, $timeout, $location, modalService) {
    this.showMessage = function (msg) {
        var modalOptions = {
            showCancelButton: false,
            headerText: 'توجه!',
            bodyText: msg
        };
        return modalService.showModal({}, modalOptions)
    }
    var self = this;
    this.get = function (url, expirationMin) {
        var defer = $q.defer();
        var res;
        if (expirationMin) {
            res = tools.storage.load(url);
        }
        if (res) {
            defer.resolve(res);
        } else {
            tools.utility.blockUI();
            $http.get(url).then(function (data) {
                if (expirationMin) {
                    tools.storage.save(url, data.data, expirationMin);
                }
                tools.utility.unblockUI();
                defer.resolve(data.data);
            }, function (data) {
                tools.utility.unblockUI();
                if (data.status == 400) {//bussiness logic error 
                    window.console.log(data);
                    self.showMessage(data.Message);
                } else if (data.status == 401) {
                    window.console.log(data);
                    self.showMessage("لطفا وارد سیستم شوید").then(function () {
                        window.location.href = window.applicationBaseUrl + "account/login?returnUrl=" + window.location.href.replace("#", "@");
                    });
                }
                else {
                    self.showMessage(data.data);
                }
                defer.reject(data);
            });
        }
        return defer.promise;
    }
    this.post = function (url, data) {
        var defer = Q.defer();
        tools.utility.blockUI();
        $http.post(url, data).then(function (data) {
            tools.utility.unblockUI();
            defer.resolve(data.data);
        }, function (data) {
            tools.utility.unblockUI();
            if (data.status == 400) {//bussiness logic error 
                self.showMessage(data.data);
            } else if (data.status == 401) {
                window.console.log(data);
                self.showMessage("لطفا وارد سیستم شوید").then(function () {
                    window.location.href = window.applicationBaseUrl + "account/login?returnUrl=" + window.location.href.replace("#", "@");
                });
            }
            else {
                self.showMessage(data.data);
            }
            defer.reject(data);
        });
        return defer.promise;
    }
    this.palyDing = function () {
        var audioElement = document.createElement('audio');
        audioElement.setAttribute('src', tools.ajax.apiBaseUrl + 'content/ding.mp3');
        audioElement.play();
    }
    this.path = function (route) {
        $timeout(function () {
            $location.path(route);
        }, 0);
    }
}]);