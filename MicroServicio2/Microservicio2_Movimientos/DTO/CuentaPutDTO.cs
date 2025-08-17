namespace Microservicio2_Movimientos.DTOs
{
    public class CuentaPutDTO
    {
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; } // opcional, si quieres actualizar saldo también
    }
}
