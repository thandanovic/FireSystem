var Utility = function () {
    function showErrorMessage(message) {
        selector = $(".alert-location");
        if (message != undefined && message.length>0) {
            $.notify(message, "error");
        }
        else if ($("#AlertMessage").val()) {
            if (selector!=undefined && selector.length>0) {
                $(selector).notify($("#AlertMessage").val(),
                     {
                         position: "right",
                         className: "error"
                     }
                 );
            }
            else {
                $.notify($("#AlertMessage").val(), {
                    position: "top center",
                    className: "error"
                });
            }
        } else if ($(".validation-summary-errors ul li:visible").length > 0) {
            var message = $(".validation-summary-errors").html();
            $(".validation-summary-errors").remove();
            if (selector != null || selector != undefined) {
                $(selector).notify(message,
                    {
                        position: "right",
                        className: "error"
                    }
                );
            }
            else {
                $.notify(message, "error");
            }
        }
    }

    return {
        showErrorMessage: showErrorMessage
    }
}();