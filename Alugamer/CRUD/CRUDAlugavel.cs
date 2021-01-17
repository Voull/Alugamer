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
    public class CRUDAlugavel
    {
        private AlugavelDao alugavelDao;
        private AlugavelValidation alugavelValidation;
        private ErroDatabase erroDatabase;

        public CRUDAlugavel()
        {
            alugavelDao = new AlugavelDao();
            alugavelValidation = new AlugavelValidation();
            erroDatabase = new ErroDatabase();
        }

        public Alugavel Busca(int id)
        {
            if (id == 0)
                return new Alugavel()
                {
                    Id = 0
                };

            Alugavel alugavel = alugavelDao.Read(id);

            return alugavel;
        }

        public List<Alugavel> Lista()
        {
            List<Alugavel> listaAlugavel = alugavelDao.ReadAllSimples();

            return listaAlugavel;
            
        }

        public string Novo(Alugavel alugavel)
        {
            List<String> errosAlugavel = alugavelValidation.validar(alugavel);
            if (errosAlugavel.Count > 0) return string.Join(Environment.NewLine, errosAlugavel);

            string resposta = alugavelDao.Insert(alugavel);

            return resposta;
        }

        public string Edita(Alugavel alugavel)
        {
            List<String> errosAlugavel = alugavelValidation.validar(alugavel);
            if (errosAlugavel.Count > 0) return string.Join(Environment.NewLine, errosAlugavel);

            string resposta = alugavelDao.Update(alugavel);
            
            return resposta;

        }

        public string Remove(int id)
        {
            if (!alugavelDao.Delete(id))
                return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);

            return string.Empty;
        }

        public string Remove(List<int> listaId)
        {
            bool completo = true;
            foreach (int id in listaId)
            {
                try
                {
                    if (!alugavelDao.Delete(id))
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
