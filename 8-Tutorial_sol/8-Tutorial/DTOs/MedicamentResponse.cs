using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.DTOs
{
    public class MedicamentResponse
    {
        public String MedicamentName { get; set; }
        public String DescriptionMedicaments { get; set; }
        public String MedicamentType { get; set; }
        public int? Dose { get; set; }
        public String Details { get; set; }
    }
}
