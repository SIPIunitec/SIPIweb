using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SIPIweb.Models
{
    public partial class SIPIContext : DbContext
    {
        public SIPIContext()
        {
        }

        public SIPIContext(DbContextOptions<SIPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<informacion> informacions { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<informacion>(entity =>
            {
                entity.HasOne(d => d.id_usuarioNavigation)
                    .WithMany(p => p.informacions)
                    .HasForeignKey(d => d.id_usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_informacion_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
