// Dtos/ClienteDto.cs
using System.ComponentModel.DataAnnotations;

namespace Microservicio1_Clientes.DTO
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El género es obligatorio")]
        public string Genero { get; set; } = null!;

        [Required(ErrorMessage = "La edad es obligatoria")]
        [Range(16, 120, ErrorMessage = "La edad mínima es 16 años")]
        public int? Edad { get; set; } = 0;

        [Required(ErrorMessage = "La identificación es obligatoria")]
        public int? Identificacion { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]

        //UNION DE TABLAS PERSONA Y CLIENTE PARA PODER
        //INSERTAR Y ACTUALIZAR LOS CAMPOS
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
