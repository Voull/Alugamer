using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDClientes
    {
        private ClienteDao clienteDao;
        private ClienteValidation clienteValidation;
        public CRUDClientes()
        {
            clienteDao = new ClienteDao();
            clienteValidation = new ClienteValidation();
        }

        public Cliente Busca(int id)
        {
            Cliente cliente = clienteDao.Read(id);
            return cliente;
        }

        public List<Cliente> Lista()
        {
            List<Cliente> listaClientes = clienteDao.ReadAllSimples();

            return listaClientes;
            
        }

        public string Novo(Cliente cliente)
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

        public void Remove(int id)
        {
            clienteDao.Delete(id);
        }
    }
}
