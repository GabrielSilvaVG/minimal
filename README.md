# minimal-api

<div align="center">
  
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

</div>
Projeto **.NET Minimal API** com **Entity Framework Core**, **MySQL** e **Swagger** — base mínima para autenticação, administração e gerenciamento de veículos.

---

## Sobre

Este repositório contém uma **API minimalista em .NET** construída com **EF Core** para persistência em **MySQL** e documentação via **Swagger**.  

O foco principal é:
- Autenticação de administradores
- Gerenciamento completo de veículos (CRUD)
- Estrutura simples com DTOs, serviços e migrations

---

## ⚙️ Funcionalidades

- 🔐 Autenticação de administradores (endpoint de login)  
- 👥 Cadastro de administradores  
- 🚗 CRUD completo de veículos  
- 🧩 Separação por camadas (DTOs, serviços, contexto EF Core)  
- 🧱 Migrations e seed com administrador inicial  
- 📖 Documentação interativa com Swagger  

---

## 🗂️ Estrutura principal

```
Program.cs                     → configura a aplicação e endpoints
Infraestrutura/Db/DbContexto.cs → DbContext do EF Core + seed inicial
Dominio/Entidades/              → entidades (Administrador.cs, Veiculo.cs)
Dominio/DTOs/                   → DTOs (VeiculoDTO, LoginDTO, AdministradorDTO)
Dominio/Interfaces/             → interfaces de serviço (IVeiculoServico, IAdministradorServico)
Dominio/Servicos/               → implementações dos serviços
Migrations/                     → migrações geradas pelo EF Core
```

---

## 📜 Contrato (rápido)

- **Inputs:** JSON via body (ex.: `VeiculoDTO`, `AdministradorDTO`, `LoginDTO`)  
- **Outputs:** JSON com recurso criado/atualizado ou mensagens de erro  
- **Códigos HTTP:** `200`, `201`, `400`, `401`, `404`

### ⚠️ Edge cases
- Atualizar ou excluir recurso inexistente → **404 Not Found**  
- Requisições sem autenticação → **401 Unauthorized**

---

## 🔐 Autenticação e uso do token

- POST `/login` — Autentica um administrador  

  **Exemplo (`LoginDTO`):**
  ```json
  { "email": "admin@exemplo.com", "senha": "123456" }
  ```

### Conta seed (usuário inicial)
Use esta conta para obter o token no ambiente de desenvolvimento:

```
Email: administrador@teste.com
Senha: 123456
```

Após o login, um token será retornado.  
No Swagger, clique em **“Authorize”** e cole o token no formato **Bearer**:

```
Value: {TOKEN}
```

---

## 🧾 Permissões por perfil (Roles)

### 👑 Administrador (`Adm`)
- Pode criar, ler, atualizar e deletar **administradores e veículos**.

### ✏️ Editor (`Editor`)
- Pode:
  - Criar veículos (`POST /veiculos`)
  - Listar veículos (`GET /veiculos`)
  - Consultar veículo por ID (`GET /veiculos/{id}`)
- Não pode:
  - Criar/modificar/deletar administradores
  - Atualizar ou deletar veículos

As regras estão implementadas em `Program.cs` e nos serviços de `Dominio/Servicos/`.

---

## 👥 Endpoints de Administradores

- **POST /administradores** — Cria um novo administrador  

  **Exemplo (`AdministradorDTO`):**
  ```json
  {
    "email": "novo@exemplo.com",
    "senha": "senhaSegura",
    "perfil": 0
  }
  ```

> O campo `perfil` usa o enum `Perfil` (0 = Adm, 1 = Editor).  
> Veja `Dominio/Enuns/Perfil.cs`.

---

## 🚗 Endpoints de Veículos (CRUD)

- **GET /veiculos** — Lista todos os veículos  
- **GET /veiculos/{id}** — Retorna um veículo por ID  
- **POST /veiculos** — Cria um novo veículo  
- **PUT /veiculos/{id}** — Atualiza um veículo existente  
- **DELETE /veiculos/{id}** — Remove um veículo  

**Exemplo (`VeiculoDTO`):**
```json
{
  "Nome": "Fiat",
  "Marca": "Uno",
  "ano": 2010
}
```

### Respostas comuns
| Código | Significado |
|--------|--------------|
| 200 | OK |
| 201 | Created |
| 400 | Bad Request |
| 401 | Unauthorized |
| 404 | Not Found |

---

## Quick Start

1. **Instale o .NET SDK** e o **MySQL/MariaDB**.
2. **Configure a connection string** em `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "mysql": "server=localhost;database=nome_do_banco;user=root;password=123456"
   }
   ```
3. **Instale a ferramenta `dotnet-ef`**:
   ```pwsh
   dotnet tool install --global dotnet-ef
   ```
4. **Aplique as migrations e rode o projeto:**
   ```pwsh
   dotnet ef database update
   dotnet run
   ```
5. **Acesse o Swagger:**  
   👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

> 💡 A porta pode variar conforme o `launchSettings.json`.  
> Verifique o console após executar `dotnet run`.

---

## Dicas de troubleshooting

- Verifique se o MySQL/MariaDB está rodando e acessível.
- Confira se as credenciais do `appsettings.json` estão corretas.
- Execute os comandos `dotnet ef` no diretório do `.csproj` ou use `--project`/`--startup-project`.

---

## Observações arquiteturais

- Os endpoints da **Minimal API** delegam lógica aos serviços em `Dominio/Servicos/`.
- **DTOs** são usados para validação e transporte de dados.
- **Entidades** representam a persistência no banco.
- **Migrations** versionam o schema e populam o administrador inicial.

---

## 📁 Arquivos úteis

- `Program.cs` — mapeamento de endpoints e middleware  
- `Infraestrutura/Db/DbContexto.cs` — configuração do EF Core e seed  
- `Dominio/DTOs/` — payloads aceitos  
- `Migrations/` — histórico do schema

---

**2025 • Projeto minimal-api**
