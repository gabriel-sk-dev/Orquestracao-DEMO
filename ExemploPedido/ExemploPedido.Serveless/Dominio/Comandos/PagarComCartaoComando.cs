namespace ExemploPedido.Serveless.Dominio.Comandos
{
    public sealed class PagarComCartaoComando
    {
        public PagarComCartaoComando(string pedidoId, string token)
        {
            PedidoId = pedidoId;
            Token = token;
        }

        public string PedidoId { get; set; }
        public string Token { get; set; }
    }
}
