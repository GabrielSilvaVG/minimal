# minimal-api

Projeto .NET Minimal API com Entity Framework Core e MySQL

## Descrição
Este projeto é uma API minimalista desenvolvida em .NET, utilizando o Entity Framework Core para acesso a dados e MySQL como banco de dados. O objetivo é fornecer uma base simples e funcional para autenticação e gerenciamento de administradores.

## Funcionalidades
- Cadastro e autenticação de administradores
- Migrations e versionamento de banco de dados com EF Core
- Configuração de conexão via appsettings.json

## Estrutura Principal
- `Program.cs`: Configuração e inicialização da aplicação
- `Infraestrutura/Db/DbContexto.cs`: Contexto do Entity Framework Core, configuração do banco e seed de dados
- `Dominio/Entidades/Administrador.cs`: Entidade Administrador
- `Migrations/`: Migrations geradas pelo EF Core

## Configuração do Banco de Dados
No arquivo `appsettings.json`, configure sua string de conexão MySQL:
```json
"ConnectionStrings": {
  "mysql": "server=localhost;database=nome_do_banco;user=root;password=123456"
}
```

## Comandos Úteis
- Aplicar migrations e atualizar banco:
  ```
  dotnet ef database update
  ```
- Gerar nova migration:
  ```
  dotnet ef migrations add NomeDaMigration
  ```

## Requisitos
- .NET 9.0 ou superior
- MySQL Server 8.0 ou superior

## Como rodar
1. Clone o repositório
2. Configure o `appsettings.json` com sua string de conexão
3. Execute as migrations:
   ```
   dotnet ef database update
   ```
4. Rode a aplicação:
   ```
   dotnet run
   ```

---

> Projeto criado para fins de estudo e demonstração de arquitetura minimalista com .NET, EF Core e MySQL.
