using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Validations;


namespace Alugamer.CRUD
{
    public class CRUDAluguel
    {
        private AluguelDao aluguelDao;
        private AluguelValidation aluguelValidation;
        public CRUDAluguel()
        {
            aluguelDao = new AluguelDao();
            aluguelValidation = new AluguelValidation();
        }

        public string Novo(Aluguel aluguel)
        {
            List<String> errosAluguel = aluguelValidation.validar(aluguel);
            if (errosAluguel.Count > 0) return string.Join(Environment.NewLine, errosAluguel);

            string resposta = aluguelDao.Insert(aluguel);

            return resposta;
        }

        public List<Aluguel> Lista()
        {
            List<Aluguel> listaAluguel = aluguelDao.Lista();

            return listaAluguel;

        }

        public Aluguel Busca(int id)
        {
            if (id == 0)
                return new Aluguel()
                {
                    Id = 0
                };

            Aluguel aluguel = aluguelDao.Read(id);

            return aluguel;
        }
    }
}
