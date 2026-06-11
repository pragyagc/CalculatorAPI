using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Register your services
builder.Services.AddScoped<Addition>();
builder.Services.AddScoped<Subtract>();
builder.Services.AddScoped<Multiply>();
builder.Services.AddScoped<Divide>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Calculator API",
        Version = "v1",
        Description = "A simple API for basic math operations"
    });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
