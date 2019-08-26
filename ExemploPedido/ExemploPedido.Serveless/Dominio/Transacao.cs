using System;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed class Transacao
    {
        private Transacao() { }
        private Transacao(Guid id, string pedidoId, EStatus status)
        {
            Id = id;
            PedidoId = pedidoId;
            Status = status;
        }

        public Guid Id { get; private set; }
        public string PedidoId { get; private set; }
        public EStatus Status { get; private set; }

        public static Transacao CriarAprovado(string pedidoId)
            => new Transacao(Guid.NewGuid(), pedidoId, EStatus.Aprovado);

        public static Transacao CriarNegado(string pedidoId)
            => new Transacao(Guid.NewGuid(), pedidoId, EStatus.Negado);

        public enum EStatus
        {
            Aprovado,
            Negado
        }
    }
}
