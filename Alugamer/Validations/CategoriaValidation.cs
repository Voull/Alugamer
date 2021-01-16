using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Utils;

namespace Alugamer.Validations
{
    public class CategoriaValidation
    {
        private ErroModel erroModel;

        public CategoriaValidation()
        {
            erroModel = new ErroModel();
        }

        public List<string> valida(Categoria categoria)
        {
            List<string> listaErros = new List<string>();

            if(categoria.Id < 0)
                listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Código"));

            if (string.IsNullOrEmpty(categoria.Nome))
                listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));

            if(categoria.Nome.Length > 100)
                listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));

            if (string.IsNullOrEmpty(categoria.Descricao))
                listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Descricao"));

            if (categoria.Descricao.Length > 200)
                listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Descricao"));

            return listaErros;
        }
    }
}
