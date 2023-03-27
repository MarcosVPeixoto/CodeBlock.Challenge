using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CodeBlock.Challenge.Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }
        public DbSet<Carga> Cargas{ get; set; }
        public DbSet<OperacaoCargueiro> OperacaoCargueiro{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CargaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OperacaoCargueiroEntityTypeConfiguration());
        }
    }
}
