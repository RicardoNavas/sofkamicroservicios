using System.Collections.Generic;

namespace Microservicio2_Movimientos.Models;

public partial class Cuenta
{
    public int CuentaId { get; set; }
    public string NumeroCuenta { get; set; } = null!;
    public string TipoCuenta { get; set; } = null!;
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; }
    public int ClienteId { get; set; }

    // 🔑 Navegación al cliente
    public virtual Cliente Cliente { get; set; } = null!;

    // 🔑 Relación con movimientos
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
