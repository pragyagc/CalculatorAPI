using FluentValidation;
using FluentValidation.AspNetCore;
using CalculatorAPI.Validators;
using CalculatorAPI.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ✅ Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();

// ✅ Register your math services
builder.Services.AddScoped<Addition>();
builder.Services.AddScoped<Subtract>();
builder.Services.AddScoped<Multiply>();
builder.Services.AddScoped<Divide>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
