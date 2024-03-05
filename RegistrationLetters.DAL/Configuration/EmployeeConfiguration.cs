using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationLetters.Domain.Entities;

namespace RegistrationLetters.DAL.Configuration
{
    /// <summary>
    /// This class is used to configure the properties of the MailEntity class.
    /// </summary>
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired().HasConversion<string>();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired().HasConversion<string>();
            builder.Property(x => x.JobPosition).HasMaxLength(100).IsRequired().HasConversion<string>();
        }
    }
}
