using PaparaBootcamp.RestfulAPI.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using PaparaBootcamp.RestfulAPI.Validations;
using FluentValidation;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseInMemoryDatabase("MyDatabase"));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
