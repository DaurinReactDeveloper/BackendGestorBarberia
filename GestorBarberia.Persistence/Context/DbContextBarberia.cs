using System;
using System.Collections.Generic;
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

        public virtual DbSet<Administradores> Administradores { get; set; } = null!;
        public virtual DbSet<Barberos> Barberos { get; set; } = null!;
        public virtual DbSet<Citas> Citas { get; set; } = null!;
        public virtual DbSet<Clientes> Clientes { get; set; } = null!;
        public virtual DbSet<Comentarios> Comentarios { get; set; } = null!;
        public virtual DbSet<Estilosdecortes> Estilosdecortes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=bxdpmbmztelivai8amdj-mysql.services.clever-cloud.com;database=bxdpmbmztelivai8amdj;user=uhu2fftmbq9ac18l;password=OTqA6PvRifOUUjSFwKUS", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Administradores>(entity =>
            {
                entity.HasKey(e => e.AdministradoresId)
                    .HasName("PRIMARY");

                entity.ToTable("administradores");

                entity.Property(e => e.AdministradoresId).HasColumnName("AdministradoresID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(70);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Barberos>(entity =>
            {
                entity.HasKey(e => e.BarberoId)  // Definir la clave primaria aquí
                    .HasName("PRIMARY");

                entity.ToTable("barberos");

                entity.Property(e => e.BarberoId).HasColumnName("BarberoID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Imgbarbero).HasMaxLength(255);

                entity.Property(e => e.Nombre).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.CitaId)  // Definir la clave primaria aquí
      .HasName("PRIMARY");

                entity.ToTable("citas");

                entity.HasIndex(e => e.ClienteId, "ClienteID");

                entity.HasIndex(e => e.EstiloId, "EstiloID");

                entity.HasIndex(e => e.BarberoId, "citas_ibfk_1");

                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.BarberoId).HasColumnName("BarberoID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Estado).HasColumnType("enum('En Proceso','Aceptada','Rechazada','Realizada','Cancelada')");

                entity.Property(e => e.EstiloId).HasColumnName("EstiloID");

                entity.Property(e => e.Hora).HasColumnType("time");

                entity.HasOne(d => d.Barbero)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.BarberoId)
                    .OnDelete(DeleteBehavior.Cascade)
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

            modelBuilder.Entity<Clientes>(entity =>
            {

                entity.HasKey(e => e.ClienteId)  // Define la clave primaria aquí
       .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Imgcliente).HasMaxLength(255);

                entity.Property(e => e.Nombre).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Comentarios>(entity =>
            {
                entity.HasKey(e => e.IdComentarios)
                    .HasName("PRIMARY");

                entity.ToTable("comentarios");

                entity.HasIndex(e => e.IdCliente, "fk_id_cliente_idx");

                entity.HasIndex(e => e.IdCorte, "fk_id_corte_idx");

                entity.HasIndex(e => e.IdBarbero, "fk_id_barbero_idx");

                entity.HasIndex(e => e.IdCita, "fk_id_cita_idx");

                entity.Property(e => e.IdComentarios)
                    .ValueGeneratedNever()
                    .HasColumnName("idComentarios");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(45)
                    .HasColumnName("Comentario");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdCorte).HasColumnName("idCorte");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_id_cliente");

                entity.HasOne(d => d.IdCorteNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdCorte)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_id_corte");

                entity.HasOne(d => d.IdBarberoNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdBarbero)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_id_barbero");

                entity.HasOne(d => d.IdCitasNavigation)
                   .WithMany(p => p.Comentarios)
                  .HasForeignKey(d => d.IdCita)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("fk_id_cita");
            });

            modelBuilder.Entity<Estilosdecortes>(entity =>
            {
                entity.HasKey(e => e.EstiloId)
                    .HasName("PRIMARY");

                entity.ToTable("estilosdecortes");

                entity.Property(e => e.EstiloId).HasColumnName("EstiloID");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Imgestilo).HasMaxLength(255);

                entity.Property(e => e.Nombre).HasMaxLength(30);

                entity.Property(e => e.Precio).HasPrecision(10, 2);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
