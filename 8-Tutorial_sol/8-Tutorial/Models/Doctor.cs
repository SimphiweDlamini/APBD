using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();

    }
}
