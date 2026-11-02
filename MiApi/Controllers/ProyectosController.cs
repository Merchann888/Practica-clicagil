using Microsoft.AspNetCore.Mvc;
using MiApi.Dtos;
using MiApi.Services;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoService _service;

        public ProyectosController(IProyectoService service)
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
            var proyecto = await _service.GetByIdAsync(id);
            if (proyecto == null) return NotFound();
            return Ok(proyecto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProyectoRequestDto dto)
        {
            var (resultado, proyecto) = await _service.CreateAsync(dto);

            if (resultado == ResultadoOperacion.TecnologiasInvalidas)
                return BadRequest("Una o más tecnologías no existen.");

            return CreatedAtAction(nameof(GetById), new { id = proyecto!.Id }, proyecto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProyectoRequestDto dto)
        {
            var resultado = await _service.UpdateAsync(id, dto);

            return resultado switch
            {
                ResultadoOperacion.NoEncontrado => NotFound(),
                ResultadoOperacion.TecnologiasInvalidas => BadRequest("Una o más tecnologías no existen."),
                _ => NoContent()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}