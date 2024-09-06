using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GestorBarberia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestorBarberia.Persistence.Context
{
    public partial class DbContextBarberia : DbContext
    {
        public DbContextBarberia()
        {
        }

        public DbContextBarberia(DbContextOptions<DbContextBarberia> options)
            : base(options)
        {
        }

        public virtual DbSet<Barbero> Barberos { get; set; } = null!;
        public virtual DbSet<Cita> Citas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Estilosdecorte> Estilosdecortes { get; set; } = null!;
        public virtual DbSet<Administrador> administradores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;database=db_barberia;user=root;password=abc2015as", ServerVersion.Parse("8.4.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Barbero>(entity =>
            {
                entity.ToTable("barberos");

                entity.Property(e => e.BarberoId).HasColumnName("BarberoID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("citas");

                entity.HasIndex(e => e.BarberoId, "BarberoID");

                entity.HasIndex(e => e.ClienteId, "ClienteID");

                entity.HasIndex(e => e.EstiloId, "EstiloID");

                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.BarberoId).HasColumnName("BarberoID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.EstiloId).HasColumnName("EstiloID");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Barbero)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.BarberoId)
                    .HasConstraintName("citas_ibfk_1");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("citas_ibfk_2");

                entity.HasOne(d => d.Estilo)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.EstiloId)
                    .HasConstraintName("citas_ibfk_3");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Estilosdecorte>(entity =>
            {
                entity.HasKey(e => e.EstiloId)
                    .HasName("PRIMARY");

                entity.ToTable("estilosdecorte");

                entity.Property(e => e.EstiloId).HasColumnName("EstiloID");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Precio).HasPrecision(10, 2);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
