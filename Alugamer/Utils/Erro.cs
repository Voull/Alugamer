using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public class Erro
    {
        private readonly string erroGenerico = "Erro Desconhecido!\nContate o Administrador.";
        private readonly string erroGenericoDatabase = "Erro ao Acessar o Banco de Dados!\nTente novamente mais tarde ou Contate o Administrador!";
       
        public enum ERRO {ERRO_GENERICO, ERRO_GENERICO_DATABASE};
        public string GeraErroGenerico(ERRO tipoErro)
        {
            switch (tipoErro)
            {
                case ERRO.ERRO_GENERICO:
                    return erroGenerico;
                case ERRO.ERRO_GENERICO_DATABASE:
                    return erroGenericoDatabase;
                default:
                    return erroGenerico;
            }
        }
    }
}
