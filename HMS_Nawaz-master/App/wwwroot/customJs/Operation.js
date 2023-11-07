function getOperationFromModel() {
    var models = [];

    var model = {};
    model.PatientID = $("#Operation_PatientID").val();
    if (model.PatientID == "") {
        model.PatientID = 0;
    }
    model.Id = $("#Operation_Id").val();
    if (model.Id == "") {
        model.Id = 0;
    }
    model.Date = $("#Operation_Date").val();
    model.Indication = $("#Operation_Indication").val();
    model.Dr_ID = $("#Patient_Dr_ID").val();
    model.Anaesthetist = $("#Operation_Anaesthetist").val();
    model.OpertingSurgeon = $("#Operation_OpertingSurgeon").val();
    model.Position = $("#Operation_Position").val();
    model.Findings = $("#Operation_Findings").val();
    model.Procedure = $("#Operation_Procedure").val();
    model.Duration = $("#Operation_Duration").val();
    model.StepsOfOperation = $("#Operation_StepsOfOperation").val();
    model.Antibiotics = $("#Operation_Antibiotics").val();
    model.SpecimensSentFor = $("#Operation_SpecimensSentFor").val();
    model.PostOperativeInstructions = $("#Operation_PostOperativeInstructions").val();
   

    models.push(model);
    $("#OperationModelsJson").val(JSON.stringify(models));
    var modelsJson = $("#OperationModelsJson").val();

    // Submit the form using AJAX
    $.ajax({
        type: "POST",
        url: "/Patient/OperationSheet",
        data: {
            model: modelsJson
        },
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",

        success: function (msg) {
            $("#OperationSuccessMessage").text(msg).show();
            $("#OperationBtn").val("Successfull").prop("disabled", true);
        },
        error: function (xhr, status, error) {
            // Handle the error response
        }
    });

    $('#AddOperationForm input[type=text]').each(function () {
        $(this).val('');
    });
    $('#AddOperationForm textarea').each(function () {
        $(this).val('');
    });
}

