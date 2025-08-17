using System;
using System.Collections.Generic;

namespace Microservicio1_Clientes.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }

    public int CuentaId { get; set; }

    public DateTime Fecha { get; set; }

    public string? TipoMovimiento { get; set; }

    public decimal Valor { get; set; }

    public decimal Saldo { get; set; }

    public virtual Cuentum Cuenta { get; set; } = null!;
}
