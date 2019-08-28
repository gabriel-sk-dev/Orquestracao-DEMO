namespace ExemploPedido.Serveless.Dominio.Comandos
{
    public sealed class BoletoRecebidoComando
    {
        public BoletoRecebidoComando(string nossoNumero, string pedidoId)
        {
            NossoNumero = nossoNumero;
            PedidoId = pedidoId;
        }

        public string PedidoId { get; set; }
        public string NossoNumero { get; set; }
    }
}
