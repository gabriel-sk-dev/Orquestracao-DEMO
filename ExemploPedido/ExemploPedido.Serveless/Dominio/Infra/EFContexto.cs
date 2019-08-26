using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExemploPedido.Serveless.Dominio.Infra
{
    public sealed class EFContexto : DbContext
    {
        public EFContexto(DbContextOptions<EFContexto> configuracao)
            : base(configuracao)
        { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Boleto> Boletos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new PedidoItemMap());
            modelBuilder.ApplyConfiguration(new BoletoMap());
            modelBuilder.ApplyConfiguration(new TransacaoMap());
        }
    }

    public sealed class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey("Id");
            builder
                .Property(p => p.Status)
                .HasConversion(new EnumToStringConverter<Pedido.EStatus>());
            builder
                 .HasMany(c => c.Itens)
                 .WithOne()
                 .HasForeignKey("PedidoId")
                 .OnDelete(DeleteBehavior.Cascade)
                 .Metadata
                 .PrincipalToDependent
                 .SetField("_itens");
        }
    }

    public sealed class PedidoItemMap : IEntityTypeConfiguration<Pedido.Item>
    {
        public void Configure(EntityTypeBuilder<Pedido.Item> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey("Id");
        }
    }

    public sealed class BoletoMap : IEntityTypeConfiguration<Boleto>
    {
        public void Configure(EntityTypeBuilder<Boleto> builder)
        {
            builder.HasKey(b => b.NossoNumero);
            builder
                .HasOne(typeof(Pedido))
                .WithMany()
                .HasForeignKey("PedidoId");
        }
    }
    public sealed class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .HasOne(typeof(Pedido))
                .WithMany()
                .HasForeignKey("PedidoId");
        }
    }
}
