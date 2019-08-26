using ExemploPedido.Serveless.Dominio.Comandos;
using ExemploPedido.Serveless.Dominio.Infra;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed class CancelaPedidoServico
    {
        private readonly EFContexto _contextoSql;

        public CancelaPedidoServico(EFContexto contextoSql)
        {
            _contextoSql = contextoSql;
        }

        public async Task<Pedido> Executar(CancelarPedidoComando comando)
        {
            var pedido = await _contextoSql.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == comando.PedidoId);
            pedido.Cancelado();
            await _contextoSql.SaveChangesAsync();
            return pedido;
        }
    }
}
