using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.DTOs
{
    public class PrescriptionResponse
    {
        public String PatientName { get; set; }
        public String PatientSurname { get; set; }
        public String DoctorName { get; set; }
        public String DoctorSurname { get; set; }
        public IEnumerable<MedicamentResponse> Medicaments{get;set;}

    }
}
