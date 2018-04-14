



function UObradiClick(s, e) {
    mySpinner(true);
    zapisnikGrid.GetSelectedFieldValues("ZapisnikId", function (values) {
        var selectedIDs = values.join(',');
        $.ajax({
            url: "ChangeStatus/Zapisnik",
            data: { IDs: selectedIDs, status: 1 },
            method: "POST",
            dataType: "json"
        }).done(function (response) {
            Utility.statusChanged();         
        });
    });        
}

function ZavrsenClick(s, e) {
    mySpinner(true);
    zapisnikGrid.GetSelectedFieldValues("ZapisnikId", function (values) {
        var selectedIDs = values.join(',');
        $.ajax({
            url: "ChangeStatus/Zapisnik",
            data: { IDs: selectedIDs, status: 2 },
            method: "POST",
            dataType: "json"
        }).done(function (response) {
            Utility.statusChanged();
        });
    });
}

function OnClick(s, e) {
    var actionParams = $("form").attr("action").split("?OutputFormat=");
    actionParams[1] = s.GetMainElement().getAttribute("OutputFormatAttribute");
    $("form").attr("action", actionParams.join("?OutputFormat="));
}

function OnCreateClick(s, e) {
    $("#zapisnikModal").modal('show');
}


$(function () {

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
});