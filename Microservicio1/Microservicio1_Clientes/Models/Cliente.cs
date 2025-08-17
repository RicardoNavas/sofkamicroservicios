using System;
using System.Collections.Generic;

namespace Microservicio1_Clientes.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Contrasena { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Persona ClienteNavigation { get; set; } = null!;

    //public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
