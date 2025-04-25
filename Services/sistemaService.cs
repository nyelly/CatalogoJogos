using CatalogoJogos.Database;
using CatalogoJogos.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoJogos.Services
{
    public class sistemaService
    {
        private readonly AppDbContext _context;

        public sistemaService(AppDbContext context)
        {
            _context = context;
        }

        public List<consolegames> GetConsoles() => _context.Consolegames.Include(c => c.Jogos).ToList();

        public consolegames AddConsole(consolegames console)
        {
            _context.Consolegames.Add(console);
            _context.SaveChanges();
            return console;
        }

        public consolegames? GetConsole(int id)
        {
            return _context.Consolegames.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
        }

        public consolegames? UpdateConsole(int id, consolegames atualizado)
        {
            var console = _context.Consolegames.FirstOrDefault(c => c.Id == id);
            if (console == null) return null;

            console.Modelo = atualizado.Modelo;
            console.Ano = atualizado.Ano;

            _context.SaveChanges();
            return console;
        }

        public bool DeleteConsole(int id)
        {
            var console = _context.Consolegames
                .Include(c => c.Jogos) 
                .FirstOrDefault(c => c.Id == id);

            if (console == null) return false;

            if (console.Jogos != null && console.Jogos.Count > 0)
            {
                _context.Jogos.RemoveRange(console.Jogos);
            }

            _context.Consolegames.Remove(console);
            _context.SaveChanges();
            return true;
        }

        public List<jogos> GetJogos()
        {
            return _context.Jogos.Include(j => j.Plataforma).ToList();
        }

        public jogos? GetJogo(int id)
        {
            return _context.Jogos.Include(j => j.Plataforma).FirstOrDefault(j => j.Id == id);
        }

        public jogos? AddJogos(jogos jogo)
        {
            var plataforma = _context.Consolegames.Find(jogo.PlataformaId);
            if (plataforma == null) return null;

            jogo.Plataforma = plataforma;
            jogo.PlataformaId = plataforma.Id;

            _context.Jogos.Add(jogo);
            _context.SaveChanges();
            return jogo;
        }

        public jogos? UpdateJogos(int id, jogos atualizado)
        {
            var jogo = _context.Jogos.Include(j => j.Plataforma).FirstOrDefault(j => j.Id == id);
            var plataforma = _context.Consolegames.Find(atualizado.PlataformaId);
            if (jogo == null || plataforma == null) return null;

            jogo.Nome = atualizado.Nome;
            jogo.Genero = atualizado.Genero;
            jogo.AnoLancamento = atualizado.AnoLancamento;
            jogo.TipoMidia = atualizado.TipoMidia;
            jogo.UrlCapa = atualizado.UrlCapa;
            jogo.Plataforma = plataforma;
            jogo.PlataformaId = plataforma.Id;

            _context.SaveChanges();
            return jogo;
        }

        public bool DeleteJogos(int id)
        {
            var jogo = _context.Jogos.Find(id);
            if (jogo == null) return false;

            _context.Jogos.Remove(jogo);
            _context.SaveChanges();
            return true;
        }
    }
}