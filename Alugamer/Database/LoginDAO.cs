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
            string sql = $"SELECT CL.COD_FUNCIONARIO, CL.NOME_USUARIO, CL.ADMIN, CF.NOME FROM CAD_LOGIN CL LEFT JOIN CAD_FUNCIONARIOS CF ON CL.COD_FUNCIONARIO = CF.COD_FUNCIONARIO WHERE CL.COD_FUNCIONARIO = {codFuncionario}";

            DataTable login = _conn.dataTable(sql);

            if (login.Rows.Count == 0)
                return new UserInfo();

            return new UserInfo
            {
                CodFuncionario = codFuncionario,
                NomeFuncionario = Convert.ToString(login.Rows[0]["NOME"]),
                NomeUsuario = Convert.ToString(login.Rows[0]["NOME_USUARIO"]),
                Admin = Convert.ToBoolean(login.Rows[0]["ADMIN"])
            };
        }

        public void SalvaPerfil(int codFuncionario, string senha)
        {
            string sql = $"UPDATE CAD_LOGIN SET SENHA = '{senha}' WHERE COD_FUNCIONARIO = '{codFuncionario}'";

            _conn.execute(sql);
        }

        public bool SalvaUsuario(int codFuncionario, string nomeUsuario, string senha, bool admin)
        {
                string sql = $@"IF NOT EXISTS (SELECT 1 FROM CAD_LOGIN WHERE NOME_USUARIO = '{nomeUsuario}')
                            BEGIN
                                IF EXISTS (SELECT 1 FROM CAD_LOGIN WHERE COD_FUNCIONARIO = {codFuncionario})
                                    BEGIN
                                        UPDATE CAD_LOGIN SET NOME_USUARIO = '{nomeUsuario}', SENHA = '{senha}', ADMIN = {Convert.ToInt32(admin)} WHERE COD_FUNCIONARIO = {codFuncionario}
                                    END
                                ELSE
                                    BEGIN
                                        INSERT INTO CAD_LOGIN (COD_FUNCIONARIO, NOME_USUARIO, SENHA, ADMIN) VALUES ({codFuncionario}, '{nomeUsuario}', '{senha}', {Convert.ToInt32(admin)})
                                    END
                                SELECT 1
                            END";

            return Convert.ToBoolean(_conn.scalar(sql));
        }
    }
}
