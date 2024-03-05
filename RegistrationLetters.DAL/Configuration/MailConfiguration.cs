using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationLetters.Domain.Entities;

namespace RegistrationLetters.DAL.Configuration
{
    /// <summary>
    /// This class is used to configure the properties of the MailEntity class.
    /// </summary>
    public class MailConfiguration : IEntityTypeConfiguration<MailEntity>
    {
        public void Configure(EntityTypeBuilder<MailEntity> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(300).IsRequired().HasConversion<string>();
            builder.Property(x => x.Content).HasMaxLength(5000).IsRequired().HasConversion<string>();
            builder.Property(x => x.Date).IsRequired().HasConversion<string>();

            builder.Property(x => x.AddresseeId).HasMaxLength(100).IsRequired().HasConversion<int>();
            builder.Property(x => x.SenderId).HasMaxLength(100).IsRequired().HasConversion<int>();
        }
    }
}
