using Microsoft.EntityFrameworkCore;
using Microservicio2_Movimientos.DTO;
using Microservicio2_Movimientos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservicio2_Movimientos.DTO;
using Microservicio2_Movimientos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio2_Movimientos.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly BaseDatosContextM2 _context;

        public CuentaRepository(BaseDatosContextM2 context)
        {
            _context = context;
        }

        public async Task<List<CuentaClienteDTO>> GetAllAsync()
        {
            return await _context.Cuenta
                .Select(c => new CuentaClienteDTO
                {
                    CuentaId = c.CuentaId,
                    NumeroCuenta = c.NumeroCuenta,
                    TipoCuenta = c.TipoCuenta,
                    SaldoInicial = c.SaldoInicial,
                    Estado = c.Estado,
                    ClienteId = c.ClienteId,
                    NombreCliente = _context.Clientes
                        .Where(cl => cl.ClienteId == c.ClienteId)
                        .Select(cl => cl.ClienteNavigation.Nombre)
                        .FirstOrDefault()!,
                    Identificacion = _context.Clientes
                        .Where(cl => cl.ClienteId == c.ClienteId)
                        .Select(cl => cl.ClienteNavigation.Identificacion)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<CuentaClienteDTO> AddAsync(CuentaPostDTO dto)
        {
            // Buscamos al cliente por nombre
            var cliente = await _context.Clientes
                .Include(c => c.ClienteNavigation) // traemos Persona
                .FirstOrDefaultAsync(c => c.ClienteNavigation.Nombre == dto.NombreCliente);

            if (cliente == null)
                throw new Exception("Cliente no existe");

            var cuenta = new Cuenta
            {
                NumeroCuenta = dto.NumeroCuenta,
                TipoCuenta = dto.TipoCuenta,
                SaldoInicial = dto.SaldoInicial,
                Estado = dto.Estado,
                ClienteId = cliente.ClienteId
            };

            _context.Cuenta.Add(cuenta);
            await _context.SaveChangesAsync();

            return new CuentaClienteDTO
            {
                CuentaId = cuenta.CuentaId,
                NumeroCuenta = cuenta.NumeroCuenta,
                TipoCuenta = cuenta.TipoCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Estado = cuenta.Estado,
                ClienteId = cliente.ClienteId,
                NombreCliente = cliente.ClienteNavigation.Nombre
            };
        }

        public async Task UpdateAsync(int cuentaId, CuentaPutDTO dto)
        {
            var cuenta = await _context.Cuenta
                .Include(c => c.Cliente)
                    .ThenInclude(cl => cl.ClienteNavigation)
                .FirstOrDefaultAsync(c => c.CuentaId == cuentaId);

            if (cuenta == null)
                throw new Exception("Cuenta no encontrada");

            var numeroDuplicado = await _context.Cuenta
                .AnyAsync(c => c.NumeroCuenta == dto.NumeroCuenta && c.CuentaId != cuentaId);
            if (numeroDuplicado)
                throw new Exception("Número de cuenta duplicado");

            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.SaldoInicial = dto.SaldoInicial;

            await _context.SaveChangesAsync();
        }

        public async Task<string> DesactivarCuentaAsync(int cuentaId)
        {
            var cuenta = await _context.Cuenta
                .FirstOrDefaultAsync(c => c.CuentaId == cuentaId);

            if (cuenta == null)
                return "Cuenta no encontrada";

            if (!cuenta.Estado)
                return "Cuenta ya está desactivada";

            cuenta.Estado = false;
            await _context.SaveChangesAsync();

            return "Cuenta desactivada correctamente";
        }




    }
}
