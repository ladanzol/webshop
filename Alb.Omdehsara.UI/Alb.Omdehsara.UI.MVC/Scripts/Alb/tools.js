
var tools = tools || {};
tools.ajax = tools.ajax || {};
tools.ajax.apiBaseUrl = window.applicationBaseUrl;
tools.subdomain = window.subdomain;
tools.ajax.get = function (url, key, expirationMin) {
    var defer = Q.defer();
    var res;
    if (key) {
        res = tools.storage.load(key);
    }
    if (res) {
        defer.resolve(res);
    } else {
        $.ajax({
            url: url,
            type: "GET"
        }).done(function (result) {
            if (key) {
                tools.storage.save(key, result, expirationMin);
            }
            defer.resolve(result);
        }).error(function (jqXHR, textStatus, errorThrown) {
            // tools.showMessage("امکان ارتباط با سرور وجود ندارد.");
        });
    }
    return defer.promise;
}

tools.ajax.post = function (url, data, contentType) {
    if (!contentType) {
        contentType = 'application/x-www-form-urlencoded; charset=UTF-8';
    }
    var defer = Q.defer();
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        contentType: contentType,
    }).done(function (result) {
        defer.resolve(result);
    }).error(function (jqXHR, textStatus, errorThrown) {
        //tools.showMessage("امکان ارتباط با سرور وجود ندارد.");
    });
    return defer.promise;
}
tools.showMessage = function (msg) {
    alert(msg);
}
tools.showSuccess = function (msg) {
    tools.hintMessage(msg, "success");
}
tools.hintMessage = function(message, messageType) {
    $("#msgType").val(messageType);
    $("#msgMessage").val(message);
    tools.showHintMessage();
}
tools.showHintMessage = function () {
    if ($("#msgMessage").val() !== "" || $("#msgTitle").val() !== "") {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-bottom-right",
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        if ($("#msgType").val() == "success") {
            toastr.success($("#msgMessage").val(), $("#msgTitle").val());
        } else if ($("#msgType").val() == "error") {
            toastr.error($("#msgMessage").val(), $("#msgTitle").val());
        }
    }
}
tools.ajax.blockui = true;
tools.ajaxStartCount = 0;
if (typeof jQuery != 'undefined') {
    $(document).ajaxStart(function () {
        tools.utility.blockUI();
    }).ajaxSuccess(function () {
        tools.utility.unblockUI();
    }).ajaxError(function () {
        if (tools.utility.browserIsOk()) {
            tools.showMessage("امکان ارتباط با سرور وجود ندارد.");
        } else {
            alert("لطفا از مرورگر نسخه بروزتر استفاده نمایید. نسخه مرورگر شما : " + $.browser.version);
        }
        tools.utility.unblockUI();
    });
}
tools.twinkle = function (obj) {
    var options = {
        effect: 'pulse',
        effectOptions: {
            radius: 100,
            color: "rgba(255,0,0,0.5)",
            duration: 3000
        }
    }
    obj.twinkle(options);
    setTimeout(function () {
        obj.twinkle(options);
    }, 3000);
    setTimeout(function () {
        obj.twinkle(options);
    }, 6000);
}
//////////////////////////
tools.utility = tools.utility || {};
tools.utility.numberWithCommas = function (str) {
    return str.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

tools.utility.blockUI = function () {
    if (tools.ajax.blockui) {
        tools.ajaxStartCount++;
        if (tools.ajaxStartCount == 1) {
            $(".divCenterFrame").block({
                centerY: true,
                centerX: true,
                css: {
                    border: 'none',
                    padding: '8px',
                    showOverlay: false,
                    backgroundColor: 'transparent',
                    '-webkit-border-radius': '3px',
                    '-moz-border-radius': '3px',
                    color: '#555555',
                }
        , message: '<div class="loading"><img src="' + tools.ajax.apiBaseUrl + 'content/images/loading.gif" /></div>'
        , overlayCSS: { opacity: .2 }
            });
            $('.blockUI.blockMsg').center();
        }
    }
}
tools.utility.unblockUI = function () {
    if (tools.ajax.blockui) {
        tools.ajaxStartCount--;
        if (tools.ajaxStartCount == 0) {
            $(".divCenterFrame").unblock();
        }
    }
}
$.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() - 100 + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() - 100 + "px");
    return this;
}
tools.utility.isNumeric = function (n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
tools.utility.getNumber = function (str) {
    var result = "";
    for (var i = 0; i < str.length; i++) {
        if (tools.utility.isNumeric(str.charAt(i))) {
            result += str.charAt(i);
        }
    }
    return result;
}
tools.utility.isToday = function (date) {
    return new Date(date).getYear() == new Date().getYear() && new Date(date).getMonth() == new Date().getMonth() && new Date(date).getDate() == new Date().getDate()
}
tools.utility.scrollTop = function () {
    if (Modernizr.touch && $(".divHeader").offset()) {
        $('html, body').animate({
            scrollTop: $(".divHeader").offset().top
        }, 1500);
    } else if ($(".divHeader ").offset()) {
        $('html, body').animate({
            scrollTop: $(".divHeader").offset().top
        }, 1500);
    }
}
tools.utility.browserIsOk = function () {

    if (navigator.appVersion.indexOf('Trident') > -1)
        return (parseFloat($.browser.version) > parseFloat('7.0'))

    else

        if ($.browser.mozilla == true || $.browser.chrome == true)

            return ($.browser.mozilla && parseFloat($.browser.version) > parseFloat('13.0')) || ($.browser.chrome && parseFloat($.browser.version) > parseFloat('15.0'))


        else
            return true


}
/////////////////////

tools.storage = {
    save: function (key, jsonData, expirationMin) {
        if (!Modernizr.localstorage) { return false; }
        if (!expirationMin) {
            expirationMin = 5000;
        }
        var expirationMS = expirationMin * 60 * 1000;
        var record = { value: JSON.stringify(jsonData), timestamp: new Date().getTime() + expirationMS }
        localStorage.setItem(key, JSON.stringify(record));
        return jsonData;
    },
    load: function (key) {
        if (!Modernizr.localstorage) { return false; }
        var record = JSON.parse(localStorage.getItem(key));
        if (!record) { return false; }
        return (new Date().getTime() < record.timestamp && JSON.parse(record.value));
    }
}
tools.cookie = {
    setCookie: function (cname, cvalue, exdays) {
        var d = new Date();
        if (!exdays) {
            exdays = 15;
        }
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toGMTString();
        document.cookie = cname + "=" + cvalue + "; " + expires + ("; path=/");
    },
    getCookie: function (cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i].trim();
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    }
}

tools.getClientID = function () {
    var cookie_expire_date = new Date(new Date().getTime() + (8 * 7 * 86400000));
    var vid = tools.cookie.getCookie('ClientID');
    if (vid && vid != '') {
    } else {
        tools.cookie.setCookie('ClientID', tools.generateClientID(), cookie_expire_date);
    }
    return tools.cookie.getCookie('ClientID');
}

tools.clearClientID = function () {
    tools.cookie.setCookie('ClientID', '');
}

tools.generateClientID = function () {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
//Array extension
Array.prototype.extend = function (other_array) {
    /* you should include a test to check whether other_array really is an array */
    other_array.forEach(function (v) { this.unshift(v) }, this);
}

//Array extension
Array.prototype.append = function (other_array) {
    /* you should include a test to check whether other_array really is an array */
    other_array.forEach(function (v) { this.push(v) }, this);
}
if (typeof String.prototype.startsWith != 'function') {
    // see below for better implementation!
    String.prototype.startsWith = function (str) {
        return this.indexOf(str) == 0;
    };
}

tools.utility.queryString = new Object();
window.location.search.replace(
new RegExp("([^?=&]+)(=([^&]*))?", "g"),
function ($0, $1, $2, $3) {
    tools.utility.queryString[$1.toLowerCase()] = $3.toLowerCase();
}
);

tools.utility.redirect = function (dest) {
    window.location.href = window.applicationBaseUrl + dest;
}

tools.utility.sort = function (myArray, strSort) {
    var sorts = strSort.split(",").map(function (v, i) {
        var o = v.match(/\s(asc|desc)$/i);
        if (o) {
            return { "prop": v.replace(/\s(asc|desc)$/i, "").replace(/^\s+|\s+$/, ""), "order": o[1].toLowerCase() };
        } else {
            return { "prop": v, "order": "asc" };
        }
    });
    myArray.sort(function (a, b) {
        var av, bv;
        for (var i = 0; i < sorts.length; i++) {
            av = a[sorts[i]["prop"]] || 0;
            bv = b[sorts[i]["prop"]] || 0;
            if (sorts[i]["order"] == "asc") {
                if (av > bv) {
                    return 1;
                } else if (bv > av) {
                    return -1;
                }
            } else {
                if (av > bv) {
                    return -1;
                } else if (bv > av) {
                    return 1;
                }
            }
        }
        return 0;
    });
    return myArray;
}
