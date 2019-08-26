using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;

namespace ExemploPedido.Serveless.Functions
{
    public class Activity_PagarComCartao
    {
        private readonly PagamentoComCartaoServico _servico;

        public Activity_PagarComCartao(PagamentoComCartaoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(Activity_PagarComCartao))]
        public async Task<Transacao> Run(
            [ActivityTrigger] PagarComCartaoComando comando,
            ILogger logger)
        {
            logger.LogInformation($"[START ACTIVITY] --> {nameof(Activity_CancelaPedido)} para pedido: {comando.PedidoId}");
            var resultado = await _servico.Executar(comando);
            return resultado;
        }
    }
}
