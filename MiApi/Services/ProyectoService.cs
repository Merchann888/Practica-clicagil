using Microsoft.EntityFrameworkCore;
using MiApi.Data;
using MiApi.Dtos;
using MiApi.Models;

namespace MiApi.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly AppDbContext _context;

        public ProyectoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProyectoResponseDto>> GetAllAsync()
        {
            var proyectos = await _context.Proyectos
                .AsNoTracking()
                .Include(p => p.Tecnologias)
                .ToListAsync();

            return proyectos.Select(ToResponse).ToList();
        }

        public async Task<ProyectoResponseDto?> GetByIdAsync(int id)
        {
            var proyecto = await _context.Proyectos
                .AsNoTracking()
                .Include(p => p.Tecnologias)
                .FirstOrDefaultAsync(p => p.Id == id);

            return proyecto == null ? null : ToResponse(proyecto);
        }

        public async Task<(ResultadoOperacion Resultado, ProyectoResponseDto? Proyecto)> CreateAsync(ProyectoRequestDto dto)
        {
            var ids = dto.TecnologiaIds.Distinct().ToList();
            var tecnologias = await _context.Tecnologias
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();

            if (tecnologias.Count != ids.Count)
                return (ResultadoOperacion.TecnologiasInvalidas, null);

            var proyecto = new Proyecto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Tecnologias = tecnologias
            };

            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return (ResultadoOperacion.Exito, ToResponse(proyecto));
        }

        public async Task<ResultadoOperacion> UpdateAsync(int id, ProyectoRequestDto dto)
        {
            var proyecto = await _context.Proyectos
                .Include(p => p.Tecnologias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proyecto == null) return ResultadoOperacion.NoEncontrado;

            var ids = dto.TecnologiaIds.Distinct().ToList();
            var tecnologias = await _context.Tecnologias
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();

            if (tecnologias.Count != ids.Count)
                return ResultadoOperacion.TecnologiasInvalidas;

            proyecto.Nombre = dto.Nombre;
            proyecto.Descripcion = dto.Descripcion;

            proyecto.Tecnologias.Clear();
            foreach (var tecnologia in tecnologias)
                proyecto.Tecnologias.Add(tecnologia);

            await _context.SaveChangesAsync();
            return ResultadoOperacion.Exito;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var proyecto = await _context.Proyectos
                .Include(p => p.Tecnologias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proyecto == null) return false;

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
            return true;
        }

        private static ProyectoResponseDto ToResponse(Proyecto p) => new()
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaCreacion = p.FechaCreacion,
            Tecnologias = p.Tecnologias
                .Select(t => new TecnologiaResponseDto { Id = t.Id, Nombre = t.Nombre })
                .ToList()
        };
    }
}