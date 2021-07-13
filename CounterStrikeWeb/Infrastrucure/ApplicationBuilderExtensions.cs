namespace CounterStrikeWeb.Infrastrucure
{
    using CounterStrikeWeb.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServises = app.ApplicationServices.CreateScope();

            var data = scopedServises.ServiceProvider.GetService<CounterStrikeDbContext>();

            data.Database.Migrate();

            return app;
        }
    }
}
