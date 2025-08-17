using Microservicio2_Movimientos.DTOs;
using Microservicio2_Movimientos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicio2_Movimientos.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly BaseDatosContextM2 _context;

        public MovimientoRepository(BaseDatosContextM2 context)
        {
            _context = context;
        }

        // GET: traer todos los movimientos
        public async Task<IEnumerable<Movimiento>> GetAllAsync()
        {
            return await _context.Movimientos
                .Include(m => m.Cuenta)
                .ToListAsync();
        }

        // POST: agregar un nuevo movimiento con validaciones
        public async Task AddAsync(MovimientoPostDTO dto)
        {
            // Buscar la cuenta
            var cuenta = await _context.Cuenta.FirstOrDefaultAsync(c => c.CuentaId == dto.CuentaId);
            if (cuenta == null)
                throw new Exception("Cuenta no encontrada");

            // Validar valor negativo
            if (dto.Valor <= 0)
                throw new Exception("Revise sus valores, solo se permiten valores positivos");

            decimal nuevoSaldo = cuenta.SaldoInicial;

            if (dto.TipoMovimiento.ToLower() == "deposito")
            {
                nuevoSaldo += dto.Valor;
            }
            else if (dto.TipoMovimiento.ToLower() == "retiro")
            {
                if (dto.Valor > cuenta.SaldoInicial)
                    throw new Exception("Saldo no disponible");

                nuevoSaldo -= dto.Valor;
            }
            else
            {
                throw new Exception("Tipo de movimiento no válido");
            }

            // Crear registro del movimiento
            var movimiento = new Movimiento
            {
                CuentaId = dto.CuentaId,
                TipoMovimiento = dto.TipoMovimiento,
                Valor = dto.Valor,
                Saldo = nuevoSaldo
            };

            // Guardar movimiento y actualizar saldo de la cuenta
            _context.Movimientos.Add(movimiento);
            cuenta.SaldoInicial = nuevoSaldo;
            await _context.SaveChangesAsync();
        }
    }
}
