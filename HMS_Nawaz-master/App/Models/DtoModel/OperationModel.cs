using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.DtoModel
{
    public class OperationModel
    {
        public long Id { get; set; }
        public long PatientID { get; set; }
        public string Indication { get; set; }
        public string Anaesthetist { get; set; }
        public string OpertingSurgeon { get; set; }
        public string Dr_ID { get; set; }
        public string Position { get; set; }
        public string Findings { get; set; }
        public string Procedure { get; set; }
        public string Duration { get; set; }
        public string StepsOfOperation { get; set; }
        public string Antibiotics { get; set; }
        public string Complications { get; set; }
        public string Drains { get; set; }
        public string Closure { get; set; }
        public string SpecimensSentFor { get; set; }
        public string PostOperativeInstructions { get; set; }
        public string Date { get; set; }
    }
}
