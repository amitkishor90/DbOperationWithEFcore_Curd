using DbOperationWithEFcore_Curd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services for ConnectionStringProvider and DatabaseContextProvider
builder.Services.AddSingleton<ConnectionStringProvider>();
builder.Services.AddScoped<DatabaseContextProvider>();

// Register the AppDbContext with the DI container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Register Swagger services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DbOperationWithEFcore API",
        Version = "v1",
        Description = "A simple API to demonstrate DB operations with EF Core."
    });
});

var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DbOperationWithEFcore API v1");
    c.RoutePrefix = string.Empty;  // Makes Swagger UI the root page
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
