
$(document).ready(function () {
    setTimeout(function () { Utility.showErrorMessage() }, 100);
    if ($.validator) {
        $.validator.setDefaults({
            ignore: ".ignore, :disabled,:hidden"
        });
    }
});

/* Global ajax handlers */
$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    toastr.clear();
    if (jqxhr.responseJSON) {
        if (jqxhr.responseJSON.errorMessage) {
            Utility.showErrorMessage(jqxhr.responseJSON.errorMessage);
            Ladda.stopAll();
        } else if (jqxhr.responseJSON.redirectUrl) {
            location.href = jqxhr.responseJSON.redirectUrl;
        } else {
            Utility.showErrorMessage("An internal server error occurred.");        
        }
    }
});