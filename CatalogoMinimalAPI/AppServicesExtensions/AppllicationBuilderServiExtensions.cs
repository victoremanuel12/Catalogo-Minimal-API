namespace CatalogoMinimalAPI.AppServicesExtensions;

public static  class AppllicationBuilderServiExtensions
{
    public static IApplicationBuilder useExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment enviromwnt)
    {
        if (enviromwnt.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        return app;

    }
        
    public static IApplicationBuilder useAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
        {
            p.AllowAnyOrigin();
            p.AllowAnyHeader();
            p.WithMethods("GET");
        });
        return app;

    }
    public static IApplicationBuilder  UseSwaggerMiddleware(this IApplicationBuilder app) 
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { });
        return app;
    }
}
