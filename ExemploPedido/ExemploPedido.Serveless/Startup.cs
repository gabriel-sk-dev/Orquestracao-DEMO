using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Infra;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(ExemploPedido.Serveless.Startup))]
namespace ExemploPedido.Serveless
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<EFContexto>(options => options.UseSqlServer(SqlConnection));
            builder.Services.AddScoped<CancelaPedidoServico>();
            builder.Services.AddScoped<CriarNovoPedidoServico>();
            builder.Services.AddScoped<GerarBoletoServico>();
            builder.Services.AddScoped<PagamentoComCartaoServico>();
            builder.Services.AddScoped<RecimentoBoletoService>();
        }
    }
}
