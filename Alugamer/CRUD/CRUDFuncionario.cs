using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDFuncionario
    {
        private FuncionarioDao funcionarioDao;
        private FuncionarioValidation funcionarioValidation;
        public CRUDFuncionario()
        {
            funcionarioDao = new FuncionarioDao();
            funcionarioValidation = new FuncionarioValidation();
        }

        public Funcionario Busca(int id)
        {
            Funcionario funcionario = funcionarioDao.Read(id);
            return funcionario;
        }

        public List<Funcionario> Lista()
        {
            List<Funcionario> listaFuncionarios = funcionarioDao.ReadAllSimples();

            return listaFuncionarios;
            
        }

        public string Novo(Funcionario funcionario)
        {
            List<String> errosFuncionario = funcionarioValidation.validar(funcionario);
            if (errosFuncionario.Count > 0) return string.Join(Environment.NewLine, errosFuncionario);

            string resposta = funcionarioDao.Insert(funcionario);

            return resposta;
        }

        public string Edita(Funcionario funcionario)
        {
            List<String> errosFuncionario = funcionarioValidation.validar(funcionario);
            if (errosFuncionario.Count > 0) return string.Join(Environment.NewLine, errosFuncionario);

            string resposta = funcionarioDao.Update(funcionario);
            
            return resposta;

        }

        public void Remove(int id)
        {
            funcionarioDao.Delete(id);
        }
    }
}
