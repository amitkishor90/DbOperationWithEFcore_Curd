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

            // Use the DatabaseContextProvider to configure DbContext (AppDbContext)
            builder.Services.AddScoped(serviceProvider =>
            {
                var connectionStringProvider = serviceProvider.GetRequiredService<ConnectionStringProvider>();
                var dbContextProvider = new DatabaseContextProvider(connectionStringProvider);
                dbContextProvider.ConfigureDbContext(builder.Services); // Ensures AppDbContext is registered
                return dbContextProvider;
            });

            // Register AppDbContext here explicitly if it's still missing
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use your connection string

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // Add Swagger for API documentation

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    c.RoutePrefix = string.Empty; // Sets Swagger UI as the app's root
                });
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    c.RoutePrefix = string.Empty; // Sets Swagger UI as the app's root
                });
            }

            app.UseHttpsRedirection();

            // Serve static files from the wwwroot folder
            app.UseStaticFiles();

            // Set default files (e.g., index.html) for root requests
            app.UseDefaultFiles();

            app.UseAuthorization();

            // Map API controllers
            app.MapControllers();

            app.Run();
        }
    }
}
