using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed partial class Pedido
    {
        public sealed class Item
        {
            private Item() { }
            private Item(Guid id, Guid prdutoId, int quantidade, decimal valor)
            {
                Id = id;
                PrdutoId = prdutoId;
                Quantidade = quantidade;
                Valor = valor;
            }

            public Guid Id { get; private set; }
            public Guid PrdutoId { get; private set; }
            public int Quantidade { get; private set; }
            public decimal Valor { get; private set; }
            public decimal ValorTotal => Quantidade * Valor;

            public static Item Criar(Guid produtoId, int quantidade, decimal valor)
                => new Item(Guid.NewGuid(), produtoId, quantidade, valor);
        }
    }
}
