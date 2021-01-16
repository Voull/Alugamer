using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public class ErroDatabase : Erro
    {
        private readonly string erroDeletar = "O Item não pode ser apagado.";
        private readonly string erroNaoExiste = "O Item não existe!";

        public enum ERRO_DATABASE { ERRO_DELETAR_NAO_EXISTE, ERRO_NAO_EXISTE };
        public string GeraErroDatabase(ERRO_DATABASE tipoErro)
        {
            switch (tipoErro)
            {
                case ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE:
                    return String.Concat(erroDeletar, Environment.NewLine, erroNaoExiste);
                case ERRO_DATABASE.ERRO_NAO_EXISTE:
                    return erroNaoExiste;
                default:
                    return GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }
    }
}
