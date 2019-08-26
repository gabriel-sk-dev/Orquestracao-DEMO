using System;
using System.Collections.Generic;

namespace ExemploPedido.Serveless.Dominio.Comandos
{
    public sealed class NovoPedidoComando
    {
        public string ProcessoId { get; set; }
        public Guid ClienteId { get; set; }
        public List<Item> Itens { get; set; }
        public Pagamento FormaPagamento { get; set; }

        public sealed class Item
        {
            public Guid ProdutoId { get; set; }
            public int Quantidade { get; set; }
            public decimal Valor { get; set; }
        }

        public sealed class Pagamento
        {
            public string Forma { get; set; }
            public string Token { get; set; }
            public string EmailPagador { get; set; }
        }
    }
}
