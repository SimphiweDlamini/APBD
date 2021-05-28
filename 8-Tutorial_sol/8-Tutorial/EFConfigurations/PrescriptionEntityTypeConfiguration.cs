using _8_Tutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.EFConfigurations
{
    public class PrescriptionEntityTypeConfiguration:IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder) {
            builder.HasKey(e => e.IdPrescription);
            builder.Property(e => e.IdPrescription).ValueGeneratedOnAdd();

            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.DueDate).IsRequired();

            builder.HasOne(e => e.Patient).WithMany(p => p.Prescriptions).HasForeignKey(pa => pa.IdPatient);
            builder.HasOne(e => e.Doctor).WithMany(p => p.Prescriptions).HasForeignKey(pa => pa.IdDoctor);

            builder.HasData(new Prescription { 
              IdPrescription=1,
              Date= DateTime.Now,
              DueDate = DateTime.Now ,
              IdDoctor = 1,
              IdPatient = 1
            });
        }
    }
}
