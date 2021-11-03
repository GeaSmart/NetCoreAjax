// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { type, error } = require("jquery");

// Write your JavaScript code.

showPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-title").html(title); //updating the modal title in the _Layout
            $("#form-modal .modal-body").html(res); //updating the modal body in the _Layout
            $("#form-modal").modal('show'); //showing the modal with jquery
        }
    })
}

jQueryAjaxPost = form => {
    $ajax({
        type: 'POST',
        url: form.Actions,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.isValid) {
                $("#list-all").html(res.html);
                $("#form-modal .modal-title").html('');
                $("#form-modal .modal-body").html('');
                $("#form-modal").modal('hide');
            }
            else {
                $("#form-modal .modal-body").html(res.html);
            }
        },
        error: function (err) {
            console.log(err);
        }

    })
}