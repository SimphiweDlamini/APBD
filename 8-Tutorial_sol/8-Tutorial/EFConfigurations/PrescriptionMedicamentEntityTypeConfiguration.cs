using _8_Tutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.EFConfigurations
{
    public class PrescriptionMedicamentEntityTypeConfiguration:IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> builder) {
            builder.HasKey(e => new { e.IdMedicament,e.IdPrescription});

            builder.Property(e => e.Details).IsRequired().HasMaxLength(100);
           // builder.Property(e => e.Dose).IsOptional();

            builder.HasOne(e => e.Medicament).WithMany(m => m.Prescription_Medicaments).HasForeignKey(f => f.IdMedicament);
            builder.HasOne(e => e.Prescription).WithMany(m => m.Prescription_Medicaments).HasForeignKey(f => f.IdPrescription);

            builder.HasData(new Prescription_Medicament
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 2,
                Details = "Take 2 after every meal"
            });
        }
    }
}
