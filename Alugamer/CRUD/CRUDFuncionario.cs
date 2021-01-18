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
    public class CRUDFuncionario
    {
        private FuncionarioDao funcionarioDAO;
        private FuncionarioValidation funcionarioValidation;
        private ErroDatabase erroDatabase;
        public CRUDFuncionario()
        {
            funcionarioDAO = new FuncionarioDao();
            funcionarioValidation = new FuncionarioValidation();
            erroDatabase = new ErroDatabase();
        }

        public Funcionario Busca(int id)
        {
            if (id == 0)
                return new Funcionario()
                {
                    Id = 0
                };

            Funcionario funcionario = funcionarioDAO.Read(id);
            return funcionario;
        }

        public List<Funcionario> Lista()
        {
            List<Funcionario> listaFuncionarios = funcionarioDAO.ReadAllSimples();

            return listaFuncionarios;

        }

        public string Insere(Funcionario funcionario)
        {
            List<String> errosFuncionario = funcionarioValidation.validar(funcionario);
            if (errosFuncionario.Count > 0) return string.Join(Environment.NewLine, errosFuncionario);

            string resposta = funcionarioDAO.Insert(funcionario);

            return resposta;
        }

        public string Edita(Funcionario funcionario)
        {
            List<String> errosFuncionario = funcionarioValidation.validar(funcionario);
            if (errosFuncionario.Count > 0) return string.Join(Environment.NewLine, errosFuncionario);

            string resposta = funcionarioDAO.Update(funcionario);

            return resposta;

        }

        public string Remove(int id)
        {
            try
            {
                if (!funcionarioDAO.Delete(id))
                    return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE);
                return string.Empty;
            }
            catch (SqlException)
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
                    if (!funcionarioDAO.Delete(id))
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
