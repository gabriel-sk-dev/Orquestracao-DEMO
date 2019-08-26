using System;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed class Boleto
    {
        public Boleto(string nossoNumero, string pedidoId, Guid clienteId, DateTime vencimento, decimal valor)
        {
            NossoNumero = nossoNumero;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Vencimento = vencimento;
            Valor = valor;
        }

        public string NossoNumero { get; private set; }
        public string PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public DateTime Vencimento { get; private set; }
        public decimal Valor { get; private set; }
    }
}
