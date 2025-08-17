using Microservicio2_Movimientos.DTO;
using Microservicio2_Movimientos.DTOs;
using Microservicio2_Movimientos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicio2_Movimientos.Repositories
{
    public interface ICuentaRepository
    {

        Task<List<CuentaClienteDTO>> GetAllAsync();
        Task<CuentaClienteDTO> AddAsync(CuentaPostDTO dto);

        

        Task<string> DesactivarCuentaAsync(int cuentaId);
        Task UpdateAsync(int cuentaId, CuentaPutDTO dto);

    }

}
