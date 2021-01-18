using Alugamer.Models;
using Alugamer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Database
{
    public class FuncionarioDao : BaseDao
    {
        public string Insert(Funcionario funcionario)
        {
            string sql = $@"INSERT INTO CAD_FUNCIONARIOS (nome, email, telefone, endereco, data_nascimento, sexo, cpf)
                            VALUES ('{funcionario.Nome}', '{funcionario.Email}', '{funcionario.Telefone}', '{funcionario.Endereco}', 
                                    '{funcionario.DataNascimento.ToString("MM-dd-yyyy")}', '{funcionario.Sexo}', '{funcionario.Cpf}')";

            try
            {
                _conn.execute(sql);
                return string.Empty;
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }

        public Funcionario Read(int id)
        {
            string sql = $@"SELECT COD_FUNCIONARIO, nome, email, telefone, endereco, data_nascimento, sexo, cpf 
                            from CAD_FUNCIONARIOS where COD_FUNCIONARIO =({id})";

            DataTable resp = _conn.dataTable(sql);

            if (resp.Rows.Count == 0) return new Funcionario();

            return new Funcionario
            {
                Id = id,
                Nome = Convert.ToString(resp.Rows[0]["nome"]),
                Email = Convert.ToString(resp.Rows[0]["email"]),
                Telefone = Convert.ToString(resp.Rows[0]["telefone"]).Trim(),
                Endereco = Convert.ToString(resp.Rows[0]["endereco"]),
                DataNascimento = Convert.ToDateTime(resp.Rows[0]["data_nascimento"], CultureInfo.InvariantCulture),
                Sexo = Convert.ToString(resp.Rows[0]["sexo"]),
                Cpf = Convert.ToString(resp.Rows[0]["cpf"]).Trim()

            };
        }

        public List<Funcionario> ReadAll()
        {
            string sql = $@"SELECT COD_FUNCIONARIO, nome, email, telefone, endereco, data_nascimento, sexo, cpf 
                            from CAD_FUNCIONARIOS";

            DataTable resp = _conn.dataTable(sql);

            List<Funcionario> listaFuncionarios = new List<Funcionario>();

            foreach (DataRow linhaFuncionario in resp.Rows)
            {
                Funcionario funcionario = new Funcionario
                {
                    Id = Convert.ToInt32(linhaFuncionario["COD_FUNCIONARIO"]),
                    Nome = Convert.ToString(linhaFuncionario["nome"]),
                    Email = Convert.ToString(linhaFuncionario["email"]),
                    Telefone = Convert.ToString(linhaFuncionario["telefone"]).Trim(),
                    Endereco = Convert.ToString(linhaFuncionario["endereco"]),
                    DataNascimento = Convert.ToDateTime(linhaFuncionario["data_nascimento"], CultureInfo.InvariantCulture),
                    Sexo = Convert.ToString(linhaFuncionario["sexo"]),
                    Cpf = Convert.ToString(linhaFuncionario["cpf"]).Trim()
                };

                listaFuncionarios.Add(funcionario);
            }

            return listaFuncionarios;
        }

        public List<Funcionario> ReadAllSimples()
        {
            string sql = $@"SELECT COD_FUNCIONARIO, nome, cpf 
                            from CAD_FUNCIONARIOS";

            DataTable resp = _conn.dataTable(sql);

            List<Funcionario> listaFuncionarios = new List<Funcionario>();

            foreach (DataRow linhaFuncionario in resp.Rows)
            {
                Funcionario funcionario = new Funcionario
                {
                    Id = Convert.ToInt32(linhaFuncionario["COD_FUNCIONARIO"]),
                    Nome = Convert.ToString(linhaFuncionario["nome"]),
                    Cpf = Convert.ToString(linhaFuncionario["cpf"])
                };

                listaFuncionarios.Add(funcionario);
            }

            return listaFuncionarios;
        }

        public string Update(Funcionario funcionario)
        {
            string sql = $@"UPDATE CAD_FUNCIONARIOS set nome ='{funcionario.Nome}', email = '{funcionario.Email}', telefone = '{funcionario.Telefone}',
                            endereco = '{funcionario.Endereco}', data_nascimento = '{funcionario.DataNascimento.ToString("MM-dd-yyyy")}', sexo = '{funcionario.Sexo}',
                            cpf = '{funcionario.Cpf}' where COD_FUNCIONARIO = {funcionario.Id}";

            try
            {
                _conn.execute(sql);
                return string.Empty;
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE);
            }
        }

        public bool Delete(int id)
        {
            string sql = $@"IF EXISTS(SELECT 1 FROM CAD_FUNCIONARIOS WHERE COD_FUNCIONARIO = {id})
                            BEGIN
                                DELETE FROM CAD_FUNCIONARIOS WHERE COD_FUNCIONARIO = {id}
                                SELECT 1
                            END";

            return Convert.ToBoolean(_conn.scalar(sql));
        }

        public bool Exists(int id)
        {
            string sql = $"SELECT 1 FROM CAD_FUNCIONARIOS WHERE COD_FUNCIONARIO = {id}";

            return Convert.ToBoolean(_conn.scalar(sql));
        }

    }
}
