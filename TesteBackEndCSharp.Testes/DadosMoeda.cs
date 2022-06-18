using Microsoft.EntityFrameworkCore;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Enums;
using TesteBackEndCSharp.Models;
using TesteBackEndCSharp.Service;

namespace TesteBackEndCSharp.Testes
{
    public class DadosMoeda
	{
		Random randNum = new Random();
		/// <summary>
		/// Teste Unitário
		/// </summary>
		//[Fact]
		[Theory]
        #region MassaDeDados
        [InlineData("SGD ")]
        [InlineData("MMK ")]
        [InlineData("VES ")]
        [InlineData("TTD ")]
        [InlineData("AFN ")]
        [InlineData("ZAR ")]
        [InlineData("BYN ")]
        [InlineData("HTG ")]
        [InlineData("NOK ")]
        [InlineData("SOS ")]
        [InlineData("XAU ")]
        [InlineData("UGX ")]
        [InlineData("CDF ")]
        [InlineData("VUV ")]
        [InlineData("BOB ")]
        [InlineData("SZL ")]
        [InlineData("DZD ")]
        [InlineData("MRO ")]
        [InlineData("CRC ")]
        [InlineData("CVE ")]
        [InlineData("XPF ")]
        [InlineData("ALL ")]
        [InlineData("GEL ")]
        [InlineData("SSP ")]
        [InlineData("UYU ")]
        [InlineData("PEN ")]
        [InlineData("SAR ")]
        [InlineData("MZN ")]
        [InlineData("LBP ")]
        [InlineData("MXN ")]
        [InlineData("ILS ")]
        [InlineData("THB ")]
        [InlineData("FJD ")]
        [InlineData("COP ")]
        [InlineData("DJF ")]
        [InlineData("MGA ")]
        [InlineData("OMR ")]
        [InlineData("ANG ")]
        [InlineData("SBD ")]
        [InlineData("ZWL ")]
        [InlineData("XAF ")]
        [InlineData("CZK ")]
        [InlineData("WST ")]
        [InlineData("XDR ")]
        [InlineData("MGB ")]
        [InlineData("CAD ")]
        [InlineData("PHP ")]
        [InlineData("NIO ")]
        [InlineData("GBP ")]
        [InlineData("KES ")]
        [InlineData("EUR ")]
        [InlineData("KMF ")]
        [InlineData("GIP ")]
        [InlineData("IRR ")]
        [InlineData("CLP ")]
        [InlineData("MRU ")]
        [InlineData("XOF ")]
        [InlineData("TRY ")]
        [InlineData("EGP ")]
        [InlineData("ISK ")]
        [InlineData("SLL ")]
        [InlineData("JPY ")]
        [InlineData("CUP ")]
        [InlineData("MUR ")]
        [InlineData("AWG ")]
        [InlineData("RON ")]
        [InlineData("LSL ")]
        [InlineData("ARS ")]
        [InlineData("PGK ")]
        [InlineData("USD ")]
        #endregion
        public void AnalisarDadosMoedaGerandoArquivo(string moeda)
		{
			//Act
			Random num = new Random();
			var dataInicial = new DateTime(num.Next(2000,2022), num.Next(1, 12), num.Next(1, 25));
			var dataFinal = new DateTime(num.Next(2000, 2022), num.Next(1, 12), num.Next(1, 25));
            while (dataFinal.CompareTo(dataInicial) < 0)
            {
                dataFinal = new DateTime(num.Next(2000, 2022), num.Next(1, 12), num.Next(1, 25));
            }
            var conexao = "DataSource=../app.db;Cache=Shared";
			var builder = new DbContextOptionsBuilder<DataContext>().UseSqlite(conexao).Options;
			var _context = new DataContext(builder);
			var _service = new MoneyService(_context);
			//Arrange
			var cadastroMoedas = new CadastroMoedas();
			var cadastroMoeda = cadastroMoedas.getListaMoedas(moeda.Trim());
			var dadosCotacao = _service.lerArquivoDadosCotacao(cadastroMoeda.Codigo, dataInicial, dataFinal);
			var resultado = String.Empty;
			if (cadastroMoeda.Moeda is null)
			{
				resultado = String.Empty;
			} else
			{
				resultado = _service.escreverCsv(dadosCotacao, cadastroMoeda.Moeda);
			}
			//Assert
			Assert.True(resultado == "Arquivo Gerado com Sucesso !!!");
		}
	}
}

