using CalculatorAPI.Data;
using CalculatorAPI.Models;
using CalculatorAPI.Services;
using CalculatorAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Scrutor;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);


// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();

//builder.Services.AddScoped<IOperation, Addition>();
//builder.Services.AddScoped<IOperation, Subtract>();
//builder.Services.AddScoped<IOperation, Multiply>();
//builder.Services.AddScoped<IOperation, Divide >();


builder.Services.AddScoped<OperationFactory>();


// Register DbContext with PostgreSQL
builder.Services.AddDbContext<CalculatorDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Calculator_db")));

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


//builder.Services.AddScoped<Calculator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5174") // React dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



var app = builder.Build();

// Use CORS before mapping controllers
app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator API v1");
            c.DocExpansion(DocExpansion.None); // collapse endpoints by default
            c.DefaultModelsExpandDepth(-1);    // hide schemas section
        }
        );
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CalculatorDbContext>();
    db.Database.EnsureCreated();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
