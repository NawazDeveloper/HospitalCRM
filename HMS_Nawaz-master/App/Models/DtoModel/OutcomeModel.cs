using System;

namespace App.Models.DtoModel
{
    public class OutcomeModel
    {
        public class OutcomeForm
        {
            public DateTime DischargeDate { get; set; }
            public OutcomeType Outcome { get; set; }
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
}
