using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatalogoJogos.Models
{
    public class consolegames
    {
        public int Id { get; set; }

        [Required]
        public string Modelo { get; set; }

        public int Ano { get; set; }

        public List<jogos> Jogos { get; set; }
    }
}