using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Utils;

namespace Alugamer.Validations
{
    public class AlugavelValidation
    {
		private ErroModel erroModel;

		public AlugavelValidation()
        {
			erroModel = new ErroModel();
		}

		public List<String> validar(Alugavel alugavel)
		{
			List<string> listaErros = new List<string>();

			if (alugavel.Id < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Código"));

			if (string.IsNullOrEmpty(alugavel.Nome))
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));
			else if (alugavel.Nome.Length > 100)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));

			if (string.IsNullOrEmpty(alugavel.Descricao))
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Descricao"));
			else if (alugavel.Descricao.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_TAMANHO_MAX, "Descricao"));

			if (alugavel.Quantidade < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Quantidade"));

			if (alugavel.Valor_aluguel < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Valor para aluguel"));

			if (alugavel.Valor_compra < 0)
				listaErros.Add(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Valor de Compra"));

			return listaErros;
		}
	}
}
