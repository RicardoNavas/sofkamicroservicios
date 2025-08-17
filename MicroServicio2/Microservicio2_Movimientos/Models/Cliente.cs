using System;
using System.Collections.Generic;

namespace Microservicio2_Movimientos.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Contrasena { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Persona ClienteNavigation { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
