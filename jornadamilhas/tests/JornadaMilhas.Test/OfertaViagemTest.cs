using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemTest
    {
        [Fact]
        public void TestandoOfertaValida()
        {
            // Cen�rio - Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 8));
            double preco = 100.00;
            var validacao = true;

            // A��o - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Valida��o - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }
        
        [Fact]
        public void TestandoOfertaComRotaNula()
        {
            // Cen�rio - Arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 8));
            double preco = 100.00;

            // A��o - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Valida��o - Assert
            Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void TestandoOfertaComPeriodoInvalido()
        {
            // Cen�rio - Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100.0;

            // A��o - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Valida��o - Assert
            Assert.Contains("Erro: Data de ida n�o pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }
    }
}