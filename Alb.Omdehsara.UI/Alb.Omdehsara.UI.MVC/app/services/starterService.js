app.service('starterService', ['$q', '$http', '$timeout', '$rootScope', function ($q, $http, $timeout, $rootScope) {
    this.started = false;
    var self = this;
//    $.connection.hub.url = appConfig.signalRUrl;
    var itHub = $.connection.itHub;
    $.connection.hub.transportConnectTimeout = 3000;
    $.connection.hub.logging = true;
    var callBack = [];
    self.client = itHub.client;
    self.server = itHub.server;

    self.register = function (func) {
        callBack.push(func);
    }

    angular.forEach(callBack, function (index, func) {
        func();
    });

    $timeout(function () {
        $.connection.hub.start().done(function (state) {
            self.started = true;
        });
    }, 2000);
    self.client.generalError = function (data) {
        console.log(data);
        //$rootScope.generalError(data);
    };

    $.connection.hub.reconnected(function () {
    });

    $.connection.hub.disconnected(function () {
        self.started = false;
        setTimeout(function () {
            $.connection.hub.start().done(function () {
                //$rootScope...();
            })
            .fail(function (data) {
                console.log(data);
            });
        }, 5000); // Restart connection after 5 seconds.
    });
}]);