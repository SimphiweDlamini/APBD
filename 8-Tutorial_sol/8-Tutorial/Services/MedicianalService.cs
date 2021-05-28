using _8_Tutorial.DTOs;
using _8_Tutorial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Services
{
    public class MedicianalService:IMedicinalService
    {
        private DoctorDBContext _doctorDBContext;

        public MedicianalService(DoctorDBContext doctorDBContext)
        {
            _doctorDBContext = doctorDBContext;
        }

        public async Task AddDoctor(DoctorRequest doctor)
        {
            var adddoc = new Doctor { FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email };
            await _doctorDBContext.Doctors.AddAsync(adddoc);
            await _doctorDBContext.SaveChangesAsync();
        }

        public async Task DeleteDoctor(int docid)
        {
            var del = await _doctorDBContext.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == docid);
             _doctorDBContext.Doctors.Remove(del);
            await _doctorDBContext.SaveChangesAsync();
        }

        public async Task<bool> DoctorExist(int docid)
        {
            return await _doctorDBContext.Doctors.AnyAsync(d => d.IdDoctor == docid);
            
        }
        public async Task<bool> DoctorExist(DoctorRequest doctor)
        {
            return await _doctorDBContext.Doctors.AnyAsync(d => d.IdDoctor == doctor.IdDoctor);

        }

        public async Task<Doctor> GetDoctor(int docid)
        {
            var result = await _doctorDBContext.Doctors.Where(e => e.IdDoctor == docid).FirstAsync();
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            var result = await _doctorDBContext.Doctors.ToListAsync();
            return result;
        }

        public async Task<PrescriptionResponse> getPrescription(int prescriptionid)
        {
            
            var allpres = await _doctorDBContext.Prescriptions.Include(p => p.Prescription_Medicaments).ThenInclude(pm => pm.Medicament).Include(p => p.Patient)
                .Include(p => p.Doctor).Where(p=>p.IdPrescription==prescriptionid).FirstAsync();
            var respMedicaments = allpres.Prescription_Medicaments.Select(e => new MedicamentResponse
            {
                MedicamentName = e.Medicament.Name,
                DescriptionMedicaments = e.Medicament.Description,
                MedicamentType = e.Medicament.Type,
                Dose = e.Dose,
                Details = e.Details
            });
            var result = new PrescriptionResponse {
                PatientName = allpres.Patient.FirstName,
                PatientSurname = allpres.Patient.LastName,
                DoctorName = allpres.Doctor.FirstName,
                DoctorSurname = allpres.Doctor.LastName,
                Medicaments = respMedicaments
            };

            return result;

        }

        public async Task<bool> PrescriptionExist(int prescriptionid)
        {
            return await _doctorDBContext.Prescriptions.AnyAsync(p => p.IdPrescription == prescriptionid);
        }

        public async Task UpdateDoctor(DoctorRequest doctor)
        {
            var thedoctor = await _doctorDBContext.Doctors.FirstAsync(d => d.IdDoctor == doctor.IdDoctor);
            thedoctor.FirstName = doctor.FirstName;
            thedoctor.LastName = doctor.LastName;
            thedoctor.Email = doctor.Email;
             _doctorDBContext.Update(thedoctor);
            await _doctorDBContext.SaveChangesAsync();
        }
    }
}
