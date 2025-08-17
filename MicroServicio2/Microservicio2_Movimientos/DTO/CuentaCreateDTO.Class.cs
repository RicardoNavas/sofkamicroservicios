namespace Microservicio2_Movimientos.DTO
{
    public class CuentaCreateDTO
    {
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public int ClienteId { get; set; }
    }
}
