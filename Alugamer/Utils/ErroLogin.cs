using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public enum ERRO_LOGIN { ERRO_LOGIN_INVALIDO, ERRO_SENHA_IGUAL, ERRO_SENHA_INCORRETA, ERRO_PERFIL_DIVERGENTE};

    public class ErroLogin : Erro
    {
        private readonly string erroLoginInvalido = "Usuário/Senha incorretos!";
        private readonly string erroSenhaInvalida = "Senha Atual incorreta!";
        private readonly string erroSenhaIgual = "A Senha Nova não pode ser igual à Atual!";
        private readonly string erroPerfilDivergente = "Houve um problema com sua requisição. Favor fazer login novamente!";

        public string GeraErroLogin(ERRO_LOGIN erroLogin)
        {
            switch (erroLogin)
            {
                case ERRO_LOGIN.ERRO_LOGIN_INVALIDO:
                    return erroLoginInvalido;
                case ERRO_LOGIN.ERRO_SENHA_IGUAL:
                    return erroSenhaIgual;
                case ERRO_LOGIN.ERRO_SENHA_INCORRETA:
                    return erroSenhaIgual;
                case ERRO_LOGIN.ERRO_PERFIL_DIVERGENTE:
                    return erroSenhaIgual;
                default:
                    return GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }

    }
}
