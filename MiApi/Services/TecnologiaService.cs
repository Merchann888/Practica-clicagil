using Microsoft.EntityFrameworkCore;
using MiApi.Data;
using MiApi.Dtos;
using MiApi.Models;

namespace MiApi.Services
{
    public class TecnologiaService : ITecnologiaService
    {
        private readonly AppDbContext _context;

        public TecnologiaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TecnologiaResponseDto>> GetAllAsync()
        {
            return await _context.Tecnologias
                .AsNoTracking()
                .Select(t => new TecnologiaResponseDto { Id = t.Id, Nombre = t.Nombre })
                .ToListAsync();
        }

        public async Task<TecnologiaResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Tecnologias
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TecnologiaResponseDto { Id = t.Id, Nombre = t.Nombre })
                .FirstOrDefaultAsync();
        }

        public async Task<TecnologiaResponseDto> CreateAsync(TecnologiaRequestDto dto)
        {
            var tecnologia = new Tecnologia { Nombre = dto.Nombre };
            _context.Tecnologias.Add(tecnologia);
            await _context.SaveChangesAsync();
            return new TecnologiaResponseDto { Id = tecnologia.Id, Nombre = tecnologia.Nombre };
        }

        public async Task<bool> UpdateAsync(int id, TecnologiaRequestDto dto)
        {
            var tecnologia = await _context.Tecnologias.FindAsync(id);
            if (tecnologia == null) return false;

            tecnologia.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tecnologia = await _context.Tecnologias.FindAsync(id);
            if (tecnologia == null) return false;

            _context.Tecnologias.Remove(tecnologia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}