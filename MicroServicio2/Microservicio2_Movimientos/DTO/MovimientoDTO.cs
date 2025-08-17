namespace Microservicio2_Movimientos.DTOs
{
    public class MovimientoDTO
    {
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Fecha { get; set; }
    }    
}
