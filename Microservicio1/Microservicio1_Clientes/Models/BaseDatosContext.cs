using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Microservicio1_Clientes.Models;

public partial class BaseDatosContext : DbContext
{
    public BaseDatosContext()
    {
    }

    public BaseDatosContext(DbContextOptions<BaseDatosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Cliente>(entity =>
        //{
        //    entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__71ABD0873BD3F9E3");

        //    entity.ToTable("Cliente");

        //    entity.Property(e => e.ClienteId).ValueGeneratedNever();
        //    entity.Property(e => e.Contrasena).HasMaxLength(100);
        //    entity.Property(e => e.Estado).HasDefaultValue(true);

        //    entity.HasOne(d => d.ClienteNavigation).WithOne(p => p.Cliente)
        //       .HasForeignKey<Cliente>(d => d.ClienteId)
        //       .HasConstraintName("FK_Cliente_Persona");
        //});


        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__71ABD0873BD3F9E3");

            entity.ToTable("Cliente");

            entity.Property(e => e.ClienteId).ValueGeneratedNever();
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.Estado).HasDefaultValue(true);

            // -------- RELACIÓN CON PERSONA (ELIMINAR) --------
            entity.HasOne(d => d.ClienteNavigation)
                  .WithOne(p => p.Cliente)
                  .HasForeignKey<Cliente>(d => d.ClienteId)
                  .HasConstraintName("FK_Cliente_Persona");
            // NO OnDelete(DeleteBehavior.Cascade)
        });



        modelBuilder.Entity<Cuentum>(entity =>

        {
            entity.HasKey(e => e.CuentaId).HasName("PK__Cuenta__40072E81328480C1");

            entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuenta__E039507B72A587C5").IsUnique();

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NumeroCuenta).HasMaxLength(50);
            entity.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoCuenta).HasMaxLength(15);

            //entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
              //  .HasForeignKey(d => d.ClienteId)
              //  .HasConstraintName("FK_Cuenta_Cliente");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MovimientoId).HasName("PK__Movimien__BF923C2CB7162EB0");

            entity.ToTable("Movimiento");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK_Movimiento_Cuenta");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PersonaId).HasName("PK__Persona__7C34D303458E5651");

            entity.ToTable("Persona");

            entity.HasIndex(e => e.Identificacion, "UQ__Persona__D6F931E55866504E").IsUnique();

            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);

 




    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
