using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal.Dominio.DTOs;
using minimal.Dominio.Interfaces;
using minimal.Dominio.Servicos;
using minimal.Infraestrutura.Db;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

builder.Services.AddDbContext<DbContexto>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    )
);

var app = builder.Build();

app.MapGet("/", () => "Olá pessoal");

app.MapPost("/login", ([FromBody] LoginDTO loginDto, IAdministradorServico administradorServico) =>//frombody para dizer que o dado vem do corpo da requisição
{
    if (administradorServico.Login(loginDto) != null)
        return Results.Ok("Login bem-sucedido");
    else
        return Results.Unauthorized(); 
});

app.Run();

