using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;

namespace ExemploPedido.Serveless.Functions
{
    public class Activity_CancelaPedido
    {
        private readonly CancelaPedidoServico _servico;

        public Activity_CancelaPedido(CancelaPedidoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(Activity_CancelaPedido))]
        public async Task<Pedido> Run(
            [ActivityTrigger] CancelarPedidoComando comando,
            ILogger logger)
        {
            logger.LogInformation($"[START ACTIVITY] --> {nameof(Activity_CancelaPedido)} para pedido: {comando.PedidoId}");
            var resultado = await _servico.Executar(comando);
            return resultado;
        }
    }
}
