namespace ExemploPedido.Serveless.Dominio.Comandos
{
    public sealed class CancelarPedidoComando
    {
        public CancelarPedidoComando(string pedidoId)
        {
            PedidoId = pedidoId;
        }

        public string PedidoId { get; set; }
    }
}
