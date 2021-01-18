using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Models
{
    public class UserInfo
    {
        public int CodFuncionario { get; set; }
        [DisplayName("Nome")]
        public string NomeFuncionario { get; set; }
        [DisplayName("Usuário")]
        public string NomeUsuario { get; set; }
        public bool Admin { get; set; }

        public UserInfo()
        {
            CodFuncionario = -1;
            NomeFuncionario = string.Empty;
            NomeUsuario = string.Empty;
            Admin = false;
        }

    }
}
