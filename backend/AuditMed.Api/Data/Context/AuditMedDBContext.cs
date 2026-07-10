using System;
using System.Collections.Generic;
using AuditMed.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditMed.Api.Data.Context;

public partial class AuditMedDBContext : DbContext
{
    public AuditMedDBContext(DbContextOptions<AuditMedDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atencion> Atencion { get; set; }

    public virtual DbSet<Paciente> Paciente { get; set; }

    public virtual DbSet<RegistroAtencion> RegistroAtencion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atencion>(entity =>
        {
            entity.Property(e => e.FechaAtencion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Atencion).HasConstraintName("FK_Atencion_Pacientes");
        });

        modelBuilder.Entity<RegistroAtencion>(entity =>
        {
            entity.Property(e => e.FechaAtencion).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
