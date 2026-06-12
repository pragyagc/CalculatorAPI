using CalculatorAPI.Services;
using CalculatorAPI.Models;
using CalculatorAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();

//builder.Services.AddScoped<IOperation, Addition>();
//builder.Services.AddScoped<IOperation, Subtract>();
//builder.Services.AddScoped<IOperation, Multiply>();
//builder.Services.AddScoped<IOperation, Divide >();


builder.Services.AddScoped<OperationFactory>();
builder.Services.AddScoped<Calculator>();


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
