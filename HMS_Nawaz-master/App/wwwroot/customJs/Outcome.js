function getOutcomeFromModel() {
    var models = [];

    var model = {};
    model.PatientID = $("#Outcome_PatientID").val();
    if (model.PatientID == "") {
        model.PatientID = 0;
    }
    model.Id = $("#Outcome_Id").val();
    if (model.Id == "") {
        model.Id = 0;
    }
    model.Date = $("#Outcome_Date").val();
    model.Indication = $("#Outcome_Indication").val();

    models.push(model);
    $("#OutcomeModelsJson").val(JSON.stringify(models));
    var modelsJson = $("#OutcomeModelsJson").val();

    // Submit the form using AJAX
    $.ajax({
        type: "POST",
        url: "/Patient/Outcome",
        data: {
            model: modelsJson
        },
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",

        success: function (msg) {
            $("#OutcomeSuccessMessage").text(msg).show();
            $("#OutcomeBtn").val("Successfull").prop("disabled", true);
        },
        error: function (xhr, status, error) {
            // Handle the error response
        }
    });

    $('#AddOutcomeForm input[type=text]').each(function () {
        $(this).val('');
    });
    $('#AddOutcomeForm textarea').each(function () {
        $(this).val('');
    });
}

