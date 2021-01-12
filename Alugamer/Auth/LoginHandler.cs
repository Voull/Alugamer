using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Microsoft.AspNetCore.Http;

namespace Alugamer.Auth
{
    public class LoginHandler
    {
        private LoginDAO loginDAO;
        private HttpContext context;
        public LoginHandler(HttpContext context)
        {
            loginDAO = new LoginDAO();
            this.context = context;
        }
        public bool AuthLogin(string nomeUsuario, string senha)
        {
            int codFuncionario = loginDAO.ValidaLogin(nomeUsuario, senha);

            if (codFuncionario == 0)
                return false;
            
            GeraToken(codFuncionario);

            return true;
        }

        public void GeraToken(int codFuncionario)
        {
            UserInfo userInfo = loginDAO.GetUserInfo(codFuncionario);

            string token = TokenService.GenerateToken(userInfo);

            context.Response.Cookies.Append("auth", token);
        }
    }
}
