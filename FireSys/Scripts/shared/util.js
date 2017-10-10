var Utility = function () {
    function showErrorMessage(message) {
        selector = $(".alert-location");
        if (message != undefined && message.length>0) {
            $.notify(message, "error");
        }
        else if ($("#AlertMessage").val()) {
            if (selector != null || selector != undefined) {
                $(selector).notify($("#AlertMessage").val(),
                     {
                         position: "right",
                         className: "error"
                     }
                 );
            }
            else {
                $.notify($("#AlertMessage").val(), "error");
            }
        }
    }

    return {
        showErrorMessage: showErrorMessage
    }
}();