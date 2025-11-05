# minimal-api

Projeto .NET Minimal API com Entity Framework Core, MySQL e Swagger

## Descrição
Este projeto é uma API minimalista desenvolvida em .NET, utilizando o Entity Framework Core para acesso a dados, MySQL como banco de dados e Swagger para documentação interativa. O objetivo é fornecer uma base simples e funcional para autenticação e gerenciamento de administradores.

## Funcionalidades
- Cadastro e autenticação de administradores
- Migrations e versionamento de banco de dados com EF Core
- Configuração de conexão via appsettings.json
- Documentação interativa dos endpoints via Swagger

## Estrutura Principal
- `Program.cs`: Configuração e inicialização da aplicação
- `Infraestrutura/Db/DbContexto.cs`: Contexto do Entity Framework Core, configuração do banco e seed de dados
- `Dominio/Entidades/Administrador.cs`: Entidade Administrador
- `Migrations/`: Migrations geradas pelo EF Core
- `Dominio/ModelViews/Home.cs`: Struct para acesso rápido à URL da documentação

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

## Documentação da API
Ao rodar a aplicação, acesse a documentação interativa pelo navegador:

```
http://localhost:5000/swagger
```
Ou utilize o struct `Home` para obter o caminho da documentação:

```csharp
new Home().Documentacao // retorna "/swagger"
```

## Endpoints principais

- `GET /` — Retorna mensagem de boas-vindas
- `POST /login` — Autenticação de administrador
  - Exemplo de requisição:
    ```json
    {
      "email": "admin@exemplo.com",
      "senha": "123456"
    }
    ```
  - Respostas possíveis:
    - 200 OK: Login bem-sucedido
    - 401 Unauthorized: Credenciais inválidas

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

5. Acesse a documentação interativa em `/swagger` para testar os endpoints.

---

> Projeto criado para fins de estudo e demonstração de arquitetura minimalista com .NET, EF Core, MySQL e Swagger.
