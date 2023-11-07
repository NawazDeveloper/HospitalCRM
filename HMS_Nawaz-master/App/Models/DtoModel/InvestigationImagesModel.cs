using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace App.Models.DtoModel
{
    public class InvestigationImagesModel
    {
        public long Id { get; set; }
        public long InvestigationID { get; set; }
        public int PatientId { get; set; }
        public IFormFile CBC_img { get; set; }
        public IFormFile RFT_img { get; set; }
        public IFormFile BloodSugar_img { get; set; }
        public IFormFile SerumElectrolytes_img { get; set; }
        public IFormFile LipidProfile_img { get; set; }
        public IFormFile USG_img { get; set; }
        public IFormFile CECT_img { get; set; }
        public IFormFile MRI_img { get; set; }
        public IFormFile FNAC_img { get; set; }
        public IFormFile TrucutBiopsy_img { get; set; }
        public IFormFile ReceptorStatus_img { get; set; }
        public IFormFile MRCP_img { get; set; }
        public IFormFile ERCP_img { get; set; }
        public IFormFile EndoscopyUpperGI_img { get; set; }
        public IFormFile EndoscopyLowerGI_img { get; set; }
        public IFormFile PETCT_img { get; set; }
        public IFormFile TumorMarkers_img { get; set; }
        public IFormFile SONOMMOGRAPHY_img { get; set; }
        public IFormFile LFT_img { get; set; }
        public IFormFile TSPAG_img { get; set; }
        public IFormFile TFT_img { get; set; }
        public IFormFile IVP_img { get; set; }
        public IFormFile MCU_img { get; set; }
        public IFormFile RGU_img { get; set; }
        public string Name { get; set; }
        public string ExtTtype { get; set; }
        public string Msg { get; set; }
    }
}
