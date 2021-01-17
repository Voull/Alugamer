using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public enum ERRO_DATABASE { ERRO_DELETAR_NAO_EXISTE, ERRO_DELETAR_MULTIPLO, ERRO_DELETAR_CONFLITO, ERRO_NAO_EXISTE };

    public class ErroDatabase : Erro
    {
        private readonly string erroDeletar = "O Item não pode ser apagado.";
        private readonly string erroDeletarMultiplo = "Alguns Itens não puderam ser apagados!\nVerifique se os Itens estão sendo utilizados!";
        private readonly string erroDeletarConflito = "O Item está sendo utilizado!";
        private readonly string erroNaoExiste = "O Item não existe!";

        public string GeraErroDatabase(ERRO_DATABASE tipoErro)
        {
            switch (tipoErro)
            {
                case ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE:
                    return String.Concat(erroDeletar, Environment.NewLine, erroNaoExiste);
                case ERRO_DATABASE.ERRO_NAO_EXISTE:
                    return erroNaoExiste;
                case ERRO_DATABASE.ERRO_DELETAR_CONFLITO:
                    return erroDeletarConflito;
                case ERRO_DATABASE.ERRO_DELETAR_MULTIPLO:
                    return erroDeletarMultiplo;
                default:
                    return GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }
    }
}
