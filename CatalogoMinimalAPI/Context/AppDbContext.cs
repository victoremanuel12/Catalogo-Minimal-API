using CatalogoMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMinimalAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
       public  DbSet<Produto>? Produtos { get; set; }
       public  DbSet<Categoria>? Categorias { get; set; }
        //usado para sobrescrever os metodos padroes do EF de tamanho das string das colunas e int por ex 
        //estamos usando a fluente API usada para configurar as clasees de dominio e susbistituir as convencoes do EF Core
        //a classe ModelBuilder do EF atua como uma Fluent API
        //Permite configurar o Model(schema padrao), o Entity(primaryKey,skema,indes),Porperty(Nome da coluna,valor padrao)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   // esses metodos vão sobrescrever o padrao do EF quando aplicar o migrations 
            modelBuilder.Entity<Categoria>().HasKey(c => c.CategoriaId);
            modelBuilder.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Produto>().HasKey(c => c.ProdutoId);
            modelBuilder.Entity<Produto>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Produto>().Property(c => c.Descricao).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Produto>().Property(c => c.Preco).HasPrecision(14,2).IsRequired();

            //Relacionamento
            modelBuilder.Entity<Produto>()
                .HasOne<Categoria>(c => c.Categoria)
                .WithMany(P => P.Produtos)
                .HasForeignKey(c => c.CategoriaId);


        }
    }
}
