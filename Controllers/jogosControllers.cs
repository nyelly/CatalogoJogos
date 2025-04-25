using CatalogoJogos.Models;
using CatalogoJogos.Services;
using CatalogoJogos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly sistemaService _service;

        public JogosController(sistemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.GetJogos());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var jogo = _service.GetJogo(id);
            return jogo != null ? Ok(jogo) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] JogoDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados do jogo são necessários.");

            if (dto.PlataformaId == 0)
                return BadRequest("PlataformaId é necessário.");

            var plataforma = _service.GetConsoles().FirstOrDefault(c => c.Id == dto.PlataformaId);
            if (plataforma == null)
                return BadRequest("Plataforma inválida.");

            var novoJogo = new jogos
            {
                Nome = dto.Nome,
                Genero = dto.Genero,
                AnoLancamento = dto.AnoLancamento,
                TipoMidia = dto.TipoMidia,
                UrlCapa = dto.UrlCapa,
                PlataformaId = dto.PlataformaId
            };

            var adicionado = _service.AddJogos(novoJogo);
            return CreatedAtAction(nameof(Get), new { id = adicionado.Id }, adicionado);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JogoDTO dto)
        {
            var atualizado = new jogos
            {
                Nome = dto.Nome,
                Genero = dto.Genero,
                AnoLancamento = dto.AnoLancamento,
                TipoMidia = dto.TipoMidia,
                UrlCapa = dto.UrlCapa,
                PlataformaId = dto.PlataformaId
            };

            var editado = _service.UpdateJogos(id, atualizado);
            return editado != null ? Ok(editado) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _service.DeleteJogos(id) ? NoContent() : NotFound();
        }
    }
}