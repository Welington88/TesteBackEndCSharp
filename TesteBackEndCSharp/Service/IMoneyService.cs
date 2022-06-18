using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TesteBackEndCSharp.Models;

namespace TesteBackEndCSharp.Service
{
	public interface IMoneyService
	{
		Task<List<Money>> GetItemFila();

		Task<bool> AddItemFila(List<Money> money);

		DadosMoeda lerArquivoDadosMoeda(string codigoMoeda);

		IEnumerable<DadosMoeda> lerArquivoDadosMoeda();

		List<DadosCotacao> lerArquivoDadosCotacao(int codMoeda, DateTime dataInicial, DateTime dataFinal);

		IEnumerable<DadosCotacao> lerArquivoDadosCotacao();

		string escreverCsv(List<DadosCotacao> dadosCotacao, string moeda);

		IConfiguration _configuration { get; set; }
    }
}

