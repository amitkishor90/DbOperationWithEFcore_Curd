using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DbOperationWithEFcore_Curd.Data
{
    public class DatabaseContextProvider
    {
        private readonly ConnectionStringProvider _connectionStringProvider;

        public DatabaseContextProvider(ConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public void ConfigureDbContext(IServiceCollection services)
        {
            var connectionString = _connectionStringProvider.GetConnectionString();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
