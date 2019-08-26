namespace ExemploPedido.Serveless.Dominio.Comandos
{
    public sealed class GerarBoletoComando
    {
        public GerarBoletoComando(string pedidoId)
        {
            PedidoId = pedidoId;
        }

        public string PedidoId { get; set; }
    }
}
