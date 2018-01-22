
$(document).ready(function () {
    setTimeout(function () { Utility.showErrorMessage() }, 100);
    if ($.validator) {
        $.validator.setDefaults({
            ignore: ".ignore, :disabled,:hidden"
        });
    }



    $('.delete-row').on('click', function () {
        var URL = $(this).attr('data-ref');

        $.get(URL, function (data) {
            Utility.showErrorMessage(data);
        });

        //$.ajax({
        //    url: URL,
        //    success: 
        //});

    });

    if (jQuery.validator != null) {
        jQuery.validator.addMethod("date", function (value, element) {
            if (value != null && value != "") {
                if (!moment(value, "DD.MM.YYYY", true).isValid() && !moment(value, "D.M.YY", true).isValid()) {
                    return false;
                }
            }
            return true;
        }, "Date is not valid.");
    }
    $('.date').datepicker({
        format: "dd.mm.yyyy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0"
    });

    Utility.initLocationChangeEvent();

    $('#zapisnikModal').on('shown.bs.modal', function () {

        var validator = $('#zapisnikForm').validate();
        validator.resetForm();
    })

    $('#zapisnikModal').on('hidden.bs.modal', function () {

        var validator = $('#zapisnikForm').validate();
        validator.resetForm();

        //$('.field-validation-error').remove();

        $('#zapisnikForm').find('input:text, input:password, select, textarea').not('.exclude-reset').val('');
        $('#zapisnikForm').find('input[name="Zapisnik.BrojZapisnika"]').val('0');
        $('#zapisnikForm').find('input:radio, input:checkbox').prop('checked', false);


    })
});

/* Global ajax handlers */
$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    //toastr.clear();
    if (jqxhr.responseJSON) {
        if (jqxhr.responseJSON.errorMessage) {
            Utility.showErrorMessage(jqxhr.responseJSON.errorMessage);
            //  Ladda.stopAll();
        } else if (jqxhr.responseJSON.redirectUrl) {
            location.href = jqxhr.responseJSON.redirectUrl;
        } else {
            Utility.showErrorMessage("An internal server error occurred.");
        }
    }
});