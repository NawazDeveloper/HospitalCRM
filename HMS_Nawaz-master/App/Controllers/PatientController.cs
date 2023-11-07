using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Models.DbContext;
using App.Interface;
using System.Threading;
using App.DtoModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Models.ViewModel;
using Newtonsoft.Json;
using App.Models.DtoModel;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace App.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IPatientRepo _patient;
        private readonly IDocterRepo _docterRepo;
        public static long _PatientId = 0;
        public static long _InvestigationId = 0;
        static int _catId = 0;

        List<InvestigationModel> TabledataList = new List<InvestigationModel>();

        public PatientController(ApplicationContext context, IPatientRepo patient, IDocterRepo docterRepo)
        {
            _context = context;
            _patient = patient;
            _docterRepo = docterRepo;
        }

        [HttpGet("Index")]
        // GET: Patient
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            // Get the user's roles from the ClaimsPrincipal
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            string Role = "";
            if (userRoles.Contains("Admin"))
            {
                Role = "Admin";
            }

            var result = await _patient.GetPatientList(cancellationToken, Role);
            return View(result);
        }

        // GET: Patient/Details/5
        public IActionResult Details(long id)
        {
            if (id == 0) { return NotFound(); }
            //var patient = _patient.GetAllDetail(id);
            //if (patient == null) { return NotFound(); }
            PatientModel model = new PatientModel();
            model.PatientID = id;
            return View(model);
        }

        // GET: Patient/Create
        public async Task<IActionResult> Create()
        {
            var doctors = _docterRepo.GetDoterList();
            ViewBag.DoctorList = new SelectList(doctors, "Dr_ID", "Dr_Name");

            var JRlist = await _docterRepo.getDropDownlist("jr");
            ViewBag.JRlist = new SelectList(JRlist, "Name", "Name");

            var SRlist = await _docterRepo.getDropDownlist("sr");
            ViewBag.SRlist = new SelectList(SRlist, "Name", "Name");

            return View();
        }

        // GET: Patient/Edit/5
        public async Task<IActionResult> Edit(long? id, CancellationToken cancellationToken)
        {
            if (id == null) { return NotFound(); }

            var doctors = _docterRepo.GetDoterList();
            ViewBag.DoctorList = new SelectList(doctors, "Dr_ID", "Dr_Name");


            var patient = await _patient.PatientDetail(id, cancellationToken);
            if (patient == null) { return NotFound(); }
            return View(patient);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPatient(string model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                // long patientid = 0;
                string msg;


                var _model = JsonConvert.DeserializeObject<List<PatientModel>>(model).FirstOrDefault();
                if (_model.PatientID > 0)
                {
                    await _patient.UpdatePatient(_model, cancellationToken);
                    msg = "Data updated successfully";
                }
                else
                {
                    _PatientId = _patient.AddPatient(_model, cancellationToken);
                    msg = "Data added successfully";
                }

                var doctors = _docterRepo.GetDoterList();
                ViewBag.DoctorList = new SelectList(doctors, "Dr_ID", "Dr_Name");


                return Json(msg);
            }
            return Json("something went wrong");
        }

        [HttpGet]
        public async Task<IActionResult> Patientdata(long PatientID, string ViewName, CancellationToken cancellationToken)
        {
            var doctors = _docterRepo.GetDoterList();
            ViewBag.DoctorList = new SelectList(doctors, "Dr_Name", "Dr_Name");

            var JRlist = await _docterRepo.getDropDownlist("jr");
            ViewBag.JRlist = new SelectList(JRlist, "Name", "Name");

            var SRlist = await _docterRepo.getDropDownlist("sr");
            ViewBag.SRlist = new SelectList(SRlist, "Name", "Name");


            PatientViewModel data = new PatientViewModel();
            if (PatientID > 0 && ViewName == "Edit")
            {
                data = await _patient.PatientDetail(PatientID, cancellationToken);
                return PartialView("AddPatient", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = await _patient.PatientDetail(PatientID, cancellationToken);
                return PartialView("_ViewPatient", data);
            }
            else if (PatientID > 0 && ViewName == "Print")
            {
                data = await _patient.PatientDetail(PatientID, cancellationToken);

                return GeneratePdf(data.PatientModel);
            }
            return PartialView("AddPatient", data);
        }

        //Add Investigation
        [HttpPost]
        public async Task<IActionResult> AddInvestigation(string data, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var doctors = _docterRepo.GetDoterList();
                ViewBag.DoctorList = new SelectList(doctors, "Dr_ID", "Dr_Name");


                var _model = JsonConvert.DeserializeObject<List<InvestigationModel>>(data).FirstOrDefault();
                if (_model == null) { return Json(null); }
                if (_model.Id > 0)
                {
                    await _patient.UpdateInvestigationData(_model, cancellationToken);
                    var investigationList = _context.Investigation.Where(a => a.PatientID == _model.PatientId).ToList();
                    string jsonData = JsonConvert.SerializeObject(investigationList);
                    return Json(jsonData);
                }
                else
                {
                    if (_PatientId > 0)
                    {
                        _InvestigationId = _patient.AddInvestigationData(_model, _PatientId);
                    }

                    var investigationList = _context.Investigation.Where(a => a.PatientID == _PatientId).ToList();
                    string jsonData = JsonConvert.SerializeObject(investigationList);
                    return Json(jsonData);
                }

            }
            return Json(null);
        }

        [HttpGet]
        public IActionResult AddInvestigation(long PatientID, string ViewName)
        {

            PatientViewModel data = new PatientViewModel();

            if (PatientID > 0 && ViewName == "Edit")
            {
                data = _patient.InvestigationDetail(PatientID);
                return PartialView("_EditInvestigation", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.InvestigationDetail(PatientID);
                return PartialView("_ViewInvestigation", data);
            }
            else if (PatientID > 0 && ViewName == "Print")
            {
                 data = _patient.InvestigationDetail(PatientID);

                return GeneratePdf(data.InvestigationList);
            }
            return PartialView("_EditInvestigation", data);

        }

        [HttpPost]
        public async Task<IActionResult> AddUpadateInvestigationDay(string data, CancellationToken cancellationToken)
        {

            PatientViewModel viewModel = new PatientViewModel();

            var _model = JsonConvert.DeserializeObject<List<InvestigationModel>>(data).FirstOrDefault();
            if (_model == null) { return Json(null); }
            if (_model.Id > 0)
            {
                await _patient.UpdateInvestigationData(_model, cancellationToken);
            }

            viewModel.InvestigationList = _context.Investigation.Where(a => a.PatientID == _model.PatientId).ToList();
            string jsonData = JsonConvert.SerializeObject(viewModel.InvestigationList);
            return Json(jsonData);

        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPicture(InvestigationImagesModel imageFiles, CancellationToken cancellationToken)
        {
            if (imageFiles != null)
            {
                string msg;
                if (imageFiles.PatientId > 0)
                {
                    await _patient.UpdateInvestigationImages(imageFiles, cancellationToken);
                    msg = "Data updated successfully";
                    return Json(msg);
                }

                if (_PatientId > 0 && _InvestigationId > 0)
                {

                    _patient.AddInvestigationImages(imageFiles, _InvestigationId, _PatientId);
                    msg = "Data added successfully";
                    return Json(msg);
                }
                return Json("something went wrong");
            }
            return Json("something went wrong");
        }

        [HttpGet]
        public IActionResult AddPicture(long PatientID, string ViewName)
        {
            PatientViewModel data = new PatientViewModel();
            PatientViewModel data2 = new PatientViewModel();

            if (PatientID > 0 && ViewName == "Edit")
            {
                data = _patient.PictureDetail(PatientID);
               
                if (data.InvestigationImages != null)
                {
                    return PartialView("_EditPicture", data);
                }
                else
                {
                    ViewBag.PatientId = PatientID;
                    return PartialView("_AddPicture", data2);

                }
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.PictureDetail(PatientID);
                return PartialView("_ViewPicture", data);
            }
            return PartialView("_AddPicture", data);
        }

        [HttpPost]
        public IActionResult Progress(string model)
        {
            if (ModelState.IsValid)
            {
                string msg;
                var _model = JsonConvert.DeserializeObject<List<ProgressModel>>(model).FirstOrDefault();
                if (_model.Id > 0)
                {
                    var result = _patient.UpdateProgress(_model);
                    msg = "Data updated successfully";
                    return Json(msg);
                }

                if (_PatientId > 0)
                {
                    _patient.AddProgress(_model, _PatientId);
                    msg = "Data added successfully";
                    return Json(msg);
                }
                return Json("something went wrong");
            }
            return Json("something went wrong");
        }

        [HttpGet]
        public IActionResult Progress(long PatientID, string ViewName)
        {
            PatientViewModel data = new PatientViewModel();

            if (PatientID > 0 && ViewName == "Edit")
            {
                data = _patient.ProgressDetail(PatientID);
                return PartialView("AddProgress", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.ProgressDetail(PatientID);
                return PartialView("_ViewProgress", data);
            }
            else if (PatientID > 0 && ViewName == "Print")
            {
                data = _patient.ProgressDetail(PatientID);

                return GeneratePdf(data.Progress);
            }
            return PartialView("AddProgress", data);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Diagnosis(string model)
        {
            if (ModelState.IsValid)
            {
                var _model = JsonConvert.DeserializeObject<List<PatientModel>>(model);
                string msg;
                if (_PatientId > 0)
                {
                    _patient.AddDiagnosis(_model[0], _PatientId);
                    msg = "Successfull";
                    return Json(msg);
                }
                if (_model[0].PatientID > 0)
                {
                    _patient.AddDiagnosis(_model[0], _model[0].PatientID);
                    msg = "Data updated successfully";
                    return Json(msg);
                }
                return Json("something went wrong");
            }
            return Json("something went wrong");
        }

        [HttpGet]
        public async Task<IActionResult> DiagnosisEdit(long PatientID, string ViewName, CancellationToken cancellationToken)
        {

            PatientViewModel data = new PatientViewModel();

            if (PatientID > 0 && ViewName == "Edit")
            {
                data = await _patient.PatientDetail(PatientID, cancellationToken);
                return PartialView("_EditDiagnosis", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data =await _patient.PatientDetail(PatientID, cancellationToken); ;
                return PartialView("_ViewDiagnosis", data);
            }
            return PartialView("_ViewDiagnosis", data);

        }

        [HttpPost]
        public async Task<IActionResult> CaseSheet(string model)
        {
            if (model != null)
            {
                string msg;
                var _model = JsonConvert.DeserializeObject<List<CaseSheetModel>>(model);
                if (_model[0].Id > 0)
                {
                    await _patient.UpdateCaseSheet(_model[0]);
                    msg = "Data updated successfully";
                    return Json(msg);
                }
                if (_PatientId > 0)
                {
                    _patient.AddCaseSheet(_model[0], _PatientId);
                    msg = "Data added successfully";
                    return Json(msg);
                }

                return Json("something went wrong");
            }
            return Json("something went wrong");

        }

        [HttpGet]
        public IActionResult CaseSheet(long PatientID, string ViewName)
        {
            PatientViewModel data = new PatientViewModel();

            if (PatientID > 0 && ViewName == "Edit")
            {
                data = _patient.CaseSheetDetail(PatientID);
                return PartialView("CaseSheet", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.CaseSheetDetail(PatientID);
                return PartialView("_ViewCaseSheet", data);
            }
            else if (PatientID > 0 && ViewName == "Print")
            {
                data = _patient.CaseSheetDetail(PatientID);

                return GeneratePdf(data.CaseSheet);
            }
            return PartialView("CaseSheet", data);

        }

        [HttpGet]
        public IActionResult Operation(long PatientID, string ViewName)
        {
            var doctors = _docterRepo.GetDoterList();
            ViewBag.DoctorList = new SelectList(doctors, "Dr_ID", "Dr_Name");
            PatientViewModel data = new PatientViewModel();
            if (PatientID > 0 && ViewName == "Edit")
            {
                data = _patient.OperationDetail(PatientID);
                return PartialView("OperationSheet", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.OperationDetail(PatientID);
                return PartialView("_ViewOperation", data);
            }
            else if (PatientID > 0 && ViewName == "Print")
            {
                data = _patient.OperationDetail(PatientID);

                return GeneratePdf(data.Operation);
            }
            return PartialView("OperationSheet", data);
        }

        [HttpPost]
        public async Task<IActionResult> OperationSheet(string model)
        {
            if (model != null)
            {
                string msg;
                var _model = JsonConvert.DeserializeObject<List<OperationModel>>(model);
                if (_model[0].Id > 0)
                {
                    await _patient.UpdateOperationSheet(_model[0]);
                    msg = "Data updated successfully";
                    return Json(msg);
                }
                if (_PatientId > 0)
                {
                    _patient.AddOperationSheet(_model[0], _PatientId);
                    msg = "Data added successfully";
                    return Json(msg);
                }

                return Json("something went wrong");
            }
            return Json("something went wrong");
        }

        [HttpPost]
        public IActionResult DischargePost(string model)
        {
            if (ModelState.IsValid)
            {
                string msg;
                PatientViewModel modl = new PatientViewModel();
                var _model = JsonConvert.DeserializeObject<List<DischargeModel>>(model).FirstOrDefault();
                if (_model.Id > 0)
                {
                    _patient.UpdateDischarge(_model);
                    msg = "Data updated successfully";
                    return Json(msg);
                }
                if (_model.PatientID > 0)
                {
                    _patient.AddDischarge(_model, _model.PatientID);
                    msg = "Data added successfully";
                    return Json(msg);
                }
                return Json("something went wrong");
            }
            return Json("something went wrong");
        }

        [HttpGet]
        public async Task<IActionResult> Discharge(long PatientID, string ViewName)
        {
            var JRlist = await _docterRepo.getDropDownlist("jr");
            ViewBag.JRlist = new SelectList(JRlist, "Name", "Name");

            var SRlist = await _docterRepo.getDropDownlist("sr");
            ViewBag.SRlist = new SelectList(SRlist, "Name", "Name");

            PatientViewModel data = new PatientViewModel();
            if (PatientID > 0)
            {

                data = _patient.DischargeDetail(PatientID);
                return PartialView("Discharge", data);
            }
            if ( _PatientId > 0)
            {

                data = _patient.DischargeDetail(_PatientId);
                return PartialView("Discharge", data);
            }

            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.DischargeDetail(PatientID);
                return PartialView("_ViewDischarge", data);
            }
            else if (PatientID > 0 && ViewName == "Print")
            {
                data = _patient.DischargeDetail(PatientID);

                return GeneratePdf(data.Discharge);
            }
            return PartialView("Discharge", data);
        }

        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(long? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patient = await _patient.PatientDetail(id, cancellationToken);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, CancellationToken cancellationToken)
        {
            var result = await _patient.DeletePatient(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult SaveModels(string data)
        {
            try
            {
                List<InvestigationModel> Tabledata = JsonConvert.DeserializeObject<List<InvestigationModel>>(data);
                var _tabledata = _patient.addTempData(Tabledata);
                string jsonData = JsonConvert.SerializeObject(_tabledata);
                return Json(jsonData);
            }
            catch (System.Exception e)
            {
                return Json(false, e);
            }
        }

        [HttpGet]
        public IActionResult GetSubCategoryList(string query = null, int CategoryId = 0)
        {
            if (CategoryId > 0)
            {
                _catId = CategoryId;
            }
            if (!string.IsNullOrEmpty(query))
            {
                var FilterdCategory = _context.SubCategory
                .Where(d => d.SubCategoryTitle.Contains(query) && d.CategoryId == _catId)
                .Select(d => new { id = d.Id, text = d.SubCategoryTitle })
                .ToList();
                return Json(FilterdCategory);
            }
            else
            {
                var AllSubCategory = _context.SubCategory
                      .Where(a => a.CategoryId == _catId)
                      .Select(d => new { id = d.Id, text = d.SubCategoryTitle })
                      .ToList();
                return Json(AllSubCategory);
            }


        }

        public IActionResult DaysEdit(int investigationId = 0)
        {

            if (investigationId > 0)
            {
                var FilterdCategory = _context.Investigation.Where(a => a.Id == investigationId);
                return Json(FilterdCategory);
            }
            return Json("");

        }

        public IActionResult DeleteInvestigation(int InvestigationId = 0, long PatientID = 0)
        {
            if (InvestigationId > 0)
            {
                var result = _patient.RemoveInvestigation(InvestigationId);
                if (result)
                {
                    var investigationList = _context.Investigation.Where(a => a.PatientID == PatientID).ToList();
                    string jsonData = JsonConvert.SerializeObject(investigationList);
                    return Json(jsonData);
                }
            }
            return Json("");
        }


        [HttpGet("/Patient/GeneratePdf")]
        public IActionResult GeneratePdf<TModel>(TModel model)
        {
            // Create a new document
            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);


            List<string> propertiesToIgnore = new List<string>
            { "PatientID","Address_ID","Dr_ID","OtherO","OtherT","OtherTh","Id",
                "CreatedBy","UpdateBy","CreatedOn","UpdatedOn","DateOfBirth",
                "Address","Email","Status","IsActive","Title","SubCategoryTitle",
                "subCategory","InvestigationModel","InvestigationImagesModel" 
            };

            // Add data to the PDF document
            doc.Open();
            // Iterate through the properties of the model and add them to the PDF
            PropertyInfo[] properties = typeof(TModel).GetProperties();
            doc.AddTitle("heading");
            doc.AddHeader("H1", "this is header");
            foreach (PropertyInfo property in properties)
            {
                if (propertiesToIgnore.Contains(property.Name))
                {
                    continue; // Skip this property
                }
                string propertyName = property.Name;
                object propertyValue = property.GetValue(model);

                // Add the property name and value to the PDF as paragraphs
                doc.Add(new Paragraph($"{propertyName}: {propertyValue}"));
                
                

            }
            doc.Close(); // Close the document

            // Create a copy of the MemoryStream
            MemoryStream copyStream = new MemoryStream(memoryStream.ToArray());

            // Set the content type and file name for the response
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.Headers.Add("content-disposition", "inline;filename=mydocument.pdf");

            // Write the PDF content to the response
            copyStream.CopyTo(HttpContext.Response.Body);
            return new EmptyResult();
        }

        
        [HttpGet]
        public IActionResult Outcome(long PatientID, string ViewName)
        {
            PatientViewModel data = new PatientViewModel();

            if (PatientID > 0 && ViewName == "Edit")
            {
                data = _patient.CaseSheetDetail(PatientID);
                return PartialView("_Outcome", data);
            }
            else if (PatientID > 0 && ViewName == "Detail")
            {
                data = _patient.CaseSheetDetail(PatientID);
                return PartialView("_ViewOutcome", data);
            }
           
            return PartialView("Outcome", data);

        }

        [HttpPost]
        public async Task<IActionResult> Outcome(string model)
        {
            if (model != null)
            {
                string msg;
                var _model = JsonConvert.DeserializeObject<List<CaseSheetModel>>(model);
                if (_model[0].Id > 0)
                {
                    await _patient.UpdateCaseSheet(_model[0]);
                    msg = "Data updated successfully";
                    return Json(msg);
                }
                if (_PatientId > 0)
                {
                    _patient.AddCaseSheet(_model[0], _PatientId);
                    msg = "Data added successfully";
                    return Json(msg);
                }

                return Json("something went wrong");
            }
            return Json("something went wrong");

        }

    }
}
