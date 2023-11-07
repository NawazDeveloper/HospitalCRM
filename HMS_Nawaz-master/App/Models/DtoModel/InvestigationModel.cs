using App.DtoModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.DtoModel
{
    public class InvestigationModel
    {

        public long Id { get; set; }
        public long PatientId { get; set; }
        public string CBC { get; set; }
        public string RFT { get; set; }
        public string BloodSugar { get; set; }
        public string SerumElectrolytes { get; set; }
        public string LipidProfile { get; set; }
        public string USG { get; set; }
        public string CECT { get; set; }
        public string MRI { get; set; }
        public string FNAC { get; set; }
        public string TrucutBiopsy { get; set; }
        public string ReceptorStatus { get; set; }
        public string MRCP { get; set; }
        public string ERCP { get; set; }
        public string EndoscopyUpperGI { get; set; }
        public string EndoscopyLowerGI { get; set; }
        public string PETCT { get; set; }
        public string TumorMarkers { get; set; }
        public string SONOMMOGRAPHY { get; set; }
        public string LFT { get; set; }
        public string TSPAG { get; set; }
        public string TFT { get; set; }
        public string OtherO { get; set; }
        public string OtherT { get; set; }
        public string OtherTh { get; set; }
        [DataType(DataType.Date)]
        public string Day { get; set; }
        public string IVP { get; set; }
        public string MCU { get; set; }
        public string RGU { get; set; }

    }
}
