function getCaseSheetFromModel() {
    var models = [];

    var model = {};

    model.PatientID = $("#CaseSheet_PatientID").val();
    if (model.PatientID == "") {
        model.PatientID = 0;
    }
    model.Id = $("#CaseSheet_Id").val();
    if (model.Id == "") {
        model.Id = 0;
    }
    model.complaintsOf = $("#CaseSheet_complaintsOf").val();
    model.HistoryOfPresentingIllness = $("#CaseSheet_HistoryOfPresentingIllness").val();
    model.PastHistory = $("#CaseSheet_PastHistory").val();
    model.PersonalHistory = $("#CaseSheet_PersonalHistory").val();
    model.Diet = $("#CaseSheet_Diet").val();
    model.Appetite = $("#CaseSheet_Appetite").val();
    model.Sleep = $("#CaseSheet_Sleep").val();
    model.Bowel = $("#CaseSheet_Bowel").val();
    model.Bladder = $("#CaseSheet_Bladder").val();
    model.Addiction = $("#CaseSheet_Addiction").val();
    model.FamilyHistory = $("#CaseSheet_FamilyHistory").val();
    model.Vitals = $("#CaseSheet_Vitals").val();
    model.BP = $("#CaseSheet_BP").val();
    model.PR = $("#CaseSheet_PR").val();
    model.RR = $("#CaseSheet_RR").val();
    model.Temp = $("#CaseSheet_Temp").val();
    model.SpO2 = $("#CaseSheet_SpO2").val();

    
    model.Icterus = $("#CaseSheet_Icterus").val();
    model.Cyanosis = $("#CaseSheet_Cyanosis").val();
    model.Clubbing = $("#CaseSheet_Clubbing").val();
    model.PedalEdema = $("#CaseSheet_PedalEdema").val();

    model.Pallor = $("#CaseSheet_Pallor").val();
    model.Lymphadenopathy = $("#CaseSheet_Lymphadenopathy").val();


    model.CNS = $("#CaseSheet_CNS").val();
    model.CardiovascularSystem = $("#CaseSheet_CardiovascularSystem").val();
    model.RespiratorySystem = $("#CaseSheet_RespiratorySystem").val();
    model.PerAbdomen = $("#CaseSheet_PerAbdomen").val();
    model.Umbilicus = $("#CaseSheet_Umbilicus").val();
    model.Movements = $("#CaseSheet_Movements").val();
    model.DilatedVeins = $("#CaseSheet_DilatedVeins").val();
    model.VisiblePeristalsis = $("#CaseSheet_VisiblePeristalsis").val();
    model.Pulsations = $("#CaseSheet_Pulsations").val();
    model.ScarMarks = $("#CaseSheet_ScarMarks").val();
    model.LocalisedSweling = $("#CaseSheet_LocalisedSweling").val();
    model.Pulpation = $("#CaseSheet_Pulpation").val();
    model.Temprature = $("#CaseSheet_Temprature").val();
    model.TendernessRebound = $("#CaseSheet_TendernessRebound").val();
    model.GuardingRigidity = $("#CaseSheet_GuardingRigidity").val();
    model.Percussion = $("#CaseSheet_Percussion").val();
    model.ShiftingDullness = $("#CaseSheet_ShiftingDullness").val();
    model.FluidThrill = $("#CaseSheet_FluidThrill").val();
    model.Auscultation = $("#CaseSheet_Auscultation").val();
    model.BowelSounds = $("#CaseSheet_BowelSounds").val();
    model.Remark = $("#CaseSheet_Remark").val();
    model.Value1 = $("#CaseSheet_Value1").val();
    model.Value2 = $("#CaseSheet_Value2").val();

    models.push(model);
    $("#CaseSheetModelsJson").val(JSON.stringify(models));
    return true;
}

$("#AddCaseSheetForm").submit(function (event) {

    event.preventDefault();
    // var formData = $(this).serialize();
    var modelsJson = $("#CaseSheetModelsJson").val();

    // Submit the form using AJAX
    $.ajax({
        type: "POST",
        url: "/Patient/CaseSheet",
        data: {
            model: modelsJson
        },
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",

        success: function (msg) {
            $("#CaseSheetMessage").text(msg).show();
            $("#CaseSheetBtn").val("Successfull").prop("disabled", true);
        },
        error: function (xhr, status, error) {
            // Handle the error response
        }
    });

    $('#AddCaseSheetForm input[type=text]').each(function () {
        $(this).val('');
    });
    $('#AddCaseSheetForm textarea').each(function () {
        $(this).val('');
    });
});