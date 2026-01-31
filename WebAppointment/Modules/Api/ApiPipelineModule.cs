namespace WebApi.Modules.Api
{
    public static class ApiPipelineModule
    {
        public static WebApplication UseApiPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.MapControllers();

            return app;
        }
    }
}
