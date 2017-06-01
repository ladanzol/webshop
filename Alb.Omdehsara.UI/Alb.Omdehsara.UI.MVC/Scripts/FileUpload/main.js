/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload();
    $('#fileupload').fileupload('option', {
            maxFileSize: 500000000,
            resizeMaxWidth: 1920,
            resizeMaxHeight: 1200,
            stop: function (e) {
                tools.utility.unblockUI();
                $(".progress").hide();
            },
            maxNumberOfFiles: parseInt($("#MaxUploadSize").val()),
            //submit: function (event, files) {
            //    var fileCount = files.originalFiles.length;
            //    if (fileCount > parseInt($("#MaxUploadSize").val())) {
            //        alert("The max number of files is " + maxFiles);
            //        throw 'This is not an error. This is just to abort javascript';
            //        return false;
            //    }
            //}
    });
    $('#fileupload').bind('fileuploadsubmit', function (e, data) {
        var inputs = data.context.find(':input');
        if (inputs.filter(function () {
                return !this.value && $(this).prop('required');
        }).first().focus().length) {
            data.context.find('button').prop('disabled', false);
            return false;
        }
        if ($("#fileupload .files tr").length > parseInt($("#MaxUploadSize").val())) {
            tools.showMessage("لطفا تعداد فایل کمتری را انتخاب نمایید.");
            return false;
        }
        data.formData = inputs.serializeArray();
    });

    $('#fileupload').bind('fileuploaddestroyed ', function (e, data) {
        /* your code, like : if (data.kind === "error") alert(data.message); */
        tools.utility.unblockUI();
    });

    //$('#fileupload').bind('fileuploadadd ', function (e, data) {
    //    var fileCount = data.files.length;
    //    if (fileCount > parseInt( $("#MaxUploadSize").val())) {
    //        alert("The max number of files is " + maxFiles);
    //        return false;
    //    }
    //});
});
