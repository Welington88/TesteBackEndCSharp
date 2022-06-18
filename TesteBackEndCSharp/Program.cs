using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TesteBackEndCSharp.Context;
using TesteBackEndCSharp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexao = builder.Configuration.GetConnectionString("ApiConnection");
builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlite(conexao));

builder.Services.AddScoped<IMoneyService, MoneyService>();
builder.Services.AddTransient<IMoneyService, MoneyService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

