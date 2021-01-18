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


        public List<RelatorioRow> buscaTodos(DateTime dataIni, DateTime dataFim)
        {
            List<RelatorioRow> listaAlugavel = relatoriolDao.ReadAll(dataIni, dataFim);

            return listaAlugavel;
          
        }

        public List<RelatorioRow> BuscaCliente(DateTime dataIni, DateTime dataFim, int id)
        {
            List<RelatorioRow> listaAlugavel = relatoriolDao.ReadAllCliente(dataIni, dataFim, id);

            return listaAlugavel;

        }

    }
}
