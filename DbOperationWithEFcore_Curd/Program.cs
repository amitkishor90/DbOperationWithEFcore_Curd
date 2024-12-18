
using DbOperationWithEFcore_Curd.Data;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEFcore_Curd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services for ConnectionStringProvider and DatabaseContextProvider
            builder.Services.AddSingleton<ConnectionStringProvider>();
            builder.Services.AddScoped<DatabaseContextProvider>();
            // Use the DatabaseContextProvider to configure DbContext
            builder.Services.AddScoped(serviceProvider =>
            {
                var connectionStringProvider = serviceProvider.GetRequiredService<ConnectionStringProvider>();
                var dbContextProvider = new DatabaseContextProvider(connectionStringProvider);
                dbContextProvider.ConfigureDbContext(builder.Services);
                return dbContextProvider;
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
