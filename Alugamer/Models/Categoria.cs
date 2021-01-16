using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Models
{
    public class Categoria
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public Categoria()
        {
            Id = -1;
            Nome = string.Empty;
            Descricao = string.Empty;
        }
    }
}
