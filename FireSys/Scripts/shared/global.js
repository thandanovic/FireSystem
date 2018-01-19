﻿
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


    if ($(".dropdown-klijenti").length > 0) {
        $(".dropdown-klijenti").change(function () {
            var klijentId = $(".dropdown-klijenti").val();
            $.getJSON("../Lokacija/GetLokacijeByKlijent",
                { klijentId: klijentId },
                function (Data) {
                    $(".dropdown-lokacije").empty();
                    $(".dropdown-lokacije").append("<option value='0'>--Odaberi lokaciju--</option>");
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