using Api.WeChip.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.WeChip.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoTipo> ProdutoTipos { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<OfertaProduto> OfertaProdutos { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(entity =>
                    {
                        entity.HasOne(p => p.Tipo).WithMany(pt => pt.Produto)
                        .HasForeignKey(pt => pt.TipoId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Produtos_ProdutoTipos_TipoId");
                    });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasOne(p => p.Status).WithMany(pt => pt.Cliente)
                .HasForeignKey(pt => pt.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Status_StatusId");
            });
        }
    }
}
