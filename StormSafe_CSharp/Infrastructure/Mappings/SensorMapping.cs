using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormSafe.Infrastructure.Persistence;

namespace StormSafe.Infrastructure.Mappings
{
    public class SensorMapping : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("TBL_SENSORES");

            builder.HasKey(s => s.SensorId);

            builder.Property(s => s.SensorId)
                   .HasColumnName("id_sensor");

            builder.Property(s => s.Tipo)
                   .HasColumnName("tipo")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(s => s.RioId)
                   .HasColumnName("id_rio");
        }
    }
}
