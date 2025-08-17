namespace Microservicio2_Movimientos.DTOs
{
    public class CuentaPostDTO
    {
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; } = true;

        // En vez de clienteId opcional, recibimos NombreCliente
        public string NombreCliente { get; set; } = null!;
    }
}
