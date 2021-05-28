using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Models
{
    public class Prescription_Medicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int? Dose { get; set; }
        public String Details { get; set; }

        public Prescription Prescription { get; set; }
        public Medicament Medicament { get; set; }
    }
}
