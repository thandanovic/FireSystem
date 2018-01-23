
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

    if ($(".dropdown-regije, .dropdown-klijenti").length > 0) {
        $(".dropdown-regije, .dropdown-klijenti").change(function () {
            var regijaId = $(".dropdown-regije").val();
            var klijentId = $(".dropdown-klijenti").val();
            if (!klijentId) {
                klijentId = "0";
            }
            if (!regijaId) {
                regijaId = "0";
            }
            $.getJSON("../Lokacija/GetLokacijeByKlijentRegija",
            { regijaId: regijaId, klijentId: klijentId },
            function (Data) {
                $(".dropdown-lokacije").empty();
                $(".dropdown-lokacije").append("<option value=''>Odaberi lokaciju</option>");
                if (Data != null) {
                    $.each(Data, function (index, fooListItem) {
                        $(".dropdown-lokacije").append("<option value='" + fooListItem.Value + "'>" + fooListItem.Text + "</option>");
                    });
                }
                else
                    alert("Odabrani klijent nema lokacija. Unesite lokaciju.");
            });

        });
    }

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