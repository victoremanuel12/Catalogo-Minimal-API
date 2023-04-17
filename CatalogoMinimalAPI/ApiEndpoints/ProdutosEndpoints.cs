using CatalogoMinimalAPI.Context;
using CatalogoMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMinimalAPI.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoint(this WebApplication app )
        {
            app.MapGet("/produtos", async (AppDbContext db) =>
            {
                var produtos = await db.Produtos.ToListAsync();
                if (produtos.Count == 0) return Results.NotFound("Nenhum produto foi encontrado!");
                return Results.Ok(produtos);
            }).WithTags("Produtos").RequireAuthorization();
            app.MapGet("produto/porCategoria/{id}", async (int id, AppDbContext db) =>
            {
                var produtos = await db.Categorias.Where(c => c.CategoriaId == id).Select(p => p.Produtos).ToListAsync();
                if (produtos.Count == 0) return Results.NotFound("Nenhum produto encontrado para essa categoria");

                return Results.Ok(produtos);
            });
            app.MapPost("/produto", async (Produto produto, AppDbContext db) =>
            {
                if (produto is null) return Results.BadRequest("Produto não criado");
                await db.Produtos.AddAsync(produto);
                db.SaveChanges();
                return Results.Created($"/produto/{produto.ProdutoId}", produto);
            });
            app.MapPut("/produto/{id:int}", async (int id, Produto produtoAltarado, AppDbContext db) =>
            {
                if (produtoAltarado is null) return Results.NotFound();
                Produto produto = await db.Produtos.FindAsync(id);
                if (produto is null) return Results.NotFound();
                produto.Nome = produtoAltarado.Nome;
                produto.Descricao = produtoAltarado.Descricao;
                produto.Preco = produtoAltarado.Preco;
                produto.DataCompra = produtoAltarado.DataCompra;
                produto.Categoria = produtoAltarado.Categoria;
                produto.Estoque = produtoAltarado.Estoque;
                await db.SaveChangesAsync();
                return Results.Ok(produto);




            });
            app.MapDelete("/produto/{id:int}", async (int id, AppDbContext db) =>
            {
                Produto produto = await db.Produtos.FindAsync(id);
                if (produto is null) return Results.NotFound("Produto não encontrado");
                db.Remove(produto);
                db.SaveChanges();
                return Results.Ok("Produto excluido");
            });

        }
    }
}
