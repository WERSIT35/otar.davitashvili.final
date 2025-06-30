using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using otardavitashvili.Models;

namespace otardavitashvili.DB
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(id => id.CarId);

            builder.Property(x => x.Model)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(x => x.Speed)
                .IsRequired();

            builder.Property(x => x.ReleaseDate)
                .IsRequired();

            builder.HasOne(t => t.Person)
                .WithMany(s => s.Cars)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
