using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Models;
using TesteBackEndCSharp.Service;

namespace WorkService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;
    private IMoneyService _service;
    private int INTERVALO_PROCESSAMENTO;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        var conexao = _configuration.GetConnectionString("ApiConnection");
        var builder = new DbContextOptionsBuilder<DataContext>().UseSqlite(conexao).Options;
        _context = new DataContext(builder);
        _service = new MoneyService(_context);
        INTERVALO_PROCESSAMENTO = (Int32.Parse(_configuration.GetSection("WorkService").GetSection("IntervaloProcessamentoEmMinutos").Value) * 1000) * 60;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var moneyService = new MoneyService(_context);
            var fila = await moneyService.GetItemFila();
            foreach (var item in fila)
            {
                var moeda = item.Moeda;
                var cadastroMoedas = new CadastroMoedas();
                var cadastroMoeda = cadastroMoedas.getListaMoedas(moeda);
                var dataInicial = item.data_inicio;
                var dataFinal = item.data_fim;
                var dadosCotacao = _service.lerArquivoDadosCotacao(cadastroMoeda.Codigo, dataInicial, dataFinal.Date);
                var resultado = _service.escreverCsv(dadosCotacao, cadastroMoeda.Moeda);
                _logger.LogInformation("---------------------------------------------------------------------------------------------");
                var log = $"--> Nº daFila {item.id} - ";
                log += $"Moeda {item.Moeda} - ";
                log += $"Data {item.data_inicio} - ";
                log += $"Data Fim {item.data_fim} ";

                _logger.LogWarning(log);
                _logger.LogInformation("---------------------------------------------------------------------------------------------");
                _logger.LogCritical(resultado);

            }
            await Task.Delay(this.INTERVALO_PROCESSAMENTO, stoppingToken);
        }
    }
}

