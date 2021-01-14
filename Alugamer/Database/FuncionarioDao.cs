using Alugamer.Models;
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
        private Conexao _conn;
        public FuncionarioDao()
		{
            _conn = new Conexao();
		}

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
                return erroDatabase.GeraErroGenerico(Utils.Erro.ERRO.ERRO_GENERICO);
            }
        }

        public Funcionario Read(int id)
        {
            string sql = $@"SELECT cod_funcionario, nome, email, telefone, endereco, data_nascimento, sexo, cpf 
                            from CAD_FUNCIONARIOS where cod_funcionario =({id})";

            DataTable resp = _conn.dataTable(sql);

            if (resp.Rows.Count == 0) return new Funcionario();

            return new Funcionario
            {
                Id = id,
                Nome = Convert.ToString(resp.Rows[0]["nome"]),
                Email = Convert.ToString(resp.Rows[0]["email"]),
                Telefone = Convert.ToString(resp.Rows[0]["telefone"]),
                Endereco = Convert.ToString(resp.Rows[0]["endereco"]),
                DataNascimento = Convert.ToDateTime(resp.Rows[0]["data_nascimento"], CultureInfo.InvariantCulture),
                Sexo = Convert.ToString(resp.Rows[0]["sexo"]),
                Cpf = Convert.ToString(resp.Rows[0]["cpf"])

            };
        }

        public List<Funcionario> ReadAll()
		{
            string sql = $@"SELECT cod_funcionario, nome, email, telefone, endereco, data_nascimento, sexo, cpf 
                            from CAD_FUNCIONARIOS";

            DataTable resp = _conn.dataTable(sql);

            List<Funcionario> listaFuncionarios = new List<Funcionario>();

            foreach(DataRow linhaFuncionario in resp.Rows)
			{
                Funcionario funcionario = new Funcionario
                {
                    Id = Convert.ToInt32(linhaFuncionario["cod_funcionario"]),
                    Nome = Convert.ToString(linhaFuncionario["nome"]),
                    Email = Convert.ToString(linhaFuncionario["email"]),
                    Telefone = Convert.ToString(linhaFuncionario["telefone"]),
                    Endereco = Convert.ToString(linhaFuncionario["endereco"]),
                    DataNascimento = Convert.ToDateTime(linhaFuncionario["data_nascimento"], CultureInfo.InvariantCulture),
                    Sexo = Convert.ToString(linhaFuncionario["sexo"]),
                    Cpf = Convert.ToString(linhaFuncionario["cpf"])
                };

                listaFuncionarios.Add(funcionario);
			}

            return listaFuncionarios;
        }

        public List<Funcionario> ReadAllSimples()
        {
            string sql = $@"SELECT cod_funcionario, nome 
                            from CAD_FUNCIONARIOS";

            DataTable resp = _conn.dataTable(sql);

            List<Funcionario> listaFuncionarios = new List<Funcionario>();

            foreach (DataRow linhaFuncionario in resp.Rows)
            {
                Funcionario funcionario = new Funcionario
                {
                    Id = Convert.ToInt32(linhaFuncionario["cod_funcionario"]),
                    Nome = Convert.ToString(linhaFuncionario["nome"])
                };

                listaFuncionarios.Add(funcionario);
            }

            return listaFuncionarios;
        }

        public string Update(Funcionario funcionario)
        {
            string sql = $@"UPDATE CAD_FUNCIONARIOS set nome ='{funcionario.Nome}', email = '{funcionario.Email}', telefone = '{funcionario.Telefone}',
                            endereco = '{funcionario.Endereco}', data_nascimento = '{funcionario.DataNascimento.ToString("MM-dd-yyyy")}', sexo = '{funcionario.Sexo}',
                            cpf = '{funcionario.Cpf}' where cod_funcionario = {funcionario.Id}";

            try
            {
                _conn.execute(sql);
                return string.Empty;
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(Utils.Erro.ERRO.ERRO_GENERICO_DATABASE);
            }
        }

        public void Delete(int id)
        {
            string sql = $@"DELETE FROM CAD_FUNCIONARIOS WHERE COD_FUNCIONARIO = {id}";
            
            _conn.execute(sql);
        }
    }
}
