using Microsoft.AspNetCore.Mvc;
using Microservicio2_Movimientos.Repositories;
using Microservicio2_Movimientos.DTOs;

namespace Microservicio2_Movimientos.Controllers
{
    [ApiController]
    [Route("api/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteRepository _repo;

        public ReportesController(ReporteRepository repo)
        {
            _repo = repo;
        }

        // GET: api/reportes?fechaInicio=2022-01-01&fechaFin=2022-12-31&nombreCliente=Marianela%20Montalvo
        [HttpGet]
        public async Task<ActionResult<List<ReporteEstadoCuentaDTO>>> GetReporte([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin, [FromQuery] string nombreCliente)
        {
            var reporte = await _repo.GetReporteAsync(fechaInicio, fechaFin, nombreCliente);

            if (!reporte.Any())
                return NotFound("No se encontraron movimientos para el cliente en el rango indicado");

            return Ok(reporte);
        }
    }
}
