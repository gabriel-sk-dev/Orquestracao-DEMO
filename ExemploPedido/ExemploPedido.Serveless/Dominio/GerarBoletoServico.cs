using ExemploPedido.Serveless.Dominio.Comandos;
using ExemploPedido.Serveless.Dominio.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploPedido.Serveless.Dominio
{
    public sealed class GerarBoletoServico
    {
        private readonly EFContexto _contextoSql;

        public GerarBoletoServico(EFContexto contextoSql)
        {
            _contextoSql = contextoSql;
        }

        public async Task<Boleto> Executar(GerarBoletoComando comando)
        {
            var pedido = await _contextoSql.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == comando.PedidoId);
            var boleto = new Boleto(Guid.NewGuid().ToString(), pedido.Id, pedido.ClienteId, DateTime.UtcNow.AddDays(3), pedido.ValorTotal);
            pedido.AguardandoPagamento();
            await _contextoSql.Boletos.AddAsync(boleto);
            await _contextoSql.SaveChangesAsync();
            return boleto;
        }
    }
}
