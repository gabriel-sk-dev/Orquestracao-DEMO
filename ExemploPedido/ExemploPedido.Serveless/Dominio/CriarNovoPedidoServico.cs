using ExemploPedido.Serveless.Dominio.Comandos;
using ExemploPedido.Serveless.Dominio.Infra;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed class CriarNovoPedidoServico
    {
        private readonly EFContexto _contextoSql;

        public CriarNovoPedidoServico(EFContexto contextoSql)
        {
            _contextoSql = contextoSql;
        }

        public async Task<Pedido> Executar(NovoPedidoComando comando)
        {
            var pedido = Pedido.Criar(
                comando.ProcessoId,
                comando.ClienteId,
                comando.Itens.Select(i => Pedido.Item.Criar(i.ProdutoId, i.Quantidade, i.Valor)).ToList());
            await _contextoSql.Pedidos.AddAsync(pedido);
            await _contextoSql.SaveChangesAsync();
            return pedido;
        }
    }
}
