using MiApi.Dtos;

namespace MiApi.Services
{
    public interface ITecnologiaService
    {
        Task<List<TecnologiaResponseDto>> GetAllAsync();
        Task<TecnologiaResponseDto?> GetByIdAsync(int id);
        Task<TecnologiaResponseDto> CreateAsync(TecnologiaRequestDto dto);
        Task<bool> UpdateAsync(int id, TecnologiaRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}