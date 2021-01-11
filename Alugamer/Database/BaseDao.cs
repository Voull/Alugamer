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

        public BaseDao()
        {
            erroDatabase = new ErroDatabase();
        }
    }
}
