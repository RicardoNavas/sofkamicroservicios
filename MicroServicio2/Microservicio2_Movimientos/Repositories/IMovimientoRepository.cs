using Microservicio2_Movimientos.DTOs;
using Microservicio2_Movimientos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicio2_Movimientos.Repositories
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<Movimiento>> GetAllAsync();
        Task AddAsync(MovimientoPostDTO dto);
    }
}
