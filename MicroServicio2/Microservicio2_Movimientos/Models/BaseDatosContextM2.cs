using Microsoft.EntityFrameworkCore;

namespace Microservicio2_Movimientos.Models
{
    public partial class BaseDatosContextM2 : DbContext
    {
        public BaseDatosContextM2() { }

        public BaseDatosContextM2(DbContextOptions<BaseDatosContextM2> options)
            : base(options)
        {
        }

        // Solo lo que le compete a Micro2
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }

        // Opcional si quieres validar clienteId sin navegar al Cliente completo
        public virtual DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // vacío: se configura por DI en Program.cs
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ====== CLIENTE (solo simple para FK) ======
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__71ABD0873BD3F9E3");

                entity.ToTable("Cliente");

                entity.Property(e => e.ClienteId).ValueGeneratedNever();
                entity.Property(e => e.Contrasena).HasMaxLength(100);
                entity.Property(e => e.Estado).HasDefaultValue(true);

                entity.HasOne(d => d.ClienteNavigation)  // Cliente -> Persona
                    .WithOne(p => p.Cliente)             // Persona -> Cliente
                    .HasForeignKey<Cliente>(d => d.ClienteId)
                    .HasConstraintName("FK_Cliente_Persona");
            });

            // ====== CUENTA ======
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuentaId).HasName("PK__Cuenta__40072E81");

                entity.ToTable("Cuenta");

                entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuenta__E039507B").IsUnique();

                entity.Property(e => e.NumeroCuenta)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.TipoCuenta)
                    .HasMaxLength(15);

                entity.Property(e => e.SaldoInicial)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Estado)
                    .HasDefaultValue(true);

                // IMPORTANTE: NO configurar relación con Cliente aquí.
                // ClienteId queda como columna simple (FK en BD), pero sin navegación en EF para Micro2.
            });

            // ====== MOVIMIENTO ======
            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MovimientoId).HasName("PK__Movimien__BF923C2C");

                entity.ToTable("Movimiento");

                entity.Property(e => e.Fecha)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TipoMovimiento)
                      .HasMaxLength(10)
                      .IsUnicode(false);

                entity.Property(e => e.Valor)
                      .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Saldo)
                      .HasColumnType("decimal(18, 2)");

                // Relación Movimiento -> Cuenta (sí la necesitamos)
                entity.HasOne(d => d.Cuenta)
                      .WithMany(p => p.Movimientos)
                      .HasForeignKey(d => d.CuentaId)
                      .HasConstraintName("FK_Movimiento_Cuenta")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
