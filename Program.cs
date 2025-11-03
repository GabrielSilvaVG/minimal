using minimal.Dominio.DTOs;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Olá pessoal");

app.MapPost("/login", (LoginDTO loginDto) =>
{
    if (loginDto.Email == "adm@teste.com" && loginDto.Password == "123456")
        return Results.Ok("Login bem-sucedido");
    else
        return Results.Unauthorized();
});

app.Run();

