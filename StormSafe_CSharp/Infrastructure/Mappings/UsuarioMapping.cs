using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormSafe.Infrastructure.Persistence;

namespace StormSafe.Infrastructure.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TBL_USUARIOS");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.IdUsuario)
                   .HasColumnName("id_usuario");

            builder.Property(u => u.Nome)
                   .HasColumnName("nome")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(u => u.Senha)
                   .HasColumnName("senha")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.TipoUsuario)
                   .HasColumnName("role")
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();
        }
    }
}
