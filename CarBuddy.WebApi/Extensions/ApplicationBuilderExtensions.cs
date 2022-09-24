using CarBuddy.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CarBuddy.WebApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var context = serviceScope.ServiceProvider.GetService<CarBuddyContext>();
            context.Database.EnsureCreated();
            var hasPendingMigrations = context.Database.GetPendingMigrations().Any();

            if (hasPendingMigrations)
                context.Database.Migrate();
        }
    }
}
