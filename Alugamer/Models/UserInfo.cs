using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Models
{
    public class UserInfo
    {
        private int codFuncionario;
        private string nomeUsuario;
        private int codPerfil;

        public int CodFuncionario { get => codFuncionario; set => codFuncionario = value; }
        public string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        public int CodPerfil { get => codPerfil; set => codPerfil = value; }
    }
}
