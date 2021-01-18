using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDRelatorio
    {
        private RelatorioDao relatoriolDao;
        public CRUDRelatorio()
        {
            relatoriolDao = new RelatorioDao();
        }


        public List<Aluguel> buscaTodos(DateTime dataIni, DateTime dataFim)
        {
            List<Aluguel> listaAlugavel = relatoriolDao.ReadAll(dataIni, dataFim);

            return listaAlugavel;
          
        }

        public List<Aluguel> BuscaCliente(DateTime dataIni, DateTime dataFim, int id)
        {
            List<Aluguel> listaAlugavel = relatoriolDao.ReadAllCliente(dataIni, dataFim, id);

            return listaAlugavel;

        }

    }
}
