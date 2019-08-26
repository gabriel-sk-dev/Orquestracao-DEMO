using System;
using System.Threading.Tasks;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Comandos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ExemploPedido.Serveless.Functions
{
    public static class Flow_ProcessarPedido
    {
        [FunctionName(nameof(Flow_ProcessarPedido))]
        public static async Task<PedidoEmProcessamento> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context,
            ILogger logger)
        {
            var novoPedidoComando = context.GetInput<NovoPedidoComando>();
            novoPedidoComando.ProcessoId = context.InstanceId;
            var pedidoEmProcessamento = new PedidoEmProcessamento(context.InstanceId, "");
            if (!context.IsReplaying)
                logger.LogInformation($"Processando pedido {pedidoEmProcessamento.Id}");

            var pedido = await context.CallActivityAsync<Pedido>(nameof(Activity_CriarPedido), novoPedidoComando);
            pedidoEmProcessamento.Status = pedido.Status.ToString();
            context.SetCustomStatus(pedidoEmProcessamento.Status);

            if(novoPedidoComando.FormaPagamento.Forma == "Boleto")
            {
                var gerarBoletoComando = new GerarBoletoComando(novoPedidoComando.ProcessoId);
                var boleto = await context.CallActivityAsync<Boleto>(nameof(Activity_GerarBoleto), gerarBoletoComando);
                pedidoEmProcessamento.Status = "AguardandoPagamento";
                context.SetCustomStatus(pedidoEmProcessamento.Status);
                var confirmacaoPagamento = await context.WaitForExternalEvent<string>("PagamentoRealizado", 
                    TimeSpan.FromDays((boleto.Vencimento.AddDays(7) - DateTime.UtcNow).TotalDays), "Negado");
                
                if(confirmacaoPagamento == "Negado")
                {
                    var cancelarPedidoComando = new CancelarPedidoComando(novoPedidoComando.ProcessoId);
                    await context.CallActivityAsync<Boleto>(nameof(Activity_CancelaPedido), cancelarPedidoComando);
                    pedidoEmProcessamento.Status = "Cancelado";
                    return pedidoEmProcessamento;
                }
            }

            if (novoPedidoComando.FormaPagamento.Forma == "Cartao")
            {
                pedidoEmProcessamento.Status = "AguardandoPagamento";
                context.SetCustomStatus(pedidoEmProcessamento.Status);
                var pagamentoCartao = new PagarComCartaoComando(novoPedidoComando.ProcessoId, novoPedidoComando.FormaPagamento.Token);
                var transacao = await context.CallActivityAsync<Transacao>(nameof(Activity_PagarComCartao), pagamentoCartao);                
                if(transacao.Status == Transacao.EStatus.Negado)
                {
                    var confirmacaoPagamento = await context.WaitForExternalEvent<string>("PagamentoAprovado");
                    if (confirmacaoPagamento == "Negado")
                    {
                        var cancelarPedidoComando = new CancelarPedidoComando(novoPedidoComando.ProcessoId);
                        await context.CallActivityAsync<Boleto>(nameof(Activity_CancelaPedido), cancelarPedidoComando);
                        pedidoEmProcessamento.Status = "Cancelado";
                        return pedidoEmProcessamento;
                    }
                }
            }
            context.SetCustomStatus("PagamentoRealizado");
            context.SetCustomStatus(pedidoEmProcessamento.Status);
            return pedidoEmProcessamento;
        }
    }

    public sealed class PedidoEmProcessamento
    {
        public PedidoEmProcessamento(string id, string status)
        {
            Id = id;
            Status = status;
        }

        public string Id { get; set; }
        public string Status { get; set; }
    }
}