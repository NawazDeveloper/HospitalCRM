﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using App.Models.EntityModels;
using Microsoft.AspNetCore.Http;

namespace App.Models.DbContext
{
    public class InvestigationImages
    {
        [Key]
        public long Id { get; set; }
        public long InvestigationID { get; set; }
        [ForeignKey(nameof(InvestigationID))]
        public Investigation Investigation { get; set; }
        public int PatientId { get; set; }
        public string CBC_img { get; set; }
        public string RFT_img { get; set; }
        public string BloodSugar_img { get; set; }
        public string SerumElectrolytes_img { get; set; }
        public string LipidProfile_img { get; set; }
        public string USG_img { get; set; }
        public string CECT_img { get; set; }
        public string MRI_img { get; set; }
        public string FNAC_img { get; set; }
        public string TrucutBiopsy_img { get; set; }
        public string ReceptorStatus_img { get; set; }
        public string MRCP_img { get; set; }
        public string ERCP_img { get; set; }
        public string EndoscopyUpperGI_img { get; set; }
        public string EndoscopyLowerGI_img { get; set; }
        public string PETCT_img { get; set; }
        public string TumorMarkers_img { get; set; }
        public string SONOMMOGRAPHY_img { get; set; }
        public string LFT_img { get; set; }
        public string TSPAG_img { get; set; }
        public string TFT_img { get; set; }
        public string Name { get; set; } 
        public string ExtTtype { get; set; } 
        public string Msg { get; set; }
        public string IVP_img { get; set; }
        public string MCU_img { get; set; }
        public string RGU_img { get; set; }
        public string OtherO { get; set; }
        public string OtherT { get; set; }
        public string OtherTh { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }


    }
}
