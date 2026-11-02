using Microsoft.AspNetCore.Mvc;
using MiApi.Dtos;
using MiApi.Services;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TecnologiasController : ControllerBase
    {
        private readonly ITecnologiaService _service;

        public TecnologiasController(ITecnologiaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tecnologia = await _service.GetByIdAsync(id);
            if (tecnologia == null) return NotFound();
            return Ok(tecnologia);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TecnologiaRequestDto dto)
        {
            var creada = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TecnologiaRequestDto dto)
        {
            var actualizada = await _service.UpdateAsync(id, dto);
            if (!actualizada) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminada = await _service.DeleteAsync(id);
            if (!eliminada) return NotFound();
            return NoContent();
        }
    }
}