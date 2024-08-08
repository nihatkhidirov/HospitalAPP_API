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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(g => g.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(g => g.Limit).IsRequired(true).HasMaxLength(50);
            builder.Property(g => g.CreatedDate).IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Property(g => g.UpdateDate).IsRequired(true).HasDefaultValueSql("getdate()");


        }
    }
}
