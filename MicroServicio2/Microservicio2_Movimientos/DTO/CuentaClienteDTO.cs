namespace Microservicio2_Movimientos.DTO
{
    public class CuentaClienteDTO
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = null!;
        public int? Identificacion { get; set; }
    }
}
