using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Utils
{
    public enum ERRO_LOGIN { ERRO_LOGIN_INVALIDO, ERRO_SENHA_IGUAL, ERRO_SENHA_INCORRETA, ERRO_PERFIL_DIVERGENTE, ERRO_NOME_USUARIO_DUPLICADO, ERRO_PERMISSAO};

    public class ErroLogin : Erro
    {
        private readonly string erroLoginInvalido = "Usuário/Senha incorretos!";
        private readonly string erroSenhaInvalida = "Senha Atual incorreta!";
        private readonly string erroUsuarioDuplicado = "Nome de Usuário já existe";
        private readonly string erroSenhaIgual = "A Senha Nova não pode ser igual a Atual!";
        private readonly string erroPerfilDivergente = "Houve um problema com sua requisição. Favor fazer login novamente!";
        private readonly string erroPermissao = "Você não possui permissão para acessar esta página!";

        public string GeraErroLogin(ERRO_LOGIN erroLogin)
        {
            switch (erroLogin)
            {
                case ERRO_LOGIN.ERRO_LOGIN_INVALIDO:
                    return erroLoginInvalido;
                case ERRO_LOGIN.ERRO_SENHA_IGUAL:
                    return erroSenhaIgual;
                case ERRO_LOGIN.ERRO_SENHA_INCORRETA:
                    return erroSenhaInvalida;
                case ERRO_LOGIN.ERRO_PERFIL_DIVERGENTE:
                    return erroPerfilDivergente;
                case ERRO_LOGIN.ERRO_NOME_USUARIO_DUPLICADO:
                    return erroUsuarioDuplicado;
                case ERRO_LOGIN.ERRO_PERMISSAO:
                    return erroPermissao;
                default:
                    return GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }

    }
}
