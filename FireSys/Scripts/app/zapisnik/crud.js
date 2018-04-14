$(function () {

    $('.header-left').html('<h1>Detalji zapisnika</h1>');

    if ($('#Zapisnik_ZapisnikTipId').val() != 1) {
        $('.zapisnik-container').css("grid-template-areas", "'zapisnik hidranti' 'zapisnik hidranti'");
        $('.page-header-right').text('Lista hidranata na lokaciji');

        var hidrantTip = JSON.parse($("#boxHidrantTip").val());
        var instalacije = JSON.parse($("#boxInstalacije").val());
        var kompletnost = JSON.parse($("#boxKompletnost").val());
        var promjerMlaznice = JSON.parse($("#boxPromjerMlaznice").val());

        $("#hidrantiGrid").jsGrid({
            width: "100%",
            height: "100%",
            inserting: statusId != 2,
            noDataContent: "Nema podataka",
            editing: statusId != 2,
            sorting: true,
            data: [],
            paging: true,
            autoload: true,
            pageloading: true,
            filtering: true,
            controller: {
                loadData: function () {
                    var d = $.Deferred();

                    $.ajax({
                        url: getStandpipesUrl,
                        data: { zapisnikId: parseInt($("#ZapisnikId").val()) },
                        method: "GET",
                        dataType: "json"
                    }).done(function (response) {
                        d.resolve(JSON.parse(response.hidranti));
                    });

                    return d.promise();
                },
                insertItem: function (insertingItem) {
                    insertingItem["LokacijaId"] = parseInt($("#LokacijaId").val());
                    insertingItem["ZapisnikId"] = parseInt($("#ZapisnikId").val());
                    var d = $.Deferred();
                    $.ajax({
                        type: "GET",
                        url: pushStandpipeUrl,
                        data: insertingItem
                    }).success(function (response) {
                        d.resolve(JSON.parse(response.item));
                    }).error(function (xhr, ajaxOptions, thrownError) {
                        d.resolve();
                    });

                    return d.promise();
                },
                updateItem: function (updatingItem) {
                    var d = $.Deferred();
                    $.ajax({
                        type: "GET",
                        url: updateStandpipeUrl,
                        data: updatingItem
                    }).success(function (response) {
                        d.resolve(JSON.parse(response.item));
                    }).error(function (xhr, ajaxOptions, thrownError) {
                        d.resolve(updatingItem);
                    });

                    return d.promise();
                },
                deleteItem: function (deletingItem) {
                    var d = $.Deferred();
                    $.ajax({
                        type: "POST",
                        url: popStandpipeUrl,
                        data: deletingItem
                    }).success(function (response) {
                        d.resolve();
                    }).error(function (xhr, ajaxOptions, thrownError) {
                        d.resolve();
                    });

                    return d.promise();
                }
            },

            fields: [
                { name: "Oznaka", type:"text", validate: "required" },
                { name: "InstalacijaId", title: "Inst", type: "select", items: instalacije, valueField: "InstalacijaId", textField: "Naziv", validate: "required"},
                { name: "HidrostatickiPritisak", title: "Hidros. pritisak", type:"text", validate: "required" },
                { name: "HidrodinamickiPritisak", title: "Hidrod. pritisak", type:"text", validate: "required" },
                { name: "PromjerMlazniceId", title: "Promjer", type: "select", items: promjerMlaznice, valueField: "PromjerMlazniceId", textField: "Promjer", validate: "required" },
                { name: "Protok", type:"text", validate: "required" },
                { name: "KompletnostId", title: "Kompletnost", type: "select", items: kompletnost, valueField: "KompletnostId", textField: "Naziv", validate: "required" },
                { name: "HidrantTipId", title: "Tip", type: "select", items: hidrantTip, valueField: "HidrantTipId", textField: "Naziv", validate: "required" },
                { name: "Mikrolokacija", type:"text", validate: "required" },
                {
                    type: "control",
                    title: "Akcije",
                    deleteButton: statusId != 2,
                    editButton: statusId != 2                   
                }              
            ]
        });


    } else {
        $('.page-header-right').text('Lista aparata na lokaciji');

        var ispravnost = JSON.parse($("#boxIspravnost").val());
        var tip = JSON.parse($("#boxTip").val());
        var vrsta = JSON.parse($("#boxVrste").val());

        $("#aparatiGrid").jsGrid({
            width: "100%",
            height: "100%",
            inserting: statusId != 2,
            noDataContent: "Not found",
            editing: statusId != 2,
            sorting: true,
            data: [],
            paging: true,
            autoload: true,
            pageloading: true,
            filtering: true,
            controller: {
                loadData: function () {
                    var d = $.Deferred();

                    $.ajax({
                        url: getExtinguishersUrl,
                        data: { zapisnikId: parseInt($("#ZapisnikId").val()) },
                        method: "GET",
                        dataType: "json"
                    }).done(function (response) {
                        d.resolve(JSON.parse(response.streamAparati));
                    });

                    return d.promise();
                },
                insertItem: function (insertingItem) {
                    insertingItem["LokacijaId"] = parseInt($("#LokacijaId").val());
                    insertingItem["ZapisnikId"] = parseInt($("#ZapisnikId").val());
                    var d = $.Deferred();
                    $.ajax({
                        type: "GET",
                        url: pushExtinguisherUrl,
                        data: insertingItem
                    }).success(function (response) {
                        d.resolve(JSON.parse(response.item));
                    }).error(function (xhr, ajaxOptions, thrownError) {
                        d.resolve();
                    });

                    return d.promise();
                },
                updateItem: function (updatingItem) {
                    var d = $.Deferred();
                    $.ajax({
                        type: "GET",
                        url: updateExtinguisherUrl,
                        data: updatingItem
                    }).success(function (response) {
                        d.resolve(JSON.parse(response.item));
                    }).error(function (xhr, ajaxOptions, thrownError) {
                        d.resolve(updatingItem);
                    });

                    return d.promise();
                },
                deleteItem: function (deletingItem) {
                    var d = $.Deferred();
                    $.ajax({
                        type: "POST",
                        url: popExtinguisherUrl,
                        data: deletingItem
                    }).success(function (response) {
                        d.resolve();
                    }).error(function (xhr, ajaxOptions, thrownError) {
                        d.resolve();
                    });

                    return d.promise();
                }

            },

            fields: [

                { name: "TipId", title: "Tip", type: "select", width: 70, items: tip, valueField: "VatrogasniAparatTipId", textField: "Naziv", validate: "required" },
                { name: "BrojAparata", title: "Broj aparata", type: "text", width: 50, validate: "required" },
                { name: "GodinaProizvodnje", title: "Godina proizvodnje", type: "number", width: 50, validate: "required" },
                { name: "VrijediDo", title: "Vrijedi do", type: "number", width: 50, validate: "required" },
                { name: "BrojKartice", title: "Broj kartice", type: "text", width: 50 },
                { name: "Napomena", type: "text" },
                { name: "IspravnostId", title: "Ispravnost", type: "select", width: 70, items: ispravnost, valueField: "IspravnostId", textField: "Naziv", validate: "required" },
                { name: "VrstaId", title: "Vrsta", type: "select", width: 70, items: vrsta, valueField: "VatrogasniAparatVrstaId", textField: "Naziv", validate: "required" },
                {
                    type: "control",
                    title: "Akcije",
                    deleteButton: statusId != 2,
                    editButton: statusId != 2
                    //itemTemplate: function (_, item) {
                    //    return '<a class="fa fa-edit" href="/Hidrants/Edit/' + item.ID + '"> </a> | <a class="fa fa-search" href="/Hidrants/Details/' + item.ID + '"> </a> | <a class="fa fa-trash-o" href="/Hidrants/Delete/' + item.ID + '"> </a>';
                    //}
                }
            ]
        });
    }
    });

function createWorkOrder(s, e) {
    var d = $.Deferred();
    $.ajax({
        type: "POST",
        url: "@Url.Action('CreateWorkOrder', 'Zapisnik')",
        data: { zapisnikID: $("#ZapisnikId").val() }
    }).success(function (response) {
        d.resolve();
        Utility.showErrorMessage(response.message);
    }).error(function (xhr, ajaxOptions, thrownError) {
        d.resolve();
    });

    return d.promise();
}

function returnOnOrderList(s, e) {
    window.location.href = '/Zapisnik/Index';
}

