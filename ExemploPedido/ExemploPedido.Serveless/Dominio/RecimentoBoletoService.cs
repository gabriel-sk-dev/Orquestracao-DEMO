using ExemploPedido.Serveless.Dominio.Comandos;
using ExemploPedido.Serveless.Dominio.Infra;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed class RecimentoBoletoService
    {
        private readonly EFContexto _contextoSql;

        public RecimentoBoletoService(EFContexto contextoSql)
        {
            _contextoSql = contextoSql;
        }

        public async Task<Pedido> Executar(BoletoRecebidoComando comando)
        {
            var pedido = await _contextoSql.Pedidos.Include(x=> x.Itens).FirstOrDefaultAsync(x=> x.Id == comando.PedidoId);
            var transacao = Transacao.CriarAprovado(comando.PedidoId);
            pedido.PagamentoAprovado(transacao);
            await _contextoSql.AddAsync(transacao);
            await _contextoSql.SaveChangesAsync();
            return pedido;
        }
    }
}
