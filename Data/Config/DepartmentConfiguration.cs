using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPIDemo.Models;

namespace WebAPIDemo.Data.Config
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired(true);
            builder.ToTable("Departments");
            builder.HasData(LoadingData());
        }
        public static Department[] LoadingData() {
            return new Department[] {
                new Department {Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "Sales" }
            };
        }
    }
}
