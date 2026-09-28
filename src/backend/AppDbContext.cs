using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("Produtos");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                      .UseIdentityColumn(); 
            
            entity.Property(c => c.Nome)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(c => c.Preco)
                  .HasColumnType("decimal(10,2)")
                  .HasPrecision(18, 2)
                  .IsRequired();

            entity.Property(c => c.Categoria)
                   .HasMaxLength(100)
                   .IsRequired();

            entity.Property(c => c.Estoque)
                   .IsRequired();
        });

        // Seed inicial
        modelBuilder.Entity<Produto>().HasData(
            new { Id = 1, Nome = "Mouse Gamer RGB", Preco = 120.90m, Categoria = "Periféricos", Estoque = 15 },
            new { Id = 2, Nome = "Teclado Mecânico Redragon", Preco = 289.00m, Categoria = "Periféricos", Estoque = 8 },
            new { Id = 3, Nome = "Monitor 27'' 144hz", Preco = 1499.90m, Categoria = "Monitores", Estoque = 5 },
            new { Id = 4, Nome = "Notebook i7 16GB 512SSD", Preco = 4899.90m, Categoria = "Hardware", Estoque = 3 },
            new { Id = 5, Nome = "Headset HyperX Cloud", Preco = 399.90m, Categoria = "Headset", Estoque = 10 },
            new { Id = 6, Nome = "Placa de Vídeo RTX 4060 8GB", Preco = 2399.00m, Categoria = "Hardware", Estoque = 4 },
            new { Id = 7, Nome = "SSD NVMe 1TB Gen4", Preco = 499.90m, Categoria = "Armazenamento", Estoque = 12 },
            new { Id = 8, Nome = "Roteador Wi-Fi 6 AX1800", Preco = 349.00m, Categoria = "Redes", Estoque = 9 },
            new { Id = 9, Nome = "Cadeira Gamer Ergonômica", Preco = 899.90m, Categoria = "Acessórios", Estoque = 6 },
            new { Id = 10, Nome = "Webcam Full HD 1080p", Preco = 189.00m, Categoria = "Periféricos", Estoque = 14 },
            new { Id = 11, Nome = "Mesa Gamer", Preco = 1200.00m, Categoria = "Acessórios", Estoque = 5 },
            new { Id = 12, Nome = "Computador Gamer", Preco = 7200.00m, Categoria = "Hardware", Estoque = 3 }
        );

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("Pedidos");

            entity.HasKey(p => p.Id);
     
            entity.Property(p => p.Status)
                   .HasMaxLength(50)
                   .IsRequired();
     
            entity.Property(p => p.DescontoPercentual)
                  .HasColumnType("decimal(10,2)");

            entity.HasMany(p => p.Itens)
                   .WithOne(i => i.Pedido)
                   .HasForeignKey(i => i.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ItemPedido>(entity =>
        {
            entity.ToTable("ItensPedido");

            entity.HasKey(i => i.Id);
            entity.Property(c => c.Id)
                      .UseIdentityColumn(); 

            entity.Property(i => i.Nome)
                  .HasMaxLength(40)
                  .IsRequired();

            entity.Property(i => i.Preco)
                  .HasColumnType("decimal(10,2)")
                  .HasPrecision(18, 2)
                  .IsRequired();
        });


    }
}