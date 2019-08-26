using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;

namespace ExemploPedido.Serveless.Functions
{
    public class TriggerRealizarPedido
    {
        public TriggerRealizarPedido() { }

        [FunctionName(nameof(TriggerRealizarPedido))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Pedidos")] HttpRequest requisicao,
            [OrchestrationClient]DurableOrchestrationClient clienteOrquestracao,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho TriggerRealizarPedido recebido");

            var json = await new StreamReader(requisicao.Body).ReadToEndAsync();
            var comando = JsonConvert.DeserializeObject<NovoPedidoComando>(json);

            var processoId = await clienteOrquestracao.StartNewAsync(nameof(Flow_ProcessarPedido), comando);
            logger.LogInformation($"Pedido recebido e sendo processado pelo ID '{processoId}'.");

            return new OkObjectResult(clienteOrquestracao.CreateHttpManagementPayload(processoId));
        }
    }
}
