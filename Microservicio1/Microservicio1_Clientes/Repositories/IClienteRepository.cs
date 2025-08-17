using Microservicio1_Clientes.DTO;
using Microservicio1_Clientes.Models;

namespace Microservicio1_Clientes.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync(int id);
        Task AddWithPersonaAsync(ClienteDto dto);
        Task<bool> UpdateAsync(int id, ClienteDto dto);
        Task<bool> DeleteAsync(int id);



    }
}
