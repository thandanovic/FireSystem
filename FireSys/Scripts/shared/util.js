var Utility = function () {
    function showErrorMessage(message) {
        selector = $(".alert-location");
        if (message != undefined && message.length > 0) {
            $.notify(message, "error");
        }
        else if ($("#AlertMessage").val()) {
            if (selector != undefined && selector.length > 0) {
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
    function initLocationChangeEvent() {
        if ($(".dropdown-regije, .dropdown-klijenti").length > 0) {

            $(".dropdown-regije, .dropdown-klijenti").change(function () {
                var regijaId = $(".dropdown-regije").val();
                var klijentId = $(".dropdown-klijenti").val();
                var dropdownLokacija = $(".dropdown-lokacije:visible");
                if (!klijentId) {
                    klijentId = "0";
                }
                if (!regijaId) {
                    regijaId = "0";
                }
                $.getJSON("/Lokacija/GetLokacijeByKlijentRegija",
                    { regijaId: regijaId, klijentId: klijentId },
                    function (Data) {
                        $(dropdownLokacija).empty();
                        $(dropdownLokacija).append("<option value=''>Odaberi lokaciju</option>");
                        if (Data != null) {
                            $.each(Data, function (index, fooListItem) {
                                $(dropdownLokacija).append("<option value='" + fooListItem.Value + "'>" + fooListItem.Text + "</option>");
                            });
                        }
                        else
                            alert("Odabrani klijent nema lokacija. Unesite lokaciju.");
                    });

            });
        }
    }
   

    return {
        showErrorMessage: showErrorMessage,
        initLocationChangeEvent: initLocationChangeEvent
    }
}();


