﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Utils;

namespace Alugamer.Validations
{
    public class AluguelValidation
    {
		private ErroModel erroModel;

		public AluguelValidation()
        {
			erroModel = new ErroModel();
		}

		public List<String> validar(Aluguel aluguel)
		{
			List<string> listaErros = new List<string>();

			if (aluguel.Locatario < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Cliente"));

			if (aluguel.Vendedor < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Vendedor"));

			if (aluguel.Valor_total < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Valor Total"));

			if (aluguel.Valor_desconto <= 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Valor Desconto"));

			 if (aluguel.DataInicial > DateTime.Today)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Data Inicial"));
			
			if (aluguel.DataFinal < DateTime.Today)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Data Final"));


			foreach (ItemAluguel item in aluguel.Itens)
            {
				if (item.IdItem < 0)
					listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Id de um Item Alugado"));
				if (item.Quantidade < 0)
					listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Quantidade de um Item Alugado"));
				if (item.Valor < 0)
					listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Valor de um Item Alugado"));
			}

			return listaErros;
		}
	}
}
