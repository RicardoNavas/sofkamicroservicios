using Microsoft.AspNetCore.Mvc;
using Microservicio2_Movimientos.DTOs;
using Microservicio2_Movimientos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicio2_Movimientos.Controllers
{
    [ApiController]
    [Route("api/movimientos")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _repo;

        public MovimientosController(IMovimientoRepository repo)
        {
            _repo = repo;
        }

        // GET: api/movimientos
        [HttpGet]
        public async Task<ActionResult<List<MovimientoDTO>>> GetAll()
        {
            var movimientos = await _repo.GetAllAsync();
            return Ok(movimientos);
        }

        // POST: api/movimientos

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovimientoPostDTO dto)
        {
            try
            {
                await _repo.AddAsync(dto);
                return Ok(new { mensaje = "Movimiento registrado correctamente" });
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno en el servidor", detalle = ex.Message });
            }
        }


    }
}
