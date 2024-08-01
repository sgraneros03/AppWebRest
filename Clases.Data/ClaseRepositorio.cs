using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class ClaseRepositorio
    {
        private readonly AppDbContext _context;

        public ClaseRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClaseDTO>> ObtenerTodasLasClases()
        {
            return await _context.Clase.Select(g => new ClaseDTO
            {
                Id = g.Id,
                Nombre = g.Nombre,
                Fecha = g.Fecha,
                Pago = g.Pago
            }).ToListAsync();
        }

        public async Task<ClaseDTO> ObtenerClasePorId(int id)
        {
            var clase = await _context.Clase.FindAsync(id);
            if (clase == null) return null;

            return new ClaseDTO
            {
                Id = clase.Id,
                Nombre = clase.Nombre,
                Fecha = clase.Fecha,
                Pago = clase.Pago
            };
        }

        public async Task AgregarClase(ClaseDTO clase)
        {
            var nuevaClase = new ClaseDTO
            {
                Nombre = clase.Nombre,
                Fecha = clase.Fecha,
                Pago = clase.Pago
            };

            await _context.Clase.AddAsync(nuevaClase);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarClase(ClaseDTO clase)
        {
            var existingClase = await _context.Clase.FindAsync(clase.Id);
            if (existingClase == null) return;

            existingClase.Nombre = clase.Nombre;
            existingClase.Fecha = clase.Fecha;
            existingClase.Pago = clase.Pago;

            _context.Clase.Update(existingClase);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarClase(int id)
        {
            var clase = await _context.Clase.FindAsync(id);
            if (clase != null)
            {
                _context.Clase.Remove(clase);
                await _context.SaveChangesAsync();
            }
        }
    }
}