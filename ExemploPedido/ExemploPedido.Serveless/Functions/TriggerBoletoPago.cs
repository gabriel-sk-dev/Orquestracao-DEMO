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
using ExemploPedido.Serveless.Dominio.Infra;

namespace ExemploPedido.Serveless.Functions
{
    public class TriggerBoletoPago
    {
        private readonly EFContexto _contexto;

        public TriggerBoletoPago(EFContexto contexto)
        {
            _contexto = contexto;
        }

        [FunctionName(nameof(TriggerBoletoPago))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Boletos/{nossoNumero}")] HttpRequest requisicao,
            string nossoNumero,
            [OrchestrationClient]DurableOrchestrationClient clienteOrquestracao,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho TriggerBoletoPago recebido");

            var boleto = await _contexto.Boletos.FindAsync(nossoNumero);
            if (boleto == null)
                return new NotFoundResult();
            await clienteOrquestracao.RaiseEventAsync(boleto.PedidoId, "PagamentoRealizado", "Aprovado");

            return new OkResult();
        }
    }
}
