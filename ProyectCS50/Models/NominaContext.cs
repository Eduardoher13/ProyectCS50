using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectCS50.Models;

public partial class NominaContext : DbContext
{
    public NominaContext()
    {
    }

    public NominaContext(DbContextOptions<NominaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deduccione> Deducciones { get; set; }

    public virtual DbSet<DetalleDeduccione> DetalleDeducciones { get; set; }

    public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<Nomina> Nominas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G25OVJH\\SQLEXPRESS;Database=Nomina;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deduccione>(entity =>
        {
            entity.HasKey(e => e.IdDeduccion).HasName("PK__Deduccio__D7BEE5B7F54257A5");

            entity.Property(e => e.IdDeduccion).HasColumnName("Id_Deduccion");
            entity.Property(e => e.NombreDeduccion)
                .HasMaxLength(50)
                .HasColumnName("Nombre_Deduccion");
        });

        modelBuilder.Entity<DetalleDeduccione>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDeducciones).HasName("PK__DetalleD__FEB3BBD004289D5E");

            entity.Property(e => e.IdDetalleDeducciones).HasColumnName("Id_DetalleDeducciones");
            entity.Property(e => e.FechaDeduccion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Deduccion");
            entity.Property(e => e.IdDeduccion).HasColumnName("Id_Deduccion");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDeduccionNavigation).WithMany(p => p.DetalleDeducciones)
                .HasForeignKey(d => d.IdDeduccion)
                .HasConstraintName("FK__DetalleDe__Id_De__5070F446");

            entity.HasOne(d => d.NumeroEmpleadoNavigation).WithMany(p => p.DetalleDeducciones)
                .HasForeignKey(d => d.NumeroEmpleado)
                .HasConstraintName("FK__DetalleDe__Numer__4F7CD00D");
        });

        modelBuilder.Entity<DetalleIngreso>(entity =>
        {
            entity.HasKey(e => e.IdDetalleIngresos).HasName("PK__DetalleI__69C6F96F3E842AB2");

            entity.Property(e => e.IdDetalleIngresos).HasColumnName("Id_DetalleIngresos");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Ingreso");
            entity.Property(e => e.IdIngreso).HasColumnName("Id_Ingreso");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdIngresoNavigation).WithMany(p => p.DetalleIngresos)
                .HasForeignKey(d => d.IdIngreso)
                .HasConstraintName("FK__DetalleIn__Id_In__571DF1D5");

            entity.HasOne(d => d.NumeroEmpleadoNavigation).WithMany(p => p.DetalleIngresos)
                .HasForeignKey(d => d.NumeroEmpleado)
                .HasConstraintName("FK__DetalleIn__Numer__5629CD9C");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.NumeroEmpleado).HasName("PK__Empleado__44F848FC791C67A1");

            entity.Property(e => e.Cedula).HasMaxLength(20);
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.EstadoCivil).HasMaxLength(20);
            entity.Property(e => e.FechaCierreContrato)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_CierreContrato");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Contratacion");
            entity.Property(e => e.FechaNac)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Nac");
            entity.Property(e => e.NInss)
                .HasMaxLength(50)
                .HasColumnName("N_Inss");
            entity.Property(e => e.NRuc)
                .HasMaxLength(50)
                .HasColumnName("N_Ruc");
            entity.Property(e => e.PrimerApellido).HasMaxLength(50);
            entity.Property(e => e.PrimerNombre).HasMaxLength(50);
            entity.Property(e => e.SalarioOrdinario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Salario_Ordinario");
            entity.Property(e => e.SegundoApellido).HasMaxLength(50);
            entity.Property(e => e.SegundoNombre).HasMaxLength(50);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.IdIngreso).HasName("PK__Ingresos__9A87CCD9B7495462");

            entity.Property(e => e.IdIngreso).HasColumnName("Id_Ingreso");
            entity.Property(e => e.NombreIngreso)
                .HasMaxLength(50)
                .HasColumnName("Nombre_Ingreso");
        });

        modelBuilder.Entity<Nomina>(entity =>
        {
            entity.HasKey(e => e.IdNomina).HasName("PK__Nomina__6B4092F4FD1EF8F9");

            entity.ToTable("Nomina");

            entity.Property(e => e.IdNomina).HasColumnName("Id_Nomina");
            entity.Property(e => e.DeduccionesTotales)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Deducciones_Totales");
            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IngresosTotales)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Ingresos_Totales");
            entity.Property(e => e.SalarioNeto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Salario_Neto");

            entity.HasOne(d => d.NumeroEmpleadoNavigation).WithMany(p => p.Nominas)
                .HasForeignKey(d => d.NumeroEmpleado)
                .HasConstraintName("FK__Nomina__NumeroEm__5AEE82B9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__D03DEDCBA2B00FE9");

            entity.Property(e => e.IdUser).HasColumnName("Id_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
