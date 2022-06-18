using Microsoft.EntityFrameworkCore;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Models;
using TesteBackEndCSharp.Service;

namespace TesteBackEndCSharp.Testes
{
    public class DadosMoeda
	{
		/// <summary>
        /// Teste Unitário
        /// </summary>
		[Fact]
		public void AnalisarDadosMoedaGerandoArquivo()
		{
			//Act
			var conexao = "DataSource=../app.db;Cache=Shared";
			var builder = new DbContextOptionsBuilder<DataContext>().UseSqlite(conexao).Options;
			var _context = new DataContext(builder);
			var _service = new MoneyService(_context);
			//Arrange
			var moeda = "USD";
			var cadastroMoedas = new CadastroMoedas();
			var dataInicial = new DateTime(2000, 10, 21);
			var dataFinal = new DateTime(2020, 10, 22);

			var cadastroMoeda = cadastroMoedas.getListaMoedas(moeda);
			var dadosCotacao = _service.lerArquivoDadosCotacao(cadastroMoeda.Codigo, dataInicial, dataFinal.Date);
			var resultado = _service.escreverCsv(dadosCotacao, cadastroMoeda.Moeda);
			//Assert
			Assert.True(resultado == "Arquivo Gerado com Sucesso !!!");
		}
	}
}

