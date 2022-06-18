using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Service;
using WorkService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        var conexao = hostContext.Configuration.GetConnectionString("ApiConnection");
        services.AddDbContext<DataContext>(options =>
                        options.UseSqlite(conexao));

        services.AddScoped<IMoneyService, MoneyService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

