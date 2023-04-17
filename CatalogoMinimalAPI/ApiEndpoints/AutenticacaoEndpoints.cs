using CatalogoMinimalAPI.Models;
using CatalogoMinimalAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoMinimalAPI.ApiEndpoints
{
    public static class AutenticacaoEndpoints
    {
        public static void MapUtenticacaoEndpoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel is null)
                {
                    return Results.BadRequest("Login inválido");

                }
                if (userModel.UserName == "victor" && userModel.Password == "Victor@123")
                {
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel
                    );
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login Inválido");
                }
            }).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Autenticacao");
        }

    }
}
