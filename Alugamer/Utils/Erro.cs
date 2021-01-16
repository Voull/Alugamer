using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public enum ERRO { ERRO_GENERICO, ERRO_GENERICO_DATABASE, ERRO_404 };
    public class Erro
    {
        private readonly string erroGenerico = "Erro Desconhecido!\nContate o Administrador.";
        private readonly string erro404 = "Página não encontrada!";
        private readonly string erroGenericoDatabase = "Erro ao Acessar o Banco de Dados!\nTente novamente mais tarde ou Contate o Administrador!";
       
        public string GeraErroGenerico(ERRO tipoErro)
        {
            switch (tipoErro)
            {
                case ERRO.ERRO_GENERICO:
                    return erroGenerico;
                case ERRO.ERRO_GENERICO_DATABASE:
                    return erroGenericoDatabase;
                case ERRO.ERRO_404:
                    return erro404;
                default:
                    return erroGenerico;
            }
        }
    }
}
