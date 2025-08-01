# 🔧 HyperEfficient Backend API

<div align="center">

**API REST para Sistema de Gestão Energética com Integração IoT**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql)](https://www.mysql.com/)
[![Dapper](https://img.shields.io/badge/Dapper-1.60.0-FF6B6B?style=for-the-badge)](https://dapperlib.github.io/Dapper/)
[![JWT](https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=json-web-tokens)](https://jwt.io/)

[![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Produção-brightgreen?style=for-the-badge)](https://github.com/seu-usuario/HyperEfficient)

</div>

---

## 📋 Índice

- [🎯 Sobre o Backend](#-sobre-o-backend)
- [🏗️ Arquitetura](#️-arquitetura)
- [🛠️ Tecnologias](#️-tecnologias)
- [📦 Instalação](#-instalação)
- [🔧 Configuração](#-configuração)
- [🚀 Execução](#-execução)
- [📚 Documentação da API](#-documentação-da-api)
- [🔌 Integração IoT](#-integração-iot)
- [🔐 Segurança](#-segurança)
- [📊 Banco de Dados](#-banco-de-dados)
- [🤝 Contribuição](#-contribuição)

---

## 🎯 Sobre o Backend

O backend do **HyperEfficient** é uma API REST robusta desenvolvida em ASP.NET Core 8.0, seguindo os princípios de Domain Driven Design (DDD). O sistema oferece funcionalidades completas para gestão energética empresarial, incluindo integração com dispositivos IoT da plataforma Tuya.

### ✨ Características Principais

- 🔌 **Integração IoT**: API Tuya para dispositivos inteligentes
- 🔐 **Autenticação JWT**: Sistema seguro de autenticação
- 📊 **Relatórios Avançados**: Cálculos de consumo energético
- 🤖 **Background Services**: Atualizações automáticas
- 🏗️ **Arquitetura DDD**: Organização por domínio
- 📈 **Performance**: Otimizado com Dapper e MySQL

---

## 🏗️ Arquitetura

### Estrutura de Pastas

```
HyperEfficient/
├── Controllers/              # Endpoints da API REST
│   ├── UsuarioController.cs
│   ├── CategoriaController.cs
│   ├── EquipamentoController.cs
│   ├── SetorController.cs
│   ├── RegistroController.cs
│   └── RelatorioController.cs
├── Services/                 # Lógica de negócio
│   ├── UsuarioService.cs
│   ├── CategoriaService.cs
│   ├── EquipamentoService.cs
│   ├── SetorService.cs
│   ├── RegistroService.cs
│   ├── RelatorioService.cs
│   ├── TuyaApiClientService.cs
│   └── BackgroundServices/
│       └── TuyaUpdateService.cs
├── Repositories/             # Acesso a dados
│   ├── UsuarioRepository.cs
│   ├── CategoriaRepository.cs
│   ├── EquipamentoRepository.cs
│   ├── SetorRepository.cs
│   ├── RegistroRepository.cs
│   └── RelatorioRepository.cs
├── Entities/                 # Modelos de domínio
│   ├── Base/
│   │   └── EntityBase.cs
│   ├── UsuarioEntity.cs
│   ├── CategoriaEntity.cs
│   ├── EquipamentoEntity.cs
│   ├── SetorEntity.cs
│   └── RegistroEntity.cs
├── DTOs/                     # Objetos de transferência
│   ├── Base/
│   │   ├── BaseDto.cs
│   │   └── GetAllResponseBase.cs
│   ├── Usuario/
│   ├── Categoria/
│   ├── Equipamento/
│   ├── Setor/
│   ├── Registro/
│   └── Relatorios/
├── Contracts/                # Interfaces e contratos
│   ├── Infrastructure/
│   ├── Repositories/
│   └── Services/
├── Infrastructure/           # Infraestrutura técnica
│   ├── Authentication/
│   ├── Connection/
│   ├── Criptography/
│   ├── DatabaseInitializer/
│   ├── Extensions/
│   ├── Mapping/
│   └── Middleware/
├── appsettings.json          # Configurações
├── Program.cs               # Ponto de entrada
└── CreateSQL.txt           # Script de criação do banco
```

### Padrões Arquiteturais

- **Domain Driven Design (DDD)**: Organização por domínio de negócio
- **Repository Pattern**: Abstração do acesso a dados
- **Service Layer**: Lógica de negócio centralizada
- **Dependency Injection**: Inversão de controle
- **CQRS**: Separação de comandos e consultas

---

## 🛠️ Tecnologias

### Core Framework

- **ASP.NET Core 8.0** - Framework web moderno e performático
- **C# 12.0** - Linguagem de programação com recursos avançados
- **Entity Framework Core** - ORM para mapeamento de entidades

### Banco de Dados

- **MySQL 8.0+** - Banco de dados relacional robusto
- **Dapper** - Micro-ORM para consultas SQL nativas
- **MySQL.Data** - Driver oficial do MySQL

### Autenticação e Segurança

- **JWT (JSON Web Tokens)** - Autenticação stateless
- **PBKDF2** - Criptografia de senhas
- **HMAC-SHA256** - Assinatura para API Tuya

### Mapeamento e Validação

- **AutoMapper** - Mapeamento entre objetos
- **Data Annotations** - Validação de modelos
- **FluentValidation** - Validações customizadas

### Integração IoT

- **Tuya API** - Plataforma IoT para dispositivos inteligentes
- **HttpClient** - Cliente HTTP para APIs externas
- **Background Services** - Tarefas em background

### Logging e Monitoramento

- **ILogger** - Sistema de logs integrado
- **Serilog** - Logging estruturado
- **Health Checks** - Monitoramento de saúde

---

## 📦 Instalação

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8.0+](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### 1. Clone o Repositório

```bash
git clone https://github.com/seu-usuario/HyperEfficient.git
cd HyperEfficient/Backend
```

### 2. Configuração do Banco de Dados

```sql
-- Crie o banco de dados
CREATE DATABASE hyperefficient CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Execute o script de criação das tabelas
-- Arquivo: CreateSQL.txt
```

### 3. Restauração de Dependências

```bash
dotnet restore
```

### 4. Configuração do Ambiente

```bash
# Copie o arquivo de configuração
cp appsettings.Development.json appsettings.json

# Edite as configurações
# - String de conexão do banco
# - Chave secreta do JWT
# - Credenciais da API Tuya
```

---

## 🔧 Configuração

### appsettings.json

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Port=3306;Database=hyperefficient;User=root;Password=sua_senha;CharSet=utf8mb4;"
  },
  "JwtSettings": {
    "SecretKey": "sua_chave_secreta_aqui_minimo_32_caracteres",
    "Issuer": "HyperEfficient",
    "Audience": "HyperEfficientUsers",
    "ExpirationHours": 24,
    "RememberMeExpirationDays": 30
  },
  "TuyaApi": {
    "ClientId": "seu_client_id_tuya",
    "ClientSecret": "seu_client_secret_tuya",
    "BaseUrl": "https://openapi.tuyaeu.com",
    "Region": "eu"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Variáveis de Ambiente (Opcional)

```bash
# .env ou variáveis do sistema
HYPER_EFFICIENT_DB_CONNECTION="Server=localhost;Database=hyperefficient;..."
HYPER_EFFICIENT_JWT_SECRET="sua_chave_secreta_aqui"
HYPER_EFFICIENT_TUYA_CLIENT_ID="seu_client_id"
HYPER_EFFICIENT_TUYA_CLIENT_SECRET="seu_client_secret"
```

---

## 🚀 Execução

### Desenvolvimento

```bash
# Executar em modo desenvolvimento
dotnet run

# Ou com watch para auto-reload
dotnet watch run
```

### Produção

```bash
# Build para produção
dotnet publish -c Release -o ./publish

# Executar build de produção
dotnet ./publish/HyperEfficient.dll
```

### Docker (Opcional)

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HyperEfficient.csproj", "./"]
RUN dotnet restore "HyperEfficient.csproj"
COPY . .
RUN dotnet build "HyperEfficient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HyperEfficient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HyperEfficient.dll"]
```

---

## 📚 Documentação da API

### Endpoints Principais

#### 🔐 Autenticação

```
POST /usuarios/login
POST /usuarios
GET  /usuarios/handshake
```

#### 🏢 Gestão Empresarial

```
GET    /setores
POST   /setores
PUT    /setores/{id}
DELETE /setores/{id}

GET    /categorias
POST   /categorias
PUT    /categorias/{id}
DELETE /categorias/{id}
```

#### 🔌 Equipamentos

```
GET    /equipamentos
POST   /equipamentos
PUT    /equipamentos/{id}
DELETE /equipamentos/{id}
GET    /equipamentos/{id}/status
POST   /equipamentos/{id}/ligar
POST   /equipamentos/{id}/desligar
```

#### 📊 Registros e Relatórios

```
GET    /registros
POST   /registros/start-stop/{equipamentoId}
GET    /registros/consumo/{equipamentoId}

GET    /relatorios/empresa
GET    /relatorios/setores
GET    /relatorios/categorias
```

### Swagger UI

Acesse a documentação interativa da API:

```
http://localhost:5205/swagger
```

### Exemplos de Uso

#### Login de Usuário

```bash
curl -X POST "http://localhost:5205/usuarios/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "usuario@exemplo.com",
    "senha": "senha123",
    "lembrarDeMim": false
  }'
```

#### Obter Relatório Empresarial

```bash
curl -X GET "http://localhost:5205/relatorios/empresa?dataInicio=2025-01-01&dataFim=2025-01-31" \
  -H "Authorization: Bearer SEU_TOKEN_JWT"
```

#### Controlar Equipamento

```bash
curl -X POST "http://localhost:5205/equipamentos/1/ligar" \
  -H "Authorization: Bearer SEU_TOKEN_JWT"
```

---

## 🔌 Integração IoT

### Tuya API Client

O sistema inclui um cliente completo para a API Tuya:

```csharp
public interface ITuyaApiClientService
{
    Task<EquipamentoStatusDto> GetStatusAsync(string deviceId);
    Task LigarAsync(int equipamentoId);
    Task DesligarAsync(int equipamentoId);
}
```

### Configuração da API Tuya

1. **Criar conta** em [Tuya IoT Platform](https://iot.tuya.com/)
2. **Obter credenciais** (Client ID e Client Secret)
3. **Configurar no appsettings.json**
4. **Adicionar Device IDs** nos equipamentos

### Autenticação HMAC-SHA256

```csharp
// Exemplo de geração de assinatura
var nonce = Guid.NewGuid().ToString("N");
var t = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
var stringToSign = $"GET\n{contentHash}\n\n{path}";
var signStr = clientId + accessToken + t + nonce + stringToSign;
var sign = ComputeHmac(signStr, clientSecret);
```

### Background Service

```csharp
public class TuyaUpdateService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _equipamentoService.AtualizarConsumoEquipamentos();
            await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
        }
    }
}
```

---

## 🔐 Segurança

### Autenticação JWT

```csharp
// Configuração no Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
        };
    });
```

### Criptografia de Senhas

```csharp
// PBKDF2 com salt
public static string HashPassword(string password)
{
    byte[] salt = new byte[16];
    using (var rng = new RNGCryptoServiceProvider())
    {
        rng.GetBytes(salt);
    }

    using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
    {
        byte[] hash = pbkdf2.GetBytes(32);
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);
        return Convert.ToBase64String(hashBytes);
    }
}
```

### Middleware de Tratamento de Erros

```csharp
public class ErrorHandlingMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
}
```

---

## 📊 Banco de Dados

### Schema Principal

```sql
-- Usuários
CREATE TABLE Usuario (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    Senha VARCHAR(255) NOT NULL,
    CriadoEm DATETIME NOT NULL,
    Ativo TINYINT DEFAULT 1
);

-- Setores
CREATE TABLE Setor (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(300),
    GastoGeral DOUBLE DEFAULT 0
);

-- Categorias
CREATE TABLE Categoria (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL
);

-- Equipamentos
CREATE TABLE Equipamento (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(300),
    PotenciaKwh DOUBLE NOT NULL,
    DeviceIdIntegration VARCHAR(100),
    CategoriaId INT,
    SetorId INT,
    Ativo TINYINT DEFAULT 1,
    FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id),
    FOREIGN KEY (SetorId) REFERENCES Setor(Id)
);

-- Registros
CREATE TABLE Registro (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    DataInicial DATETIME NOT NULL,
    DataFinal DATETIME,
    EquipamentoId INT NOT NULL,
    TotalKwh DOUBLE NOT NULL DEFAULT 0,
    TotalTempo DOUBLE GENERATED ALWAYS AS (
        IF(DataFinal IS NOT NULL,
           TIMESTAMPDIFF(SECOND, DataInicial, DataFinal) / 3600,
           NULL)
    ) STORED,
    FOREIGN KEY (EquipamentoId) REFERENCES Equipamento(Id)
);
```

### Índices de Performance

```sql
-- Índices para otimização
CREATE INDEX idx_equipamento_setor ON Equipamento(SetorId);
CREATE INDEX idx_equipamento_categoria ON Equipamento(CategoriaId);
CREATE INDEX idx_registro_equipamento ON Registro(EquipamentoId);
CREATE INDEX idx_registro_data ON Registro(DataInicial, DataFinal);
CREATE INDEX idx_usuario_email ON Usuario(Email);
```

### Consultas Otimizadas

```sql
-- Relatório de consumo por setor
SELECT
    s.Nome as SetorNome,
    SUM(r.TotalKwh) as ConsumoTotal,
    COUNT(DISTINCT e.Id) as TotalEquipamentos
FROM Setor s
LEFT JOIN Equipamento e ON s.Id = e.SetorId
LEFT JOIN Registro r ON e.Id = r.EquipamentoId
    AND r.DataInicial BETWEEN ? AND ?
WHERE s.Ativo = 1
GROUP BY s.Id, s.Nome
ORDER BY ConsumoTotal DESC;
```

---

## 🤝 Contribuição

### Padrões de Código

- **C#**: Seguir [convenções Microsoft](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- **SQL**: UPPERCASE para campos do banco
- **Nomenclatura**: PascalCase para classes, camelCase para variáveis
- **Documentação**: XML comments em métodos públicos

### Estrutura de Commits

```
feat: adiciona integração com Tuya API
fix: corrige cálculo de consumo energético
docs: atualiza documentação da API
style: melhora formatação do código
refactor: reorganiza estrutura de services
test: adiciona testes para RegistroService
```

### Processo de Desenvolvimento

1. **Fork** o repositório
2. **Crie** uma branch para sua feature
3. **Desenvolva** seguindo os padrões
4. **Teste** suas alterações
5. **Commit** com mensagem descritiva
6. **Push** e abra um Pull Request

### Testes

```bash
# Executar testes unitários
dotnet test

# Executar testes de integração
dotnet test --filter Category=Integration

# Cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

---

## 📞 Suporte

- **LinkTree**: https://linktr.ee/hyperefficient

---

<div align="center">

**Desenvolvido com ❤️ para o curso Entra21 - Turma C#**

[(https://img.shields.io/badge/Linktree-green?style=for-the-badge)](https://linktr.ee/hyperefficient)

</div>
