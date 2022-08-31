namespace Simon_Test.Application.Extensions;

using Microsoft.EntityFrameworkCore;
using SimonTest.Infrastructure.Persistence;

public static class SeedExtension
{
    public static IApplicationBuilder Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

        dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));

        dbContext.Database.Migrate();

        return app;
    }
}