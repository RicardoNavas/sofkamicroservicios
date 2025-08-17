using System;
using System.Collections.Generic;

namespace Microservicio2_Movimientos.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }
    public int CuentaId { get; set; }
    public DateTime Fecha { get; set; }
    public string? TipoMovimiento { get; set; } // "Deposito"/"Retiro" etc.
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }

    public virtual Cuenta Cuenta { get; set; } = null!;
}
