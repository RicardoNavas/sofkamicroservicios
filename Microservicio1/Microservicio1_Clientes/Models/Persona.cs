using System;
using System.Collections.Generic;

namespace Microservicio1_Clientes.Models;

public partial class Persona
{
    public int PersonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int Edad { get; set; }

    public int? Identificacion { get; set; }

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual Cliente? Cliente { get; set; }
}
