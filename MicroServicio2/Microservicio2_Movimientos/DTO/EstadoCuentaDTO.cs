
using Microservicio2_Movimientos.DTOs;

public class EstadoCuentaDTO
{
    public int CuentaId { get; set; }
    public string NumeroCuenta { get; set; } = null!;
    public string TipoCuenta { get; set; } = null!;
    public decimal SaldoActual { get; set; }
    public List<MovimientoDTO> Movimientos { get; set; } = new List<MovimientoDTO>();
}
