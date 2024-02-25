using Microsoft.EntityFrameworkCore;

namespace SignageLivePlayer.Api.Data.Db;

public static class MigrateDb
{
    public static void Migration(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {

            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (dbContext is not null)
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
