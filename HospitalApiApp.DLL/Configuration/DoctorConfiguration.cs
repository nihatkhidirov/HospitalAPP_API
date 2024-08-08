using HospitalApiApp.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApiApp.DLL.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(g => g.Name).IsRequired(true).HasMaxLength(20);
            builder
                .HasOne(s => s.Department)
                .WithMany(g => g.Doctors)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(g => g.CreatedDate).IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Property(g => g.UpdateDate).IsRequired(true).HasDefaultValueSql("getdate()");
        }
    }
}
