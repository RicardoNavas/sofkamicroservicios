using Microsoft.AspNetCore.Mvc;
using Microservicio2_Movimientos.DTOs;
using Microservicio2_Movimientos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservicio2_Movimientos.DTO;

namespace Microservicio2_Movimientos.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaRepository _repo;

        public CuentasController(ICuentaRepository repo)
        {
            _repo = repo;
        }

        // GET: api/cuentas
        [HttpGet]
        public async Task<ActionResult<List<CuentaClienteDTO>>> GetAll()
        {
            var cuentas = await _repo.GetAllAsync();
            return Ok(cuentas);
        }

        // POST: api/cuentas
  
   
        [HttpPost]
        public async Task<ActionResult> PostCuenta([FromBody] CuentaPostDTO dto)
        {
            if (dto == null)
                return BadRequest("Debe enviar la información de la cuenta.");

            try
            {
                await _repo.AddAsync(dto);
                return Ok("CUENTA CREADA EXITOSAMENTE.");
            }
            catch (KeyNotFoundException) // Cliente no existe
            {
                return NotFound("Cliente no existe.");
            }
            catch (InvalidOperationException) // Cuenta duplicada
            {
                return BadRequest("Cuenta duplicada.");
            }
            catch (Exception ex) // Otros errores
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


      
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> PutCuenta(int id, CuentaPutDTO dto)
        {
            try
            {
                await _repo.UpdateAsync(id, dto);
                return Ok("Cuenta actualizada exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("desactivar/{cuentaId}")]
        public async Task<ActionResult<string>> DesactivarCuenta(int cuentaId)
        {
            try
            {
                var mensaje = await _repo.DesactivarCuentaAsync(cuentaId);
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al desactivar la cuenta: {ex.Message}");
            }
        }




    }
}
