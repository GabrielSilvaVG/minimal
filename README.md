# minimal-api

Projeto .NET Minimal API com Entity Framework Core, MySQL e Swagger

## Descrição
Este projeto é uma API minimalista desenvolvida em .NET, utilizando o Entity Framework Core para acesso a dados, MySQL como banco de dados e Swagger para documentação interativa. O objetivo é fornecer uma base simples e funcional para autenticação e gerenciamento de administradores.

## Funcionalidades
- Cadastro e autenticação de administradores
- Migrations e versionamento de banco de dados com EF Core
- Configuração de conexão via appsettings.json
- Documentação interativa dos endpoints via Swagger
# minimal-api

Projeto .NET Minimal API com Entity Framework Core, MySQL e Swagger — base mínima para autenticação, administração e gerenciamento de veículos.

## Descrição
Esta é uma API minimalista em .NET (Minimal API) construída com EF Core para persistência em MySQL e documentação via Swagger. Além das funcionalidades iniciais de autenticação e gerenciamento de administradores, o projeto agora inclui gerenciamento completo de veículos (CRUD), DTOs e serviços dedicados.

## Novas funcionalidades adicionadas
- CRUD completo de veículos (rota, serviços e DTOs)
- DTOs para transferência de dados (`VeiculoDTO`, `LoginDTO`, etc.)
- Serviços e interfaces: `IVeiculoServico`, `VeiculoServico` (separação de responsabilidades)
- Seed de dados e migrations atualizadas para Administrador e Veículos
- Endpoints autenticados para ações administrativas

## Funcionalidades (resumo)
- Cadastro e autenticação de administradores
- Gerenciamento de veículos (criar, listar, obter por id, atualizar, excluir)
- Migrations e versionamento de banco de dados com EF Core
- Configuração de conexão via `appsettings.json`
- Documentação interativa dos endpoints via Swagger

## Estrutura Principal
- `Program.cs`: configuração da aplicação e mapeamento dos endpoints
- `Infraestrutura/Db/DbContexto.cs`: contexto do EF Core e seed
- `Dominio/Entidades/`: entidades (ex.: `Administrador.cs`, `Veiculo.cs`)
- `Dominio/DTOs/`: DTOs (ex.: `VeiculoDTO.cs`, `LoginDTO.cs`, `AdministradorDTO.cs`)
- `Dominio/Interfaces/`: interfaces de serviço (ex.: `IVeiculoServico.cs`, `IAdministradorServico.cs`)
- `Dominio/Servicos/`: implementações dos serviços (ex.: `VeiculoServico.cs`, `AdministradorServico.cs`)
- `Migrations/`: migrations geradas pelo EF Core

## Endpoints principais (novos e existentes)

Autenticação / Administrador
- `POST /login` — Autentica um administrador e retorna token/resultado
  - Exemplo de requisição:
    ```json
    { "email": "admin@exemplo.com", "senha": "123456" }
    ```

Administradores
- `POST /administradores` — Cria um novo administrador
  - Observação importante: o campo `Perfil` utiliza o enum `Perfil` do sistema. Envie o valor numérico no payload: `0` para `Adm` e `1` para `Editor`.
  - Exemplo de requisição (`AdministradorDTO`):
    ```json
    {
      "email": "novo@exemplo.com",
      "senha": "senhaSegura",
      "perfil": 0
    }
    ```

Veículos (CRUD)
- `GET /veiculos` — Lista todos os veículos
- `GET /veiculos/{id}` — Retorna um veículo por id
- `POST /veiculos` — Cria um novo veículo
  - Exemplo payload (`VeiculoDTO`):
    ```json
    {
      "placa": "ABC1234",
      "marca": "Fiat",
      "modelo": "Uno",
      "ano": 2010
    }
    ```
- `PUT /veiculos/{id}` — Atualiza um veículo existente
- `DELETE /veiculos/{id}` — Remove um veículo

Respostas HTTP comuns:
- 200 OK — Sucesso
- 201 Created — Recurso criado (POST)
- 400 Bad Request — Validação falhou
- 401 Unauthorized — Requisição não autenticada
- 404 Not Found — Recurso não encontrado

## Configuração do Banco de Dados
No arquivo `appsettings.json`, configure sua string de conexão MySQL:
```json
"ConnectionStrings": {
  "mysql": "server=localhost;database=nome_do_banco;user=root;password=123456"
}
```

## Seed e migrations
O projeto inclui migrations para Administrador e Veículos. Há também um seed inicial para criar um administrador padrão (ver `Migrations/` e `Infraestrutura/Db/DbContexto.cs`).

Comandos úteis:
- Aplicar migrations e atualizar banco:
```pwsh
dotnet ef database update
```
- Gerar nova migration:
```pwsh
dotnet ef migrations add NomeDaMigration
```

## Como rodar
1. Clone o repositório
2. Configure o `appsettings.json` com sua string de conexão
3. Execute as migrations e o seed:
   ```pwsh
   dotnet ef database update
   ```
4. Rode a aplicação:
   ```pwsh
   dotnet run
   ```
5. Acesse a documentação interativa (Swagger):
   ```
   http://localhost:5000/swagger
   ```

## Observações arquiteturais
- Segregação de responsabilidades: controllers/minimal endpoints apenas delegam para serviços em `Dominio/Servicos/`.
- DTOs são usados para validação e transferência; as entidades do domínio representam a persistência.

---

> Projeto atualizado: adicionadas funcionalidades de gerenciamento de veículos, DTOs e serviços. Consulte as rotas em `/swagger` para exemplos interativos.
