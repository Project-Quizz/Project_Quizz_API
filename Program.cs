using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project_Quizz_API.Data;
using Project_Quizz_API.Services;
using System.Reflection;
using System.Text;

// Create a web application builder with the specified command-line arguments
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure Swagger for API documentation
builder.Services.AddSwaggerGen(options =>
{
    // Define the information for the API documentation
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v0.5",
        Title = "Project Quiz API",
        Description = "API for our Quiz Project"
    });

    // Add a filter to apply the Api Key header to the Swagger documentation
    options.OperationFilter<SwaggerApiKeayHeader>();

    // Add the XML comments to the Swagger documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Get the connection string from the configuration and add the DbContext to the service container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Build the application
var app = builder.Build();

// Run the database migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Add a middleware to handle authentication for the Swagger endpoint
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        string authHeader = context.Request.Headers["Authorization"];
        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
            var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
            var username = decodedUsernamePassword.Split(':', 2)[0];
            var password = decodedUsernamePassword.Split(':', 2)[1];

            if (username == "admin" && password == "Marine123!") 
            {
                await next();
                return;
            }
        }

        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = 401;
        return;
    }

    await next();
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();
