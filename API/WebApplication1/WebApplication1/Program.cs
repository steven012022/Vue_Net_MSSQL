using Microsoft.Extensions.Logging;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
DotNetEnv.Env.Load();

// Read DB_PASSWORD from environment variables and ensure it exists
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
if (string.IsNullOrWhiteSpace(dbPassword))
{
    throw new InvalidOperationException(
        "DB_PASSWORD environment variable is missing or empty. " +
        "Ensure the .env file is correctly configured and located in the root directory."
    );
}

// Register services to the container
builder.Services.AddControllers();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
}); // Enable logging services with detailed configuration.

// Configure Swagger/OpenAPI (Remove these lines to disable Swagger completely)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Set up CORS (Cross-Origin Resource Sharing) based on the environment
if (app.Environment.IsDevelopment())
{
    app.UseCors(policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin() // For development: Allow any origin
    );
}
else
{
    app.UseCors(policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("https://your-production-frontend.com") // For production: Restrict to specific origin
    );
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Force HTTPS redirection
app.UseHttpsRedirection();

// Register controller endpoints
app.MapControllers();

// Log startup information
app.Logger.LogInformation("Application is starting...");
app.Logger.LogInformation("Environment: {Environment}", app.Environment.EnvironmentName);

// Ensure the app is running properly
app.Run();
