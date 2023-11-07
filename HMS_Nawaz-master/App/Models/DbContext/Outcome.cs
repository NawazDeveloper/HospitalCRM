using App.Models.DtoModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models.DbContext
{
    public class Outcome
    {
        [Key]
        public int Id { get; set; }
        public DateTime DischargeDate { get; set; }
        public OutcomeType  outcomeType { get; set; }
    }

    public enum OutcomeType
    {
        Discharged,
        LAMA,
        Absconded,
        Transferred,
        Death
    }
}
