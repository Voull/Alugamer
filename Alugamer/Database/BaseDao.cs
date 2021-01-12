using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Utils;

namespace Alugamer.Database
{
    public class BaseDao
    {
        protected ErroDatabase erroDatabase;
        protected Conexao _conn;
        public BaseDao()
        {
            erroDatabase = new ErroDatabase();
            _conn = new Conexao();
        }
    }
}
