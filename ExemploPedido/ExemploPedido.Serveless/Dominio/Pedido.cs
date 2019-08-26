using System;
using System.Collections.Generic;
using System.Linq;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed partial class Pedido
    {
        private Pedido() { }
        private Pedido(string id, DateTime criadoEm, Guid clienteId, EStatus status, List<Item> itens)
        {
            Id = id;
            CriadoEm = criadoEm;
            ClienteId = clienteId;
            Status = status;
            _itens = itens ?? new List<Item>();
        }

        private ICollection<Item> _itens;

        public string Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public Guid ClienteId { get; private set; }
        public EStatus Status { get; private set; }
        public IEnumerable<Item> Itens => _itens;
        public decimal ValorTotal => _itens.Sum(x => x.ValorTotal);

        internal void AguardandoPagamento()
        {
            Status = EStatus.AguardandoPagamento;
        }

        internal void PagamentoAprovado(Transacao transacao)
        {
            Status = EStatus.Pago;
        }

        internal void Cancelado()
        {
            Status = EStatus.Cancelado;
        }

        public static Pedido Criar(string id, Guid clienteId, List<Item> itens)
            => new Pedido(id, DateTime.UtcNow, clienteId, EStatus.Criado, itens);

        public enum EStatus
        {
            Criado,
            AguardandoPagamento,
            Pago,
            ProntoParaEnvio,
            Enviado,
            Entregue,
            Cancelado
        }
    }
}
