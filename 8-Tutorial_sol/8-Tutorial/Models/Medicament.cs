using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Models
{
    public class Medicament
    {
        
        public int IdMedicament { get; set; }
        
        public String Name { get; set; }
        
        public String Description { get; set; }
        
        public String Type { get; set; }

        public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = new HashSet<Prescription_Medicament>();
    }
}
