using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Database
{
	public class Class
	{
		protected CsvReader tabela;

		public Class()
		{
			tabela = new CsvReader(new StreamReader("Clientes.csv"), CultureInfo.InvariantCulture);
		}
	}


}

