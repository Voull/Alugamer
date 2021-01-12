using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public class ErroLogin : Erro
    {
        private readonly string erroLoginInvalido = "Usuário/Senha incorretos!";

        public enum ERRO_LOGIN {ERRO_LOGIN_INVALIDO};

        public string GeraErroLogin(ERRO_LOGIN erroLogin)
        {
            switch (erroLogin)
            {
                case ERRO_LOGIN.ERRO_LOGIN_INVALIDO:
                    return erroLoginInvalido;
                default:
                    return GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }

    }
}
