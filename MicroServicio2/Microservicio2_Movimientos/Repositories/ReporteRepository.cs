using Microservicio2_Movimientos.DTOs;
using Microservicio2_Movimientos.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservicio2_Movimientos.Repositories
{
    public class ReporteRepository
    {
        private readonly BaseDatosContextM2 _context;

        public ReporteRepository(BaseDatosContextM2 context)
        {
            _context = context;
        }

        public async Task<List<ReporteEstadoCuentaDTO>> GetReporteAsync(DateTime fechaInicio, DateTime fechaFin, string nombreCliente)
        {
            var reporte = await _context.Cuenta
                .Include(c => c.Cliente)
                    .ThenInclude(cl => cl.ClienteNavigation)
                .Include(c => c.Movimientos)
                .Where(c => c.Cliente.ClienteNavigation.Nombre.ToLower() == nombreCliente.ToLower())
                .SelectMany(c => c.Movimientos
                    .Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                    .Select(m => new ReporteEstadoCuentaDTO
                    {
                        Fecha = m.Fecha.ToString("MM/dd/yyyy"),
                        Cliente = c.Cliente.ClienteNavigation.Nombre,
                        NumeroCuenta = c.NumeroCuenta,
                        Tipo = c.TipoCuenta,
                        SaldoInicial = c.SaldoInicial,
                        Estado = c.Estado,
                        Movimiento = m.Valor,
                        SaldoDisponible = m.Saldo
                    })
                )
                .ToListAsync();

            return reporte;
        }
    }
}
