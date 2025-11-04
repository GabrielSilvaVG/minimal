using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal.Dominio.DTOs;
using minimal.Dominio.Entidades;
using minimal.Dominio.Interfaces;
using minimal.Dominio.ModelViews;
using minimal.Dominio.Servicos;
using minimal.Infraestrutura.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbContexto>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    )
);

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home()));
#endregion

#region Administradores
app.MapPost("/Administradores/login", ([FromBody] LoginDTO loginDto, IAdministradorServico administradorServico) =>//frombody para dizer que o dado vem do corpo da requisição
{
    if (administradorServico.Login(loginDto) != null)
        return Results.Ok("Login bem-sucedido");
    else
        return Results.Unauthorized(); 
});
#endregion

#region Veiculos
app.MapPost("/Veiculos", ([FromBody] VeiculoDTO veiculoDto, IVeiculoServico veiculoServico) =>
{
    var veiculo = new Veiculo
    {
        Nome = veiculoDto.Nome,
        Marca = veiculoDto.Marca,
        Ano = veiculoDto.Ano
    };
    veiculoServico.Incluir(veiculo);
    return Results.Created($"/Veiculos/{veiculo.Id}", veiculo);
});

app.MapGet("/Veiculos", ([FromQuery]int? pagina, IVeiculoServico veiculoServico) =>{
    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);
});
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion

