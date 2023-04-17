using CatalogoMinimalAPI.Context;
using CatalogoMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMinimalAPI.ApiEndpoints
{
    public static class CategoriasEndpoints
    {
        // Ao usar a palavra-chave this antes do primeiro parâmetro do método,
        // estamos indicando que o método MapCategoriasEndpoints deve ser tratado como uma extensão da classe WebApplication.
        public static void MapCategoriasEndpoints( this WebApplication app)
        {
            app.MapGet("/categorias", async (AppDbContext db) =>
            {
                return await db.Categorias.ToListAsync();
            }).WithTags("Categorias").RequireAuthorization();
            app.MapGet("categoria/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id) is Categoria categoria ? Results.Ok(categoria) : Results.NotFound();
            });


            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                await db.Categorias.AddAsync(categoria);
                await db.SaveChangesAsync();
                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            });
            app.MapPut("/categoria/{id:int}", async (Categoria categoriaAlterada, int id, AppDbContext db) =>
            {
                if (categoriaAlterada.CategoriaId != id) return Results.BadRequest("Id da categoria não confere com a que foi passado");
                var categotia = await db.Categorias.FindAsync(id);
                if (categotia is null) return Results.NotFound("Categoria não encontrada com esse ID");
                categotia.Nome = categoriaAlterada.Nome;
                categotia.Descricao = categoriaAlterada.Descricao;
                await db.SaveChangesAsync();
                return Results.Ok(categotia);
            });
            app.MapDelete("/categoria/{id}", async (int id, AppDbContext db) =>
            {
                Categoria categoria = await db.Categorias.FindAsync(id);
                if (categoria is null) return Results.NotFound("Categoria não encontrada com esse ID");
                db.Categorias.Remove(categoria);
                await db.SaveChangesAsync();
                return Results.Ok("Categoria Excluída");
            });
        }
    }
}
