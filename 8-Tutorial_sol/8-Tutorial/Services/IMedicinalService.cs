using _8_Tutorial.DTOs;
using _8_Tutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Services
{
    public interface IMedicinalService
    {
        public Task<IEnumerable<Doctor>> GetDoctors();
        public Task<Doctor> GetDoctor(int docid);
        public Task AddDoctor(DoctorRequest doctor);
        public Task<bool> DoctorExist(DoctorRequest doctor);
        public Task<bool> DoctorExist(int docid);

        public Task UpdateDoctor(DoctorRequest doctor);

        public Task DeleteDoctor(int docid);

        public Task<bool> PrescriptionExist(int prescriptionid);

        public Task<PrescriptionResponse> getPrescription(int prescriptionid);
    }
}
