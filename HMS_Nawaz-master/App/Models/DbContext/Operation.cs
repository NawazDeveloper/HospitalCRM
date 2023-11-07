using App.Models.EntityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.DbContext
{
    public class Operation
    {
        [Key]
        public long Id { get; set; }
        public long PatientID { get; set; }
        [ForeignKey(nameof(PatientID))]
        public Patient Patient { get; set; }
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
        public string PerhapsImage { get; set; }
        public string SpecimensSentFor { get; set; }
        public string PostOperativeInstructions { get; set; }
        [DataType(DataType.Date)]
        public string Date { get; set; }
    }
}
