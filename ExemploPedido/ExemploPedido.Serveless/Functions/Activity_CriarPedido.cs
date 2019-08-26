using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;

namespace ExemploPedido.Serveless.Functions
{
    public class Activity_CriarPedido
    {
        private readonly CriarNovoPedidoServico _servico;

        public Activity_CriarPedido(CriarNovoPedidoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(Activity_CriarPedido))]
        public async Task<Pedido> Run(
            [ActivityTrigger] NovoPedidoComando comando,
            ILogger logger)
        {
            logger.LogInformation($"[START ACTIVITY] --> {nameof(Activity_CriarPedido)} para pedido: {comando.ProcessoId}");
            var resultado = await _servico.Executar(comando);
            return resultado;
        }
    }
}
