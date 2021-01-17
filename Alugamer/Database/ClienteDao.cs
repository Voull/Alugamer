using Alugamer.Models;
using Alugamer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Database
{
    public class ClienteDao : BaseDao
    {
        private Conexao _conn;
        public ClienteDao()
		{
            _conn = new Conexao();
		}

        public string Insert(Cliente cliente)
		{
            string sql = $@"INSERT INTO CAD_CLIENTES (nome, email, telefone, endereco, data_nascimento, sexo, cpf)
                            VALUES ('{cliente.Nome}', '{cliente.Email}', '{cliente.Telefone}', '{cliente.Endereco}', 
                                    '{cliente.DataNascimento.ToString("MM-dd-yyyy")}', '{cliente.Sexo}', '{cliente.Cpf}')";

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

        public Cliente Read(int id)
        {
            string sql = $@"SELECT cod_cliente, nome, email, telefone, endereco, data_nascimento, sexo, cpf 
                            from CAD_CLIENTES where cod_cliente =({id})";

            DataTable resp = _conn.dataTable(sql);

            if (resp.Rows.Count == 0) return new Cliente();

            return new Cliente
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

        public List<Cliente> ReadAll()
		{
            string sql = $@"SELECT cod_cliente, nome, email, telefone, endereco, data_nascimento, sexo, cpf 
                            from CAD_CLIENTES";

            DataTable resp = _conn.dataTable(sql);

            List<Cliente> listaClientes = new List<Cliente>();

            foreach(DataRow linhaCliente in resp.Rows)
			{
                Cliente cliente = new Cliente
                {
                    Id = Convert.ToInt32(linhaCliente["cod_cliente"]),
                    Nome = Convert.ToString(linhaCliente["nome"]),
                    Email = Convert.ToString(linhaCliente["email"]),
                    Telefone = Convert.ToString(linhaCliente["telefone"]).Trim(),
                    Endereco = Convert.ToString(linhaCliente["endereco"]),
                    DataNascimento = Convert.ToDateTime(linhaCliente["data_nascimento"], CultureInfo.InvariantCulture),
                    Sexo = Convert.ToString(linhaCliente["sexo"]),
                    Cpf = Convert.ToString(linhaCliente["cpf"]).Trim()
                };

                listaClientes.Add(cliente);
			}

            return listaClientes;
        }

        public List<Cliente> ReadAllSimples()
        {
            string sql = $@"SELECT cod_cliente, nome, cpf 
                            from CAD_CLIENTES";

            DataTable resp = _conn.dataTable(sql);

            List<Cliente> listaClientes = new List<Cliente>();

            foreach (DataRow linhaCliente in resp.Rows)
            {
                Cliente cliente = new Cliente
                {
                    Id = Convert.ToInt32(linhaCliente["cod_cliente"]),
                    Nome = Convert.ToString(linhaCliente["nome"]),
                    Cpf = Convert.ToString(linhaCliente["cpf"])
                };

                listaClientes.Add(cliente);
            }

            return listaClientes;
        }

        public string Update(Cliente cliente)
        {
            string sql = $@"UPDATE CAD_CLIENTES set nome ='{cliente.Nome}', email = '{cliente.Email}', telefone = '{cliente.Telefone}',
                            endereco = '{cliente.Endereco}', data_nascimento = '{cliente.DataNascimento.ToString("MM-dd-yyyy")}', sexo = '{cliente.Sexo}',
                            cpf = '{cliente.Cpf}' where cod_cliente = {cliente.Id}";

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
            string sql = $@"IF EXISTS(SELECT 1 FROM CAD_CLIENTES WHERE COD_CLIENTE = {id})
                            BEGIN
                                DELETE FROM CAD_CLIENTES WHERE COD_CLIENTE = {id}
                                SELECT 1
                            END";

            return Convert.ToBoolean(_conn.scalar(sql));
        }

        public bool Exists(int id)
        {
            string sql = $"SELECT 1 FROM CAD_CLIENTES WHERE COD_CLIENTE = {id}";

            return Convert.ToBoolean(_conn.scalar(sql));
        }

    }
}
