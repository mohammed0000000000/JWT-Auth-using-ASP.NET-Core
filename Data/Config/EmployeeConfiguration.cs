using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPIDemo.Models;

namespace WebAPIDemo.Data.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder) {
            builder.HasKey(emp => emp.Id);
            builder.Property(emp => emp.Id).ValueGeneratedOnAdd();
            builder.Property(emp => emp.Name).HasColumnType("VARCHAR").HasMaxLength(50).IsRequired(true);
            builder.Property(emp => emp.Address).HasColumnType("VARCHAR").HasMaxLength(50);
            builder.Property(emp => emp.Salary).HasColumnType("INT").IsRequired(true);
            builder.Property(emp => emp.Age).HasColumnType("INT").IsRequired(true);
            builder.HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DeptId);
            builder.ToTable("Employees");
        }
    }
}
