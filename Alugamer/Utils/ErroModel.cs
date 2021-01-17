using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public enum ERRO_MODEL { ERRO_CAMPO_OBRIGATORIO, ERRO_TAMANHO_MAX, ERRO_NUMEROS, ERRO_INVALIDO };
    public class ErroModel : Erro
    {
        private readonly string erroCampoObrigatorio = "Preencha o campo {0}";
        private readonly string erroCampoTamanhoMax = "O campo {0} excede o tamanho máximo permitido!";
        private readonly string erroApenasNumeros = "Use apenas números no campo {0}!";
        private readonly string erroCampoInvalido = "Campo {0} Inválido!";
        public string GeraErroModel(ERRO_MODEL erroModel, string nomeCampo)
        {
            switch (erroModel)
            {
                case ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO:
                    return string.Format(erroCampoObrigatorio, nomeCampo);
                case ERRO_MODEL.ERRO_TAMANHO_MAX:
                    return string.Format(erroCampoTamanhoMax, nomeCampo);
                case ERRO_MODEL.ERRO_NUMEROS:
                    return string.Format(erroApenasNumeros, nomeCampo);
                case ERRO_MODEL.ERRO_INVALIDO:
                    return string.Format(erroCampoInvalido, nomeCampo);
                default:
                    return GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }
    }
}
