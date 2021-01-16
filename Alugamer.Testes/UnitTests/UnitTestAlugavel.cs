using System;
using System.Collections.Generic;
using System.Text;
using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;
using Xunit;
using Xunit.Extensions;

namespace Alugamer.Testes.UnitTests
{
    public class UnitTestAlugavel
    {
        private readonly AlugavelValidation alugavelValidation = new AlugavelValidation();
        private readonly ErroModel erroModel = new ErroModel();

        [Fact]
        public void TesteAlugavelVazio()
        {
            Alugavel alugavelVazio = new Alugavel();
            Assert.True(alugavelValidation.validar(alugavelVazio).Count > 0);
        }

        [Fact]
        public void TesteAlugavelValido()
        {
            Alugavel alugavelValido = new Alugavel
            {
                Id = 1,
                Nome = "Nintendo Switch",
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };

            List<string> erros = alugavelValidation.validar(alugavelValido);

            Assert.True(erros.Count == 0);
        }

        [Fact]
        public void TesteAlugavelNomeTamanhoMax()
        {
            Alugavel alugavelValido = new Alugavel
            {
                Id = 1,
                Nome = "Nintendo Switch" + new string('a', 100),
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };
            List<string> erros = alugavelValidation.validar(alugavelValido);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"), erros[0]);

        }

        [Fact]
        public void TesteAlugavelNomeAusente()
        {
            Alugavel alugavelValido = new Alugavel
            {
                Id = 1,
                Nome = string.Empty,
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };

            List<string> erros = alugavelValidation.validar(alugavelValido);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"), erros[0]);

        }


        [Fact]
        public void TesteAlugavelDescricaoTamanhoMax()
        {
            Alugavel alugavelValido = new Alugavel
            {
                Id = 1,
                Nome = "Nintendo Switch",
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho" + new string('a', 100),
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };
            List<string> erros = alugavelValidation.validar(alugavelValido);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_TAMANHO_MAX, "Descriçao"), erros[0]);

        }

        [Fact]
        public void TesteAlugavelDescricaoAusente()
        {
            Alugavel alugavelValido = new Alugavel
            {
                Id = 1,
                Nome = "Nintendo Switch",
                Descricao = string.Empty,
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };

            List<string> erros = alugavelValidation.validar(alugavelValido);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Descrição"), erros[0]);

        }

        [Fact]
        public void TesteAlugavelCategoriaAusente()
        {
            Alugavel alugavelValido = new Alugavel
            {
                Id = 1,
                Nome = "Nintendo Switch",
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 0
            };

            List<string> erros = alugavelValidation.validar(alugavelValido);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Categoria"), erros[0]);

        }

        [Theory, MemberData(nameof(AlugavelQuantidadeInvalida))]
        public void TesteAlugavelQuantidadeInvalida(Alugavel alugavel)
        {
            List<string> erros = alugavelValidation.validar(alugavel);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Quantidade"), erros[0]);
        }

        [Theory, MemberData(nameof(AlugavelValorCompraInvalida))]
        public void TesteAlugavelValorCompraInvalida(Alugavel alugavel)
        {
            List<string> erros = alugavelValidation.validar(alugavel);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Valor Compra"), erros[0]);
        }

        [Theory, MemberData(nameof(AlugavelValorCompraInvalida))]
        public void TesteAlugavelValorAluguelInvalida(Alugavel alugavel)
        {
            List<string> erros = alugavelValidation.validar(alugavel);

            Assert.True(erros.Count == 1);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_INVALIDO, "Valor Compra"), erros[0]);
        }

        public static IEnumerable<object[]> AlugavelQuantidadeInvalida
        {
            get
            {
                return new[]
                {
                //    new object[]
                //    {
                //        new Alugavel
                //        {
                //            Id = 1,
                //            Nome = "Nintendo Switch",
                //            Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                //            Quantidade = 0,
                //            Valor_compra = 2500,
                //            Valor_aluguel = 150,
                //            Categoria = 1
                //        }
                //    },
                    new object[]
                    {
                        new Alugavel
                        {
                            Id = 1,
                            Nome = "Nintendo Switch",
                            Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                            Quantidade = -1,
                            Valor_compra = 2500,
                            Valor_aluguel = 150,
                            IdCategoria = 1
                        }
                    },
            };
            }
        }

        public static IEnumerable<object[]> AlugavelValorCompraInvalida
        {
            get
            {
                return new[]
                {
                    //new object[]
                    //{
                    //    new Alugavel
                    //    {
                    //        Id = 1,
                    //        Nome = "Nintendo Switch",
                    //        Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                    //        Quantidade = 1,
                    //        Valor_compra= 0,
                    //        Valor_aluguel = 150,
                    //        Categoria = 1
                    //    }
                    //},
                    new object[]
                    {
                        new Alugavel
                        {
                            Id = 1,
                            Nome = "Nintendo Switch",
                            Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                            Quantidade = 1,
                            Valor_compra= -1,
                            Valor_aluguel = 150,
                            IdCategoria = 1
                        }
                    },
            };
            }
        }

        public static IEnumerable<object[]> AlugavelValorAluguelInvalida
        {
            get
            {
                return new[]
                {
                //    new object[]
                //    {
                //        new Alugavel
                //        {
                //            Id = 1,
                //            Nome = "Nintendo Switch",
                //            Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                //            Quantidade = 1,
                //            Valor_compra= 2500,
                //            Valor_aluguel = 0,
                //            Categoria = 1
                //        }
                //    },
                new object[]
                    {
                        new Alugavel
                        {
                            Id = 1,
                            Nome = "Nintendo Switch",
                            Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                            Quantidade = 1,
                            Valor_compra= 2500,
                            Valor_aluguel = -1,
                            IdCategoria = 1
                        }
                    },
            };
            }
        }

    }
}
