using ContactManagement.Core.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagement.Infrastructure.Data.Config;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts").HasKey(k => k.Id);
        builder.Property(p => p.FirstName)
            .HasMaxLength(Contact.MaxNameLength).IsRequired();
        builder.Property(p => p.LastName)
            .HasMaxLength(Contact.MaxNameLength).IsRequired();
        builder.Property(p => p.Age).IsRequired();

        builder.OwnsOne(p => p.Address, p =>
        {
            p.Property(pp => pp.Line1)
                .HasMaxLength(DataSchemaConstants.DefaultAddressLength).IsRequired();
            p.Property(pp => pp.Line2)
                .HasMaxLength(DataSchemaConstants.DefaultAddressLength);
            p.Property(pp => pp.City)
                .HasMaxLength(DataSchemaConstants.DefaultAddressLength).IsRequired();
            p.Property(pp => pp.State)
                .HasMaxLength(2).IsRequired();
            p.Property(pp => pp.Zip)
                .HasMaxLength(10).IsRequired();
        });

        builder.OwnsOne(p => p.PhoneNumber, p =>
        {
            p.Property(pp => pp.PhoneNumber)
                .HasMaxLength(10).IsRequired();
            p.Property(pp => pp.Extension)
                .HasMaxLength(5);
        });
    }
}