using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormSafe.Infrastructure.Persistence;

namespace StormSafe.Infrastructure.Mappings
{
    public class RioMapping : IEntityTypeConfiguration<Rio>
    {
        public void Configure(EntityTypeBuilder<Rio> builder)
        {

            builder.ToTable("TBL_RIOS");

            builder.HasKey(r => r.RioId);

            builder.Property(r => r.RioId)
                   .HasColumnName("id_rio");

            builder.Property(r => r.Nome)
                   .HasColumnName("nome")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasMany(r => r.Sensores)
                   .WithOne(s => s.Rio)
                   .HasForeignKey(s => s.RioId);
        }
    }
}

