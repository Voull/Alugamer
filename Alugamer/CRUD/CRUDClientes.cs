using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDClientes
    {
        private ClienteDao clienteDao;
        private ClienteValidation clienteValidation;
        private ErroDatabase erroDatabase;
        public CRUDClientes()
        {
            clienteDao = new ClienteDao();
            clienteValidation = new ClienteValidation();
            erroDatabase = new ErroDatabase();
        }

        public Cliente Busca(int id)
        {
            if (id == 0)
                return new Cliente()
                {
                    Id = 0
                };

            Cliente cliente = clienteDao.Read(id);
            return cliente;
        }

        public List<Cliente> Lista()
        {
            List<Cliente> listaClientes = clienteDao.ReadAllSimples();

            return listaClientes;
            
        }

        public string Insere(Cliente cliente)
        {
            List<String> errosCliente = clienteValidation.validar(cliente);
            if (errosCliente.Count > 0) return string.Join(Environment.NewLine, errosCliente);

            string resposta = clienteDao.Insert(cliente);

            return resposta;
        }

        public string Edita(Cliente cliente)
        {
            List<String> errosCliente = clienteValidation.validar(cliente);
            if (errosCliente.Count > 0) return string.Join(Environment.NewLine, errosCliente);

            string resposta = clienteDao.Update(cliente);
            
            return resposta;

        }

        public string Remove(int id)
        {
            try
            {
                if (!clienteDao.Delete(id))
                    return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE);
                return string.Empty;
            }
            catch(SqlException)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE);
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO);
            }

        }
        public string Remove(List<int> listaId)
        {
            bool completo = true;
            foreach (int id in listaId)
            {
                try
                {
                    if (!clienteDao.Delete(id))
                        completo = false;
                }
                catch (SqlException ex) when (ex.Number == (int)DatabaseErrorCodes.CONFLICT)
                {
                    completo = false;
                }
            }

            if (completo)
                return string.Empty;
            else
                return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_MULTIPLO);
        }

    }
}
