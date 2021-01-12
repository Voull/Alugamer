using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Models;

namespace Alugamer.Database
{
    public class LoginDAO : BaseDao
    {
        public int ValidaLogin(string nomeUsuario, string senha)
        {
            string sql = $"SELECT COD_FUNCIONARIO FROM CAD_LOGIN WHERE NOME_USUARIO = '{nomeUsuario}' AND SENHA = '{senha}'";

            int codFuncionario = Convert.ToInt32(_conn.scalar(sql));

            return codFuncionario;
        }

        public UserInfo GetUserInfo(int codFuncionario)
        {
            string sql = $"SELECT COD_FUNCIONARIO, NOME_USUARIO, COD_PERFIL FROM CAD_LOGIN WHERE COD_FUNCIONARIO = {codFuncionario}";

            DataTable login = _conn.dataTable(sql);

            if (login.Rows.Count == 0)
                return new UserInfo();

            return new UserInfo
            {
                CodFuncionario = codFuncionario,
                NomeUsuario = Convert.ToString(login.Rows[0]["NOME_USUARIO"]),
                CodPerfil = Convert.ToInt32(login.Rows[0]["COD_PERFIL"])
            };
        }
    }
}
