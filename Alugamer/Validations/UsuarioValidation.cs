using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Utils;

namespace Alugamer.Validations
{
    public class UsuarioValidation
    {
        ErroModel erroModel = new ErroModel();
        ErroLogin erroLogin = new ErroLogin();
        public UsuarioValidation()
        {
            erroModel = new ErroModel();
        }

        public List<String> validaPerfil(UserInfo perfil)
        {
            List<string> erros = new List<string>();

            if (perfil.CodFuncionario < 0)
                erros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Código Funcionário"));
            if (string.IsNullOrEmpty(perfil.NomeUsuario))
                erros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Usuário"));
            if (perfil.NomeUsuario.Length > 50)
                erros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_TAMANHO_MAX, "Usuário"));

            return erros;
        }

        public string validaSenha(string senhaAtual, string senhaNova)
        {
            if (senhaAtual.Equals(senhaNova))
                return erroLogin.GeraErroLogin(ERRO_LOGIN.ERRO_SENHA_IGUAL);

            return string.Empty;
        }
    }
}
