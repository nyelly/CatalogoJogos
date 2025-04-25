using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoJogos.Models
{
    public class jogos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public int AnoLancamento { get; set; }
        public string TipoMidia { get; set; }
        public string UrlCapa { get; set; }
        public int PlataformaId { get; set; }
        public consolegames Plataforma { get; set; }
    }
}