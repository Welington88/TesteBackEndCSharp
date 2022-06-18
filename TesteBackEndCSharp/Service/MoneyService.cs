using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Models;

namespace TesteBackEndCSharp.Service
{
    public class MoneyService : IMoneyService
    {
        private DataContext _context;
        public IConfiguration _configuration { get; set; }  

        public MoneyService(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Pega o ultimo item da fila e processa
        /// </summary>
        /// <returns></returns>
        public async Task<List<Money>> GetItemFila()
        {
            var lista = _context.Money.ToList();
            if (lista.Count() == 0)
            {
                throw new Exception("Não possui filas de consulta a serem processadas....");
            }
            var id  = _context.Money.Max<Money>(m => m.id);
            var resultado = await _context.Money.Where(m => m.id == id).ToListAsync();

            var money = await _context.Money.FindAsync(id);

            _context.Money.Remove(money);
            await _context.SaveChangesAsync();

            return resultado;

        }
        /// <summary>
        /// Adcionar os item da lista a fila
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public async Task<bool> AddItemFila(List<Money> money)
        {

            foreach (var m in money)
            {
                _context.Money.Add(m);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// ler o Arquivo de dados Moeda
        /// </summary>
        /// <param name="codigoMoeda"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DadosMoeda lerArquivoDadosMoeda(string codigoMoeda) {

            var moeda = lerArquivoDadosMoeda().Where(m => m.ID_MOEDA == codigoMoeda).FirstOrDefault();
            if (moeda is null)
            {
                throw new Exception("Moeda não encontrada....");
            }
            return moeda;
        }

        public IEnumerable<DadosMoeda> lerArquivoDadosMoeda()
        {
            string pastaRaiz = caminhoPastaRaiz();
            var path = Path.Combine(pastaRaiz, "TesteBackEndCSharp", "Dados", "DadosMoeda.csv");

            var fi = new FileInfo(path);

            if (!fi.Exists)
            {
                throw new FileNotFoundException($"Arquivo {path} Não Existe...");
            }

            using (var sr = new StreamReader(fi.FullName))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentUICulture)
                {
                    Delimiter = ";"
                };
                var csvReader = new CsvReader(sr, csvConfig);

                var registros = csvReader.GetRecords<DadosMoeda>();

                return registros.ToList();

                foreach (var registro in registros)
                {
                    Console.WriteLine($"cod_cotacao: {registro.ID_MOEDA }");
                    Console.WriteLine($"data cotacao: {registro.DATA_REF }");
                    Console.WriteLine("---------------------------------------");
                }
            }
        }
        public List<DadosCotacao> lerArquivoDadosCotacao(int codMoeda, DateTime dataInicial, DateTime dataFinal) {
            var dadosCotacao = lerArquivoDadosCotacao()
                                .Where(m => m.cod_cotacao == codMoeda
                                            && dataInicial.CompareTo(converterData(m.dat_cotacao)) <= 0
                                            && dataFinal.CompareTo(converterData(m.dat_cotacao)) >= 0
                                       )
                                .ToList();
            if (dadosCotacao is null)
            {
                throw new Exception("Dados não Encontrados");
            }
            return dadosCotacao;
        }

        private DateTime converterData(String data) {
            if (data is null)
            {
                return DateTime.Now;
            }
            var listaDatas = data.Split("/");
            if (listaDatas.Count() >= 3)
            {
                var dia = int.Parse(listaDatas[0]);
                var mes = int.Parse(listaDatas[1]);
                var ano = int.Parse(listaDatas[2].Substring(0,4));
                var dataDT = new DateTime(ano, mes, dia);
                return dataDT;
                
            }
            return DateTime.Now;
        }

        public IEnumerable<DadosCotacao> lerArquivoDadosCotacao()
        {
            string pastaRaiz = caminhoPastaRaiz();

            var path = Path.Combine(pastaRaiz, "TesteBackEndCSharp" , "Dados", "DadosCotacao.csv");

            var fi = new FileInfo(path);

            if (!fi.Exists)
            {
                throw new FileNotFoundException($"Arquivo {path} Não Existe...");
            }

            using (var sr = new StreamReader(fi.FullName))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentUICulture)
                {
                    Delimiter = ";"
                };
                var csvReader = new CsvReader(sr, csvConfig);

                var registros = csvReader.GetRecords<DadosCotacao>();

                return registros.ToList();

                foreach (var registro in registros)
                {
                    Console.WriteLine($"cod_cotacao: {registro.cod_cotacao }");
                    Console.WriteLine($"data cotacao: {registro.dat_cotacao }");
                    Console.WriteLine($"vlr cotacao: {registro.vlr_cotacao }");
                    Console.WriteLine("---------------------------------------");
                }
            }
        }

        public string escreverCsv(List<DadosCotacao> dadosCotacao, string moeda)
        {

            try
            {
                string pastaRaiz = caminhoPastaRaiz();

                var path = Path.Combine(pastaRaiz, "TesteBackEndCSharp", "Exports");

                var lista = new List<DadosMoedaExport>();

                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                var nomeArquivo = DateTime.Now.ToString().Replace("/","").Replace(":","").Replace(" ","_");
                path = Path.Combine(path, $"Resultado_{nomeArquivo}.csv");

                foreach (var item in dadosCotacao)
                {
                    var cotacao = new DadosMoedaExport();
                    var data  = item.dat_cotacao.Split("/");

                    cotacao.ID_MOEDA = moeda;
                    cotacao.VL_COTACAO = item.vlr_cotacao;

                    if (data.Length >= 3)
                    {
                        var dia = int.Parse(data[0]);
                        var mes = int.Parse(data[1]);
                        var ano = int.Parse(data[2]);
                        cotacao.DATA_REF = new DateTime(ano, mes, dia);
                    }
                    else
                    {
                        cotacao.DATA_REF = DateTime.Now;
                    }
                    
                    lista.Add(cotacao);
                }

                using (var sw = new StreamWriter(path))
                {
                    using (var csvWrite = new CsvWriter(sw, CultureInfo.GetCultureInfo("pt-br")))
                    {
                        csvWrite.WriteRecords(lista);
                    }
                }
                return "Arquivo Gerado com Sucesso !!!";
            }
            catch (Exception ex)
            {
                return "Falha ao gerar o  arquivo " + ex.Message;
            }
        }
        private string caminhoPastaRaiz() {

            var matrizPastaRaiz = Environment.CurrentDirectory.Split("/");
            var ttMatriz = matrizPastaRaiz.Count();
            if (ttMatriz <= 1) {
                return Environment.CurrentDirectory;
            }
            string pastaRaiz = "/";
            foreach (var i in matrizPastaRaiz)
            {
                pastaRaiz = Path.Combine(pastaRaiz, i);
                if (i.Equals("TesteBackEndCSharp"))
                {
                    break;
                }
            }

            return pastaRaiz;
        }
    }
}

