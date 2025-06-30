using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using otardavitashvili.Models;

namespace otardavitashvili.DB
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Primary Key
            builder.HasKey(p => p.PersonId);

            // Properties
            builder.Property(p => p.PersonName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.PersonLastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .IsRequired();

            // Relationship
            builder.HasMany(p => p.Cars)
                .WithOne(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
