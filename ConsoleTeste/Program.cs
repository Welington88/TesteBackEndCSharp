
using Microsoft.EntityFrameworkCore;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Models;
using TesteBackEndCSharp.Service;

var conexao = "DataSource=../app.db;Cache=Shared";
var builder = new DbContextOptionsBuilder<DataContext>().UseSqlite(conexao).Options;
var _context = new DataContext(builder);
var _service = new MoneyService(_context);

var moeda = "USD";
var cadastroMoedas = new CadastroMoedas();
var cadastroMoeda = cadastroMoedas.getListaMoedas(moeda);
var dataInicial = new DateTime(2000,10,21);
var dataFinal = new DateTime(2020,10,22);
var dadosCotacao = _service.lerArquivoDadosCotacao(cadastroMoeda.Codigo,dataInicial, dataFinal.Date);
var resultado = _service.escreverCsv(dadosCotacao, cadastroMoeda.Moeda);
Console.WriteLine(resultado);

