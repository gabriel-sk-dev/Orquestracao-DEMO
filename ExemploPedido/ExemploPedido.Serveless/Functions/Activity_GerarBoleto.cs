using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;

namespace ExemploPedido.Serveless.Functions
{
    public class Activity_GerarBoleto
    {
        private readonly GerarBoletoServico _servico;

        public Activity_GerarBoleto(GerarBoletoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(Activity_GerarBoleto))]
        public async Task<Boleto> Run(
            [ActivityTrigger] GerarBoletoComando comando,
            ILogger logger)
        {
            logger.LogInformation($"[START ACTIVITY] --> {nameof(Activity_GerarBoleto)} para pedido: {comando.PedidoId}");
            var resultado = await _servico.Executar(comando);
            return resultado;
        }
    }
}
