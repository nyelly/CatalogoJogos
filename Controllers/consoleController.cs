using CatalogoJogos.Models;
using CatalogoJogos.Services;
using CatalogoJogos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsoleController : ControllerBase
    {
        private readonly sistemaService _service;

        public ConsoleController(sistemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.GetConsoles());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var console = _service.GetConsole(id);
            return console != null ? Ok(console) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ConsoleGameDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados do console são necessários.");

            var novoConsole = new consolegames
            {
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Jogos = new List<jogos>() 
            };

            var adicionado = _service.AddConsole(novoConsole);
            return CreatedAtAction(nameof(Get), new { id = adicionado.Id }, adicionado);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ConsoleGameDTO dto)
        {
            var atualizado = new consolegames
            {
                Modelo = dto.Modelo,
                Ano = dto.Ano
            };

            var editado = _service.UpdateConsole(id, atualizado);
            return editado != null ? Ok(editado) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _service.DeleteConsole(id) ? NoContent() : NotFound();
        }
    }
}