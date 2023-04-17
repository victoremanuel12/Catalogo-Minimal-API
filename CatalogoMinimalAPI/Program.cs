using CatalogoMinimalAPI.ApiEndpoints;
using CatalogoMinimalAPI.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddApiSwegger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();


builder.Services.AddAuthorization();

var app = builder.Build();
app.MapUtenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoint();

app.UseHttpsRedirection();
var enviroment = app.Environment;
app.useExceptionHandling(enviroment)
    .UseSwaggerMiddleware()
    .useAppCors();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
