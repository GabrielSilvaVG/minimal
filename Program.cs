using Microsoft.EntityFrameworkCore;
using minimal.Dominio.DTOs;
using minimal.Infraestrutura.Db;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<DbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    )
);

app.MapGet("/", () => "Olá pessoal");

app.MapPost("/login", (LoginDTO loginDto) =>
{
    if (loginDto.Email == "adm@teste.com" && loginDto.Password == "123456")
        return Results.Ok("Login bem-sucedido");
    else
        return Results.Unauthorized();
});

app.Run();

