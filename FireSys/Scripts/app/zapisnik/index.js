var selectedIDs;

function OnSelectionChanged(s, e) {
    s.GetSelectedFieldValues("ZapisnikId", GetSelectedFieldValuesCallback);
}
function GetSelectedFieldValuesCallback(values) {
    //Capture all selected keys
    selectedIDs = values.join(',');
}

function UObradiClick(s, e) {
    mySpinner(true);
    setTimeout(function () {
        $.ajax({
            url: "@Url.Action('ChangeStatus', 'Zapisnik')",
            data: { IDs: selectedIDs, status: 1 },
            method: "POST",
            dataType: "json"
        }).done(function (response) {
            window.location.reload();
            mySpinner(false);
        });

    }, 2000);
}

function ZavrsenClick(s, e) {
    mySpinner(true);
    setTimeout(function () {
        $.ajax({
            url: "@Url.Action('ChangeStatus', 'Zapisnik')",
            data: { IDs: selectedIDs, status: 2 },
            method: "POST",
            dataType: "json"
        }).done(function (response) {
            window.location.reload();
            mySpinner(false);
        });
    }, 2000);
}

function OnClick(s, e) {
    var actionParams = $("form").attr("action").split("?OutputFormat=");
    actionParams[1] = s.GetMainElement().getAttribute("OutputFormatAttribute");
    $("form").attr("action", actionParams.join("?OutputFormat="));
}

function OnCreateClick(s, e) {
    $("#zapisnikModal").modal('show');
}
var commandName;
function GridBeginCallback(s, e) {
    commandName = e.commandName;
    //Pass all selected keys to GridView callback action
    e.customArgs["selectedIDs"] = selectedIDs;
}

function GridEndCallback(s, e) {
    console.log(commandName);
    commandName = null;
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