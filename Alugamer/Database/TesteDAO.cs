using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alugamer.Database
{
 #if (TRAVIS || TESTE)

    public class TesteDAO : BaseDao
    {
        private Conexao _conn;
        public TesteDAO()
        {
            _conn = new Conexao();
        }

        public void InicializaBDTeste()
        {
            string sql = "InitializeTests";

            _conn.execute(sql);
        }
    }
#endif
}
