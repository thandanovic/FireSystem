
$(document).ready(function () {
    setTimeout(function () { Utility.showErrorMessage() }, 100);
    if ($.validator) {
        $.validator.setDefaults({
            ignore: ".ignore, :disabled,:hidden"
        });
    }

    jQuery.extend(jQuery.validator.messages, {
        required: "Obavezno polje.",
        remote: "Please fix this field.",
        email: "Please enter a valid email address.",
        url: "Please enter a valid URL.",
        date: "Please enter a valid date.",
        dateISO: "Please enter a valid date (ISO).",
        number: "Please enter a valid number.",
        digits: "Please enter only digits.",
        creditcard: "Please enter a valid credit card number.",
        equalTo: "Please enter the same value again.",
        accept: "Please enter a value with a valid extension.",
        maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
        minlength: jQuery.validator.format("Please enter at least {0} characters."),
        rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
        min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
    });

    $('.delete-row').on('click', function () {
        var URL = $(this).attr('data-ref');

        $.ajax({
            url: URL,
            type: 'GET'
        }).done(function (data) {
            window.location = data;
        });

    });

    if (jQuery.validator != null) {
        jQuery.validator.addMethod("date", function (value, element) {
            if (value != null && value != "") {
                if (!moment(value, "DD.MM.YYYY", true).isValid() && !moment(value, "D.M.YY", true).isValid()) {
                    return false;
                }
            }
            return true;
        }, "Datum nije validan.");
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





    $(window).on('hashchange', function () {
        mySpinner(true);
    });

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

$(document).ajaxComplete(function (event, request, settings) {
    if (settings.data !== undefined && (settings.data.includes('zapisnikGrid') || settings.data.includes('radniNalogGrid'))) {
        function deleteRow() {
            var txt;
            var r = confirm("Da li zelite obrisati?");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }
        AttachReport();
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
            if (spinner != null) {
                spinner.stop();
                spinner = null;
                $('#waitSpinner').html("");
                $('#overlay').hide(); //overlay  
            }
        }, 1000);  //timeout – just to show the spinner for a while
    }
};