using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2025-01-01", "2025-02-01", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2025-02-01", "2025-02-08", 100, true)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            // Cenário - Arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            // Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Validação - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }
        
        [Fact]
        public void RetornaErrooSeRotaForNula()
        {
            // Cenário - Arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 8));
            double preco = 100.00;

            // Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Validação - Assert
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaErroSeDataDestinoForInvalido()
        {
            // Cenário - Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100.0;

            // Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Validação - Assert
            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorOuIgualAZero(double preco)
        {
            // Arrange
            Rota rota = new Rota("Origem1", "Destino1");
            Periodo periodo = new Periodo(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));

            // Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            // Arrange
            int quantidadeEsperada = 3;
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 6, 1), new DateTime(2023, 6, 1));
            double preco = -100;

            // Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Assert
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}