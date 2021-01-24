using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SIPIweb.Models
{
    public partial class Rapsodi_dbContext : DbContext
    {
        public Rapsodi_dbContext()
        {
        }

        public Rapsodi_dbContext(DbContextOptions<Rapsodi_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<res_geo_city_admin> res_geo_city_admins { get; set; }
        public virtual DbSet<res_geo_country_admin> res_geo_country_admins { get; set; }
        public virtual DbSet<res_geo_state_admin> res_geo_state_admins { get; set; }
        public virtual DbSet<res_resource> res_resources { get; set; }
        public virtual DbSet<res_type_admin> res_type_admins { get; set; }
        public virtual DbSet<usr_business> usr_businesses { get; set; }
        public virtual DbSet<usr_person> usr_people { get; set; }
        public virtual DbSet<usr_resource_usage> usr_resource_usages { get; set; }
        public virtual DbSet<usr_resource_usage_user> usr_resource_usage_users { get; set; }
        public virtual DbSet<usr_resource_user> usr_resource_users { get; set; }
        public virtual DbSet<usr_type_admin> usr_type_admins { get; set; }
        public virtual DbSet<usr_user> usr_users { get; set; }
        public virtual DbSet<view_Resource> view_Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local)\\dcserver;Initial Catalog=Rapsodi_db;Integrated Security=True;Connect Timeout=45");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<res_geo_city_admin>(entity =>
            {
                entity.HasKey(e => e.id_city)
                    .HasName("PK_geo_city");

                entity.HasOne(d => d.id_stateNavigation)
                    .WithMany(p => p.res_geo_city_admins)
                    .HasForeignKey(d => d.id_state)
                    .HasConstraintName("FK_geo_city_geo_state_id_geo_state");
            });

            modelBuilder.Entity<res_geo_country_admin>(entity =>
            {
                entity.HasKey(e => e.id_country)
                    .HasName("PK_geo_country");
            });

            modelBuilder.Entity<res_geo_state_admin>(entity =>
            {
                entity.HasKey(e => e.id_state)
                    .HasName("PK_geo_state");

                entity.HasOne(d => d.id_countryNavigation)
                    .WithMany(p => p.res_geo_state_admins)
                    .HasForeignKey(d => d.id_country)
                    .HasConstraintName("FK_geo_state_geo_country_id_geo_country");
            });

            modelBuilder.Entity<res_resource>(entity =>
            {
                entity.HasKey(e => e.id_resource)
                    .HasName("PK_resource");

                entity.HasOne(d => d.id_resource_typeNavigation)
                    .WithMany(p => p.res_resources)
                    .HasForeignKey(d => d.id_resource_type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_res_resource_admin_res_type_admin");
            });

            modelBuilder.Entity<usr_business>(entity =>
            {
                entity.HasKey(e => e.id_user_business)
                    .HasName("PK_user_business");

                entity.Property(e => e.id_user_business).ValueGeneratedNever();

                entity.HasOne(d => d.id_user_businessNavigation)
                    .WithOne(p => p.usr_business)
                    .HasForeignKey<usr_business>(d => d.id_user_business)
                    .HasConstraintName("FK_user_business_user_id_user_business");
            });

            modelBuilder.Entity<usr_person>(entity =>
            {
                entity.HasKey(e => e.id_user_person)
                    .HasName("PK_user_person");

                entity.Property(e => e.id_user_person).ValueGeneratedNever();

                entity.HasOne(d => d.id_user_personNavigation)
                    .WithOne(p => p.usr_person)
                    .HasForeignKey<usr_person>(d => d.id_user_person)
                    .HasConstraintName("FK_user_person_user_id_user_person");
            });

            modelBuilder.Entity<usr_resource_usage>(entity =>
            {
                entity.HasKey(e => e.id_resource_use)
                    .HasName("PK_usr_resource_use");
            });

            modelBuilder.Entity<usr_resource_usage_user>(entity =>
            {
                entity.HasOne(d => d.id_resource_useNavigation)
                    .WithMany(p => p.usr_resource_usage_users)
                    .HasForeignKey(d => d.id_resource_use)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usr_resource_usage_user_usr_resource_usage");

                entity.HasOne(d => d.id_user_app_userNavigation)
                    .WithMany(p => p.usr_resource_usage_users)
                    .HasForeignKey(d => d.id_user_app_user)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usr_resource_usage_user_usr_resource_user");
            });

            modelBuilder.Entity<usr_resource_user>(entity =>
            {
                entity.HasKey(e => e.id_user_app_user)
                    .HasName("PK_usr_app_user");

                entity.HasOne(d => d.id_resourceNavigation)
                    .WithMany(p => p.usr_resource_users)
                    .HasForeignKey(d => d.id_resource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usr_resource-user_res_resource");

                entity.HasOne(d => d.id_userNavigation)
                    .WithMany(p => p.usr_resource_users)
                    .HasForeignKey(d => d.id_user)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usr_resource-user_usr_user");
            });

            modelBuilder.Entity<usr_type_admin>(entity =>
            {
                entity.HasKey(e => e.id_user_type)
                    .HasName("PK_user_type_admin");
            });

            modelBuilder.Entity<usr_user>(entity =>
            {
                entity.HasKey(e => e.id_user)
                    .HasName("PK_user");

                entity.HasOne(d => d.id_city_localizationNavigation)
                    .WithMany(p => p.usr_users)
                    .HasForeignKey(d => d.id_city_localization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usr_user_res_geo-city_admin");

                entity.HasOne(d => d.id_user_typeNavigation)
                    .WithMany(p => p.usr_users)
                    .HasForeignKey(d => d.id_user_type)
                    .HasConstraintName("FK_user_user_type_admin_id_user_type");
            });

            modelBuilder.Entity<view_Resource>(entity =>
            {
                entity.ToView("view_Resources");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
