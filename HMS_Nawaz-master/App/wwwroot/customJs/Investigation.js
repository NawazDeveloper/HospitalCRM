
function getInvestigationModels() {
    var models = [];
    var model = {};

    model.Day = $("#InvestigationModel_Day").val();
    model.BloodSugar = $("#InvestigationModel_BloodSugar").val();
    model.TFT = $("#InvestigationModel_TFT").val();
    model.USG = $("#InvestigationModel_USG").val();
    model.SONOMMOGRAPHY = $("#InvestigationModel_SONOMMOGRAPHY").val();
    model.CECT = $("#InvestigationModel_CECT").val();
    model.MRI = $("#InvestigationModel_MRI").val();
    model.FNAC = $("#InvestigationModel_FNAC").val();
    model.TrucutBiopsy = $("#InvestigationModel_TrucutBiopsy").val();
    model.ReceptorStatus = $("#InvestigationModel_ReceptorStatus").val();
    model.MRCP = $("#InvestigationModel_MRCP").val();
    model.ERCP = $("#InvestigationModel_ERCP").val();
    model.EndoscopyUpperGI = $("#InvestigationModel_EndoscopyUpperGI").val();
    model.EndoscopyLowerGI = $("#InvestigationModel_EndoscopyLowerGI").val();
    model.PETCT = $("#InvestigationModel_PETCT").val();
    model.TumorMarkers = $("#InvestigationModel_TumorMarkers").val();
    model.IVP = $("#InvestigationModel_IVP").val();
    model.MCU = $("#InvestigationModel_MCU").val();
    model.RGU = $("#InvestigationModel_RGU").val();
   
    model.OtherO = $("#InvestigationModel_OtherO").val();

    models.push(model);
    $("#ModelsJson").val(JSON.stringify(models));
    return true;
}
$("#Investigationform").submit(function (event) {

    event.preventDefault();
    // var formData = $(this).serialize();
    var modelsJson = $("#ModelsJson").val();

    // Submit the form using AJAX
    $.ajax({
        type: "POST",
        url: "/Patient/AddInvestigation",
        data: {
            data: modelsJson
        },
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",

        success: function (result) {
            
            var tableData = JSON.parse(result);

            if (tableData != null) {
                $("#InvestigationsuccessMessage").text("Data added successfully").show();
                $("#AddInvestigation").val("Add more").prop("disabled", false);
                
            } else {
                $("#InvestigationerrorMessage").text("something went wrong").show();
            }

            $("#example5 tbody").empty();

            // Loop through the data and add a row for each item
            $.each(tableData, function (index, item) {
                $("#InvestigationModel_PatientId").val(item.PatientID)
                var row = $("<tr></tr>");
                row.append($("<td></td>").text(item.Day));
                row.append($("<td></td>").text(item.BloodSugar));
                row.append($("<td></td>").text(item.TFT));
                row.append($("<td></td>").text(item.USG));
                row.append($("<td></td>").text(item.CECT));
                row.append($("<td></td>").text(item.MRCP));
                row.append($("<td></td>").html("<a id='EditDay' onclick='OnDeleteInvestigation(" + item.Id + ")' class='btn btn-primary shadow btn-xs sharp mr-1'><i class='fa fa-trash'></i></a>"));
                $("#example5 tbody").append(row);
            });
        },
        error: function (xhr, status, error) {
            // Handle the error response
        }
    });

    $('#Investigationform input[type=text]').each(function () {
        $(this).val('');
    });
});


function EditInvestigationModels() {
    var models = [];
    var model = {};
    model.Id = $("#InvestigationList_0__Id").val();
    model.PatientID = $("#InvestigationList_0__PatientID").val();
    if (model.PatientID == "") {
        model.PatientID = 0;
    }
   
    if (model.Id == "") {
        model.Id = 0;
    }
   
    model.BloodSugar = $("#InvestigationList_0__BloodSugar").val();
    model.USG = $("#InvestigationList_0__USG").val();
    model.CECT = $("#InvestigationList_0__CECT").val();
    model.MRI = $("#InvestigationList_0__MRI").val();
    model.FNAC = $("#InvestigationList_0__FNAC").val();
    model.TrucutBiopsy = $("#InvestigationList_0__TrucutBiopsy").val();
    model.ReceptorStatus = $("#InvestigationList_0__ReceptorStatus").val();
    model.MRCP = $("#InvestigationList_0__MRCP").val();
    model.ERCP = $("#InvestigationList_0__ERCP").val();
    model.EndoscopyUpperGI = $("#InvestigationList_0__EndoscopyUpperGI").val();
    model.EndoscopyLowerGI = $("#InvestigationList_0__EndoscopyLowerGI").val();
    model.PETCT = $("#InvestigationList_0__PETCT").val();
    model.TumorMarkers = $("#InvestigationList_0__TumorMarkers").val();
    model.SONOMMOGRAPHY = $("#InvestigationList_0__SONOMMOGRAPHY").val();
    model.TFT = $("#InvestigationList_0__TFT").val();
    model.IVP = $("#InvestigationList_0__IVP").val();
    model.MCU = $("#InvestigationList_0__MCU").val();
    model.RGU = $("#InvestigationList_0__RGU").val();
    model.Day = $("#InvestigationList_0__Day").val();
    model.OtherO = $("#InvestigationList_0__OtherO").val();
    models.push(model);
    $("#InvestigationModelsJson").val(JSON.stringify(models));

    var modelsJson = $("#InvestigationModelsJson").val();

    // Submit the form using AJAX
    $.ajax({
        type: "POST",
        url: "/Patient/AddInvestigation",
        data: {
            data: modelsJson
        },
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",

        success: function (result) {
           
            var tableData = JSON.parse(result);

            if (tableData != null) {
                $("#InvestigationsuccessMessage").text("Data updated successfully").show();

            } else {
                $("#InvestigationerrorMessage").text("something went wrong").show();
            }

            $("#myTable tbody").empty();

            // Loop through the data and add a row for each item
            $.each(tableData, function (index, item) {
                var row = $("<tr></tr>");
                row.append($("<td></td>").text(item.Day));
                row.append($("<td></td>").text(item.BloodSugar));
                row.append($("<td></td>").text(item.TFT));
                row.append($("<td></td>").text(item.USG));
                row.append($("<td></td>").text(item.CECT));
                row.append($("<td></td>").text(item.MRCP));
                row.append($("<td></td>").html("<a id='EditDay' onclick='OnClickEdit(" + item.Id + ")' class='btn btn-primary shadow btn-xs sharp mr-1'><i class='fa fa-pencil'></i></a>"));
                $("#myTable tbody").append(row);
            });
        },
        error: function (xhr, status, error) {
            // Handle the error response
        }
    });

    $('#investigation input[type=text]').each(function () {
        $(this).val('');
    });
    $('#investigation input[type=date]').each(function () {
        $(this).val('');
    });

}

function OnDeleteInvestigation(Id) {
    var selectedValue = Id;
    var PatientID = $("#InvestigationModel_PatientId").val();
    if (PatientID == "") {
        PatientID = 0;
    }
    $.ajax({
        url: "/Patient/DeleteInvestigation",
        type: 'GET',
        data: { InvestigationId: Id, PatientID: PatientID },
        success: function (result) {
            var tableData = JSON.parse(result);
            $("#example5 tbody").empty();

            // Loop through the data and add a row for each item
            $.each(tableData, function (index, item) {
                var row = $("<tr></tr>");
                row.append($("<td></td>").text(item.Day));
                row.append($("<td></td>").text(item.CBC));
                row.append($("<td></td>").text(item.RFT));
                row.append($("<td></td>").text(item.BloodSugar));
                row.append($("<td></td>").text(item.LFT));
                row.append($("<td></td>").text(item.TSPAG));
                row.append($("<td></td>").text(item.TFT));
                row.append($("<td></td>").html("<a id='EditDay' onclick='OnDeleteInvestigation(" + item.Id + ")' class='btn btn-primary shadow btn-xs sharp mr-1'><i class='fa fa-trash'></i></a>"));
                $("#example5 tbody").append(row);
            });

        }
    });
}