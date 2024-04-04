using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class PHARMATIME_DBContext : DbContext
    {
        public PHARMATIME_DBContext()
        {
        }

        public PHARMATIME_DBContext(DbContextOptions<PHARMATIME_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auditorium> Auditoria { get; set; } = null!;
        public virtual DbSet<Enfermedad> Enfermedads { get; set; } = null!;
        public virtual DbSet<GeneroUsuario> GeneroUsuarios { get; set; } = null!;
        public virtual DbSet<Medicamento> Medicamentos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<TipoPresentacion> TipoPresentacions { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioEnfermedad> UsuarioEnfermedads { get; set; } = null!;
        public virtual DbSet<UsuarioMedicamento> UsuarioMedicamentos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=PHARMATIME_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.HasKey(e => e.IdAuditoria)
                    .HasName("PK__AUDITORI__9644A3CE95613234");

                entity.ToTable("AUDITORIA");

                entity.Property(e => e.IdAuditoria).HasColumnName("id_auditoria");

                entity.Property(e => e.Accion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("accion");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.Tabla)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tabla");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.Property(e => e.VSql)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("v_sql");

                entity.Property(e => e.ValorAnterior)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("valor_anterior");

                entity.Property(e => e.ValorNuevo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("valor_nuevo");
            });

            modelBuilder.Entity<Enfermedad>(entity =>
            {
                entity.HasKey(e => e.IdEnfermedad)
                    .HasName("PK__ENFERMED__D027B3A00B0966AD");

                entity.ToTable("ENFERMEDAD");

                entity.Property(e => e.IdEnfermedad).HasColumnName("id_enfermedad");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<GeneroUsuario>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK__GENERO_U__99A8E4F994262307");

                entity.ToTable("GENERO_USUARIO");

                entity.Property(e => e.IdGenero).HasColumnName("id_genero");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamento)
                    .HasName("PK__MEDICAME__2588C032FFFE0A57");

                entity.ToTable("MEDICAMENTO");

                entity.Property(e => e.IdMedicamento).HasColumnName("id_medicamento");

                entity.Property(e => e.Contraindicaciones)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("contraindicaciones");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Presentacion).HasColumnName("presentacion");

                entity.Property(e => e.SirvePara)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sirve_para");

                entity.HasOne(d => d.PresentacionNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.Presentacion)
                    .HasConstraintName("FK__MEDICAMEN__prese__46E78A0C");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__ROL__6ABCB5E050E799A7");

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_rol");
            });

            modelBuilder.Entity<TipoPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdPresentacion)
                    .HasName("PK__TIPO_PRE__5F94E0EDB1C9B068");

                entity.ToTable("TIPO_PRESENTACION");

                entity.Property(e => e.IdPresentacion).HasColumnName("id_presentacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__4E3E04ADEC59CA3B");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Genero).HasColumnName("genero");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");

                entity.HasOne(d => d.GeneroNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Genero)
                    .HasConstraintName("FK__USUARIO__genero__3D5E1FD2");

                entity.HasOne(d => d.TipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.TipoUsuario)
                    .HasConstraintName("FK__USUARIO__tipo_us__3E52440B");
            });

            modelBuilder.Entity<UsuarioEnfermedad>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioEnfermedad)
                    .HasName("PK__USUARIO___90246C647AC71488");

                entity.ToTable("USUARIO_ENFERMEDAD");

                entity.Property(e => e.IdUsuarioEnfermedad).HasColumnName("id_usuario_enfermedad");

                entity.Property(e => e.IdEnfermedad).HasColumnName("id_enfermedad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.UsuarioEnfermedads)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .HasConstraintName("FK__USUARIO_E__id_en__4222D4EF");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioEnfermedads)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__USUARIO_E__id_us__412EB0B6");
            });

            modelBuilder.Entity<UsuarioMedicamento>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioMedicamento)
                    .HasName("PK__USUARIO___ADA6D39C4C617784");

                entity.ToTable("USUARIO_MEDICAMENTO");

                entity.Property(e => e.IdUsuarioMedicamento).HasColumnName("id_usuario_medicamento");

                entity.Property(e => e.Dosis)
                    .HasMaxLength(209)
                    .IsUnicode(false)
                    .HasColumnName("dosis");

                entity.Property(e => e.Durante)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("durante");

                entity.Property(e => e.IdMedicamento).HasColumnName("id_medicamento");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Intervalo).HasColumnName("intervalo");

                entity.HasOne(d => d.IdMedicamentoNavigation)
                    .WithMany(p => p.UsuarioMedicamentos)
                    .HasForeignKey(d => d.IdMedicamento)
                    .HasConstraintName("FK__USUARIO_M__id_me__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioMedicamentos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__USUARIO_M__id_us__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
