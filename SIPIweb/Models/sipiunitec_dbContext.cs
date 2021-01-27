using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SIPIweb.Models
{
    public partial class sipiunitec_dbContext : DbContext
    {
        public sipiunitec_dbContext()
        {
        }

        public sipiunitec_dbContext(DbContextOptions<sipiunitec_dbContext> options)
            : base(options)
        {
        }

        // ***** Geografia ***** //
        public virtual DbSet<tbl_geografiaCiudad> tbl_geografiaCiudads { get; set; }
        public virtual DbSet<tbl_geografiaEstado> tbl_geografiaEstados { get; set; }
        public virtual DbSet<tbl_geografiaPai> tbl_geografiaPais { get; set; }

        // ***** Usuarios ***** //
        public virtual DbSet<tbl_usuario> my_usuarios { get; set; }
        public virtual DbSet<tbl_usuario_tmp> my_usuario_tmps { get; set; }
        public virtual DbSet<tbl_usuarioEstudiante> my_usuarioEstudiante { get; set; }
        public virtual DbSet<tbl_usuarioPersona> my_usuarioPersonas { get; set; }

        public virtual DbSet<tbl_usuarioAsignaRol> tbl_usuarioAsignaRols { get; set; }
        
        public virtual DbSet<tbl_usuarioRole> tbl_usuarioRoles { get; set; }
        public virtual DbSet<tbl_usuarioTipo> tbl_usuarioTipos { get; set; }

        public virtual DbSet<tbl_historico> tbl_historicos { get; set; }
        public virtual DbSet<tbl_informacion> tbl_informacions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local)\\dcserver;Initial Catalog=sipiunitec_db;Integrated Security=True;Connect Timeout=45");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<tbl_geografiaCiudad>(entity =>
            {
                entity.HasOne(d => d.id_estadoNavigation)
                    .WithMany(p => p.tbl_geografiaCiudads)
                    .HasForeignKey(d => d.id_estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_geografiaCiudad_tbl_geografiaEstado");
            });

            modelBuilder.Entity<tbl_geografiaEstado>(entity =>
            {
                entity.HasOne(d => d.id_paisNavigation)
                    .WithMany(p => p.tbl_geografiaEstados)
                    .HasForeignKey(d => d.id_pais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_geografiaEstado_tbl_geografiaPais");
            });

            modelBuilder.Entity<tbl_geografiaPai>(entity =>
            {
                entity.HasKey(e => e.id_pais)
                    .HasName("PK_tbl_pais");
            });

            modelBuilder.Entity<tbl_informacion>(entity =>
            {
                entity.HasKey(e => e.id_informacion)
                    .HasName("PK_informacion");

                entity.HasOne(d => d.id_usuarioNavigation)
                    .WithMany(p => p.tbl_informacions)
                    .HasForeignKey(d => d.id_usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_informacion_usuario");
            });

            modelBuilder.Entity<tbl_usuario>(entity =>
            {
                entity.HasKey(e => e.id_usuario)
                    .HasName("PK_usuario");

                entity.HasOne(d => d.id_usuarioTipoNavigation)
                    .WithMany(p => p.tbl_usuarios)
                    .HasForeignKey(d => d.id_usuarioTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_usuario_tipo");
            });

            modelBuilder.Entity<tbl_usuarioAsignaRol>(entity =>
            {
                entity.HasKey(e => new { e.id_persona, e.id_usuarioRoles });

                entity.HasOne(d => d.id_personaNavigation)
                    .WithMany(p => p.tbl_usuarioAsignaRols)
                    .HasForeignKey(d => d.id_persona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_usuarioAsignaRol_tbl_usuarioPersona");

                entity.HasOne(d => d.id_usuarioRolesNavigation)
                    .WithMany(p => p.tbl_usuarioAsignaRols)
                    .HasForeignKey(d => d.id_usuarioRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_usuarioAsignaRol_tbl_usuarioRoles");
            });

            modelBuilder.Entity<tbl_usuarioEstudiante>(entity =>
            {
                entity.Property(e => e.id_usuarioEstudiante).ValueGeneratedNever();

                entity.HasOne(d => d.id_usuarioEstudianteNavigation)
                    .WithOne(p => p.tbl_usuarioEstudiante)
                    .HasForeignKey<tbl_usuarioEstudiante>(d => d.id_usuarioEstudiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_usuarioEstudiante_tbl_usuario");
            });

            modelBuilder.Entity<tbl_usuarioPersona>(entity =>
            {
                entity.HasKey(e => e.id_persona)
                    .HasName("PK_usuario_persona_tbl");

                entity.Property(e => e.id_persona).ValueGeneratedNever();

                entity.Property(e => e.persona_nombreCompleto).HasComputedColumnSql("(([persona_apellidos]+', ')+[persona_nombres])", false);

                entity.HasOne(d => d.id_ciudad_nacimientoNavigation)
                    .WithMany(p => p.tbl_usuarioPersonaid_ciudad_nacimientoNavigations)
                    .HasForeignKey(d => d.id_ciudad_nacimiento)
                    .HasConstraintName("FK_tbl_usuarioPersona_tbl_geografiaCiudad");

                entity.HasOne(d => d.id_ciudad_ubicacionNavigation)
                    .WithMany(p => p.tbl_usuarioPersonaid_ciudad_ubicacionNavigations)
                    .HasForeignKey(d => d.id_ciudad_ubicacion)
                    .HasConstraintName("FK_tbl_usuarioPersona_tbl_geografiaCiudad1");

                entity.HasOne(d => d.id_personaNavigation)
                    .WithOne(p => p.tbl_usuarioPersona)
                    .HasForeignKey<tbl_usuarioPersona>(d => d.id_persona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_persona_tbl_usuario_tbl");
            });

            modelBuilder.Entity<tbl_usuarioRole>(entity =>
            {
                entity.HasKey(e => e.id_usuarioRoles)
                    .HasName("PK_usuariorol_tbl");
            });

            modelBuilder.Entity<tbl_usuarioTipo>(entity =>
            {
                entity.HasKey(e => e.id_usuarioTipo)
                    .HasName("PK_tipo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
