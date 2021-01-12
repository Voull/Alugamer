using Alugamer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Database
{
    public class ClienteDao : BaseDao
    {

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
                return erroDatabase.GeraErroGenerico(Utils.Erro.ERRO.ERRO_GENERICO);
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
                Telefone = Convert.ToString(resp.Rows[0]["telefone"]),
                Endereco = Convert.ToString(resp.Rows[0]["endereco"]),
                DataNascimento = Convert.ToDateTime(resp.Rows[0]["data_nascimento"], CultureInfo.InvariantCulture),
                Sexo = Convert.ToString(resp.Rows[0]["sexo"]),
                Cpf = Convert.ToString(resp.Rows[0]["cpf"])

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
                    Telefone = Convert.ToString(linhaCliente["telefone"]),
                    Endereco = Convert.ToString(linhaCliente["endereco"]),
                    DataNascimento = Convert.ToDateTime(linhaCliente["data_nascimento"], CultureInfo.InvariantCulture),
                    Sexo = Convert.ToString(linhaCliente["sexo"]),
                    Cpf = Convert.ToString(linhaCliente["cpf"])
                };

                listaClientes.Add(cliente);
			}

            return listaClientes;
        }

        public List<Cliente> ReadAllSimples()
        {
            string sql = $@"SELECT cod_cliente, nome 
                            from CAD_CLIENTES";

            DataTable resp = _conn.dataTable(sql);

            List<Cliente> listaClientes = new List<Cliente>();

            foreach (DataRow linhaCliente in resp.Rows)
            {
                Cliente cliente = new Cliente
                {
                    Id = Convert.ToInt32(linhaCliente["cod_cliente"]),
                    Nome = Convert.ToString(linhaCliente["nome"])
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
                return erroDatabase.GeraErroGenerico(Utils.Erro.ERRO.ERRO_GENERICO_DATABASE);
            }
        }

        public void Delete(int id)
        {
            string sql = $@"DELETE FROM CAD_CLIENTES WHERE COD_CLIENTE = {id}";
            
            _conn.execute(sql);
        }
    }
}
