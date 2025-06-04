using FCG.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FCG.Api.Infraestrutura.Data
{
    public class DBContexto : DbContext
    {

        public DBContexto(DbContextOptions<DBContexto> options)
          : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

            builder.HasAnnotation("Relational:Schema", "fcg");
            builder.HasDefaultSchema("fcg");

            base.OnModelCreating(builder);


            builder.Entity<JogoUsuario>()
                .HasKey(ju => new { ju.UsuarioId, ju.JogoId });

            builder.Entity<JogoUsuario>()
                .HasOne(ju => ju.Usuario)
                .WithMany(u => u.Jogo)
                .HasForeignKey(ju => ju.UsuarioId);

            builder.Entity<JogoUsuario>()
                .HasOne(ju => ju.Jogo)
                .WithMany(j => j.Usuario)
                .HasForeignKey(ju => ju.JogoId);


            builder.Entity<Jogo>()
             .HasKey(j => new { j.Id});

            builder.Entity<Usuario>()
             .HasKey(j => new { j.Id });
        }

    }

}
