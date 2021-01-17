﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDUsuario
    {
        private LoginDAO loginDAO;
        private UsuarioValidation usuarioValidation;
        private ErroLogin erroLogin;

        public CRUDUsuario()
        {
            loginDAO = new LoginDAO();
            erroLogin = new ErroLogin();
            usuarioValidation = new UsuarioValidation();
        }

        public UserInfo Busca(int idFuncionario)
        {
            UserInfo info = loginDAO.GetUserInfo(idFuncionario);

            return info;
        }

        public string SalvaPerfil(UserInfo perfilOriginal, UserInfo perfil, string senhaAtual, string senhaNova)
        {
            if (perfilOriginal.CodFuncionario != perfil.CodFuncionario || perfil.NomeUsuario != perfilOriginal.NomeUsuario)
                return erroLogin.GeraErroLogin(ERRO_LOGIN.ERRO_PERFIL_DIVERGENTE);

            List<string> erros = usuarioValidation.validaPerfil(perfil);
            erros.Add(usuarioValidation.validaSenha(senhaAtual, senhaNova));
            erros.RemoveAll(o => o.Equals(string.Empty));

            if (erros.Count > 0)
                return string.Join(Environment.NewLine, erros);

            if (loginDAO.ValidaLogin(perfil.NomeUsuario, senhaAtual) == 0)
                return erroLogin.GeraErroLogin(ERRO_LOGIN.ERRO_SENHA_INCORRETA);

            loginDAO.SalvaPerfil(perfil.CodFuncionario, senhaNova);

            return string.Empty;
        }
    }
}
