using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e
                .ToTable("TB_USUARIO")
                .HasKey(k => k.Id);

            e
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            e
                .Property(p => p.Id)
                .HasColumnName("IDT_USUARIO");

            // e
            //     .Property(p => p.Ativo)
            //     .HasColumnName("ATV_USUARIO")
            //     .IsRequired();

            // e
            //     .Property(p => p.Nome)
            //     .HasColumnName("NOM_USUARIO")
            //     .IsRequired();
        });
    }
}