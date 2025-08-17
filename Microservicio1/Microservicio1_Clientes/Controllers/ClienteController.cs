// Controllers/ClienteController.cs
using Microsoft.AspNetCore.Mvc;
using Microservicio1_Clientes.Repositories;
using Microservicio1_Clientes.DTO;
using Microsoft.EntityFrameworkCore;


namespace Microservicio1_Clientes.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repo;

        public ClienteController(IClienteRepository repo)
        {
            _repo = repo;
        }

        // GET: /api/cliente/lista
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _repo.GetAllAsync();
            return Ok(lista);
        }

        // GET: /api/cliente/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var dto = await _repo.GetByIdAsync(id);
            if (dto is null) return NotFound(new { mensaje = "Cliente no encontrado" });
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new { mensaje = errores });
            }

            try
            {
                await _repo.AddWithPersonaAsync(dto);
                return Ok(new { mensaje = "Cliente creado con éxito" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { mensaje = "Error al guardar datos en la base de datos" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado, por favor intente más tarde" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _repo.UpdateAsync(id, dto);

                if (!updated)
                    return NotFound(new { message = $"No se encontró el cliente con ID {id}" });

                return Ok(new { message = "Cliente actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);

                if (!deleted)
                    return NotFound(new { message = $"No se encontró el cliente con ID {id}" });

                return Ok(new { message = "Cliente eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }           
        }
    }
}
