using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Web.Configurations
{
    public static class DatabaseConfigurationExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<ProductDbContext>(
                options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ProductDbContext).GetTypeInfo().Assembly.GetName().Name)));
        }

        public static void ApplyDbMigrations(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                ProductDbContext context = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }
    }
}