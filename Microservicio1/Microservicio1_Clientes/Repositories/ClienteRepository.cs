
using Microsoft.EntityFrameworkCore;
using Microservicio1_Clientes.DTO;
using Microservicio1_Clientes.Models;


namespace Microservicio1_Clientes.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BaseDatosContext _context;

        public ClienteRepository(BaseDatosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.ClienteNavigation) // Persona
                .Select(c => new ClienteDto
                {
                    // Persona
                    ClienteId = c.ClienteId,
                    Nombre = c.ClienteNavigation.Nombre,
                    Genero = c.ClienteNavigation.Genero,
                    Edad = c.ClienteNavigation.Edad,
                    Identificacion = (int)c.ClienteNavigation.Identificacion,
                    Direccion = c.ClienteNavigation.Direccion,
                    Telefono = c.ClienteNavigation.Telefono,

                    // Cliente
                    Contrasena = c.Contrasena,
                    Estado = c.Estado
                })
                .ToListAsync();
        }

        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.ClienteNavigation)
                .Where(c => c.ClienteId == id)
                .Select(c => new ClienteDto
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.ClienteNavigation.Nombre,
                    Genero = c.ClienteNavigation.Genero,
                    Edad = c.ClienteNavigation.Edad,
                    Identificacion = (int)c.ClienteNavigation.Identificacion,
                    Direccion = c.ClienteNavigation.Direccion,
                    Telefono = c.ClienteNavigation.Telefono,
                    Contrasena = c.Contrasena,
                    Estado = c.Estado
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddWithPersonaAsync(ClienteDto dto)
        {
            // 1️⃣ Validar Edad mínima
            if (dto.Edad < 16)
            {
                throw new InvalidOperationException("La edad mínima permitida es 16 años");
            }

            // 2️⃣ Validar Identificación duplicada
            var existeIdentificacion = await _context.Personas
                .AnyAsync(p => p.Identificacion == dto.Identificacion);
            if (existeIdentificacion)
            {
                throw new InvalidOperationException("La identificación ya existe en el sistema");
            }

            // 3️⃣ Crear Persona
            var persona = new Persona
            {
                Nombre = dto.Nombre,
                Genero = dto.Genero,
                Edad = (int)dto.Edad,
                Identificacion = dto.Identificacion,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            // 4️⃣ Validar duplicado de ClienteId (ya que es FK compartida con PersonaId)
            var existeCliente = await _context.Clientes.AnyAsync(c => c.ClienteId == persona.PersonaId);
            if (existeCliente)
            {
                throw new InvalidOperationException("No se puede duplicar un Id de cliente");
            }

            // 5️⃣ Crear Cliente
            var cliente = new Cliente
            {
                ClienteId = persona.PersonaId,
                Contrasena = dto.Contrasena,
                Estado = dto.Estado
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, ClienteDto dto)
        {
            var cliente = await _context.Clientes
                .Include(c => c.ClienteNavigation) // Incluye datos de Persona
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
                return false;

            // Validaciones personalizadas
            if (dto.Edad < 0)
                throw new Exception("La edad no puede ser negativa");
            if (dto.Edad < 16)
                throw new Exception("La edad mínima permitida es 16 años");

            // Validar duplicidad de identificación
            bool identificacionDuplicada = await _context.Personas
                .AnyAsync(p => p.Identificacion == dto.Identificacion &&
                               p.PersonaId != cliente.ClienteNavigation.PersonaId);

            if (identificacionDuplicada)
                throw new Exception("La identificación ya existe en el sistema");

            // Actualizar datos de Persona
            cliente.ClienteNavigation.Nombre = dto.Nombre;
            cliente.ClienteNavigation.Genero = dto.Genero;
            cliente.ClienteNavigation.Edad = (int)dto.Edad;
            cliente.ClienteNavigation.Identificacion = dto.Identificacion;
            cliente.ClienteNavigation.Direccion = dto.Direccion;
            cliente.ClienteNavigation.Telefono = dto.Telefono;

            // Actualizar datos de Cliente
            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Traer cliente e incluir su Persona
            var cliente = await _context.Clientes
                .Include(c => c.ClienteNavigation) // Trae la Persona relacionada
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
                return false;

            // Primero borramos el Cliente
            _context.Clientes.Remove(cliente);

            // Luego borramos la Persona asociada
            if (cliente.ClienteNavigation != null)
                _context.Personas.Remove(cliente.ClienteNavigation);

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
