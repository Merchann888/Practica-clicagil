using MiApi.Dtos;

namespace MiApi.Services
{
    public interface IProyectoService
    {
        Task<List<ProyectoResponseDto>> GetAllAsync();
        Task<ProyectoResponseDto?> GetByIdAsync(int id);
        Task<(ResultadoOperacion Resultado, ProyectoResponseDto? Proyecto)> CreateAsync(ProyectoRequestDto dto);
        Task<ResultadoOperacion> UpdateAsync(int id, ProyectoRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}