using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;

namespace ExemploPedido.Serveless.Functions
{
    public class Activity_ConfirmarRecebimentoBoleto
    {
        private readonly RecimentoBoletoService _servico;

        public Activity_ConfirmarRecebimentoBoleto(RecimentoBoletoService servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(Activity_ConfirmarRecebimentoBoleto))]
        public async Task<Pedido> Run(
            [ActivityTrigger] BoletoRecebidoComando comando,
            ILogger logger)
        {
            logger.LogInformation($"[START ACTIVITY] --> {nameof(Activity_ConfirmarRecebimentoBoleto)} para pedido: {comando.PedidoId}");
            var resultado = await _servico.Executar(comando);
            return resultado;
        }
    }
}
