using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDAlugavel
    {
        private AlugavelDao alugavelDao;
        private AlugavelValidation alugavelValidation;
        public CRUDAlugavel()
        {
            alugavelDao = new AlugavelDao();
            alugavelValidation = new AlugavelValidation();
        }

        public Alugavel Busca(int id)
        {
            Alugavel alugavel = alugavelDao.Read(id);
            return alugavel;
        }

        public List<Alugavel> Lista()
        {
            List<Alugavel> listaAlugavel = alugavelDao.ReadAllSimples();

            return listaAlugavel;
            
        }

        public List<Alugavel> ListaCompleta(int categoria)
        {
            List<Alugavel> listaAlugavel = alugavelDao.ReadAllMaisDados(categoria);

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

        public void Remove(int id)
        {
            alugavelDao.Delete(id);
        }
    }
}
