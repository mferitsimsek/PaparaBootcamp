using PaparaBootcamp.RestfulAPI.Extensions;
using PaparaBootcamp.RestfulAPI.Extensions.Middleware;
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

//app.UseAuthentication();

app.MapControllers();

app.Run();
