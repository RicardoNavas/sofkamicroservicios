using System;
using System.Collections.Generic;

namespace Microservicio1_Clientes.Models;

public partial class Cuentum
{
    public int CuentaId { get; set; }

    public string NumeroCuenta { get; set; } = null!;

    public string? TipoCuenta { get; set; }

    public decimal SaldoInicial { get; set; }

    public bool Estado { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
