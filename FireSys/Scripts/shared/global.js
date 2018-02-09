
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

    function clearValidation(formElement) {
        var validator = $(formElement).validate();
        $('[name]', formElement).each(function () {
            validator.successList.push(this);
            validator.showErrors();
        });
        validator.resetForm();
        validator.reset();
    }

    $('#zapisnikModal').on('hide.bs.modal', function () {
        clearValidation($('#zapisnikForm'));
    })
  
    $('#zapisnikModal').on('hidden.bs.modal', function () {
        $('#zapisnikForm').find('input:text, input:password, select, textarea').not('.exclude-reset').val('');
        $('#zapisnikForm').find('input[name="Zapisnik.BrojZapisnika"]').val('0');
        $('#zapisnikForm').find('input:radio, input:checkbox').prop('checked', false);
        clearValidation($('#zapisnikForm'));
    })

    $(document).ajaxStart(mySpinner(true)).ajaxStop(mySpinner(false));
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


function AttachReport() {
    $('.print-report').off('click').on('click', function (e) {
        e.preventDefault();
        var dataUrl = $(this).attr('data-url');
        var dataID = $(this).attr('data-id');
        mySpinner(true);
        $.ajax({
            url: dataUrl,
            data: { id: dataID },
            dataType: 'json',
            type: "POST",
            beforeSend: function () {

            },
            complete: function () {

            },
            success: function (data) {
                $('#modal_report_body').html(data.content);
                mySpinner(false);
            }
        });
    });
}

var spinner = null;
function mySpinner(isStart) {
    var opts = {
        lines: 19, // The number of lines to draw
        length: 25, // The length of each line
        width: 7, // The line thickness
        radius: 45, // The radius of the inner circle
        scale: 0.85, // Scales overall size of the spinner
        corners: 0.5, // Corner roundness (0..1)
        color: '#ff0000', // CSS color or array of colors
        fadeColor: 'transparent', // CSS color or array of colors
        opacity: 0, // Opacity of the lines
        rotate: 69, // The rotation offset
        direction: 1, // 1: clockwise, -1: counterclockwise
        speed: 1, // Rounds per second
        trail: 49, // Afterglow percentage
        fps: 20, // Frames per second when using setTimeout() as a fallback in IE 9
        zIndex: 2e9, // The z-index (defaults to 2000000000)
        className: 'spinner', // The CSS class to assign to the spinner
        top: '50%', // Top position relative to parent
        left: '50%', // Left position relative to parent
        position: 'absolute' // Element positioning
    };

    if (isStart && !spinner) {
        spinner = new Spinner(opts).spin();
        $('#waitSpinner').html(spinner.el);
        $('#overlay').show(); //overlay  
    }
    else if (spinner) {
        setTimeout(function () {
            spinner.stop();
            spinner = null;
            $('#waitSpinner').remove();
            $('#overlay').remove(); //overlay  
        }, 1000);  //timeout – just to show the spinner for a while
    }
};