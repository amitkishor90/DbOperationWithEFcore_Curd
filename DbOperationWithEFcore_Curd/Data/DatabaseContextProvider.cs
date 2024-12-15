using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEFcore_Curd.Data
{
    public class DatabaseContextProvider
    {
        public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Get the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("ConnectionStrings");

            // Register DbContext with SQL Server configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
