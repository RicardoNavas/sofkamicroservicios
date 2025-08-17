namespace Microservicio2_Movimientos.DTOs
{
    public class MovimientoPostDTO
    {
        public int CuentaId { get; set; }           // Cuenta donde se realiza el movimiento
        public string TipoMovimiento { get; set; }  // "Deposito" o "Debito"
        public decimal Valor { get; set; }          // Valor del movimiento (positivo)
    }
}
