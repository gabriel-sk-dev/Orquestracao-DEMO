using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ExemploPedido.Serveless.Dominio.Infra
{
    public class EFContextoFactory : IDesignTimeDbContextFactory<EFContexto>
    {
        public EFContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFContexto>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new EFContexto(optionsBuilder.Options);
        }
    }
}
