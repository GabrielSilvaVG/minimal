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
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administradores
app.MapPost("/Administradores/login", ([FromBody] LoginDTO loginDto, IAdministradorServico administradorServico) =>//frombody para dizer que o dado vem do corpo da requisição
{
    if (administradorServico.Login(loginDto) != null)
        return Results.Ok("Login bem-sucedido");
    else
        return Results.Unauthorized(); 
}).WithTags("Administradores");
#endregion

#region Veiculos
static ErrosDeValidacao ValidaDTO(VeiculoDTO veiculoDto)
{
    var validacao = new ErrosDeValidacao
    {
        Mensagens = []
    };
    if (string.IsNullOrEmpty(veiculoDto.Nome))
        validacao.Mensagens.Add("O nome do veículo não pode ser vazio.");
    if (string.IsNullOrEmpty(veiculoDto.Marca))
        validacao.Mensagens.Add("A marca do veículo não pode ser vazia.");
    if (veiculoDto.Ano < 1886 || veiculoDto.Ano > DateTime.Now.Year + 1)
        validacao.Mensagens.Add("O ano do veículo é inválido.");
    return validacao;
}

app.MapPost("/Veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var validacao = ValidaDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };
    veiculoServico.Incluir(veiculo);
    return Results.Created($"/Veiculos/{veiculo.Id}", veiculo);
}).WithTags("Veiculos");

app.MapGet("/Veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{
    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);
}).WithTags("Veiculos");

app.MapGet("/Veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{

    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapPut("/Veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();

    var validacao = ValidaDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculo);
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapDelete("/Veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{

    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();

    veiculoServico.Apagar(veiculo);
    return Results.NoContent();
}).WithTags("Veiculos");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion

