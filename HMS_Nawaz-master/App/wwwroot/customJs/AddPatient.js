﻿ 

function getPatientFormModels() {
    var models = [];
    var model = {};

    model.SerialNumber = $("#PatientModel_SerialNumber").val();
    model.PatientID = $("#PatientModel_PatientID").val();
    if (model.PatientID=="") {
        model.PatientID = 0;
    }
    model.Dr_ID = $("#PatientModel_Dr_ID").val();
    model.CADSNumber = $("#PatientModel_CADSNumber").val();
    model.OPDNumber = $("#PatientModel_OPDNumber").val();
    model.Name = $("#PatientModel_Name").val();
    model.Age = $("#PatientModel_Age").val();
    model.Gender = $("#PatientModel_Gender").val();
    model.DateOfBirth = $("#PatientModel_DateOfBirth").val();
   
    model.Address_ID = $("#PatientModel_Address_ID").val();

    model.State = $("#PatientModel_State").val();
    model.City = $("#PatientModel_City").val();
    model.Street = $("#PatientModel_Street").val();
    model.ZipCode = $("#PatientModel_ZipCode").val();

    if (model.Address_ID == "") {
        model.Address_ID = 0;
    }
    model.PhoneNumber = $("#PatientModel_PhoneNumber").val();
    model.AlternateNumber = $("#PatientModel_AlternateNumber").val();
   
    model.Email = $("#PatientModel_Email").val();
    model.SeniorResident = $("#PatientModel_SeniorResident").val();
    model.JuniorResident = $("#PatientModel_JuniorResident").val();
    
    model.Title = $("#PatientModel_Title").val();
    model.SubCategoryTitle = $("#PatientModel_SubCategoryTitle").val();
    model.Dr_Name = $("#PatientModel_Dr_Name").val();


    models.push(model);
    $("#ModelsJson").val(JSON.stringify(models));

    var modelsJson = $("#ModelsJson").val();

    // Submit the form using AJAX
    $.ajax({
        type: "POST",
        url: "/Patient/AddPatient",
        data: {
            model: modelsJson
        },
        contentType: "application/x-www-form-urlencoded",
        dataType: "json",

        success: function (msg) {
            $("#PatientsuccessMessage").text(msg).show();
            $("#PatientBtn").val("Successfull").prop("disabled", true);
        },
        error: function (xhr, status, error) {
            // Handle the error response
        }
    });

    $('#AddPatient input[type=text]').each(function () {
        $(this).val('');
    });

    

}

