# ⚡ HyperEfficient - Backend

Sistema web API para gestão de consumo e custos de energia elétrica de equipamentos industriais.

Permite cadastrar setores, categorias e equipamentos, registrar períodos de uso e extrair relatórios de gasto energético.

---

## ✨ Funcionalidades

| Módulo      | Operações                                                                                  |
|-------------|--------------------------------------------------------------------------------------------|
| Equipamento | CRUD completo, busca/ordenação por critérios (EquipamentoRepository)                        |
| Setor       | CRUD completo (SetorRepository)                                                            |
| Categoria   | CRUD completo (CategoriaRepository)                                                        |
| Registro    | CRUD para períodos de uso do equipamento (RegistroRepository)                              |
| Usuário     | CRUD básico de usuários (expansão futura para permissões) (UsuarioRepository)              |
| Relatórios  | Endpoints para sumarizar consumo (expansão futura para gráficos)                           |

> **Pronto para:**  
> - Alertas automáticos (excesso/ociosidade)  
> - Exportação PDF/Excel (futuro)

---

## 🏗️ Arquitetura

- **Framework:** ASP.NET Core 7 Web API
- **ORM:** Dapper (micro-ORM, MySQL)
- **Mapper:** AutoMapper (DTO ⇆ Entidades)
- **Injeção de Dependência:** Automática por reflexão
- **Padrão:** Domain Driven Design (DDD)

### Camadas

- `Contracts/` – Interfaces de Repository & Service
- `Controllers/` – APIs REST
- `Dtos/` – Data Transfer Objects
- `Entities/` – Modelos de domínio
- `Infrastructure/` – Connection, DI, Mappings, Middleware, Criptografia, etc.
- `Repositories/` – Implementações Dapper
- `Services/` – Regras de negócio

---

## 🗄️ Banco de Dados

- **Script:** `CreateSQL.txt`  
  Cria o schema `hyperefficient` e todas as FKs necessárias.

```bash
mysql -u root -p < CreateSQL.txt
```

---

## 🚀 Execução Local

```bash
# 1. Clone o repositório
git clone https://github.com/<sua-org>/HyperEfficient.git
cd HyperEfficient

# 2. Ajuste a connection string (se necessário)
#    Infrastructure/Connection/Connection.cs

# 3. Restaure pacotes e rode
dotnet restore
dotnet run
```

- API disponível em: `https://localhost:5001`
- Documentação interativa: `/swagger`

---

## 🔌 Endpoints Principais

| Verbo | Rota           | Descrição                |
|-------|----------------|-------------------------|
| GET   | /equipamentos  | Listar equipamentos     |
| POST  | /equipamentos  | Cadastrar equipamento   |
| PUT   | /equipamentos  | Atualizar equipamento   |
| ...   | ...            | Idem para outros módulos|

> Veja os controllers para detalhes completos.

---

## 🗂️ Estrutura de Diretórios

```
HyperEfficient/
 ├─ Contracts/          # Interfaces
 ├─ Controllers/        # APIs REST
 ├─ Dtos/               # Data Transfer Objects
 ├─ Entities/           # Entidades de domínio
 ├─ Infrastructure/     # Infraestrutura técnica
 ├─ Repositories/       # Implementações Dapper
 ├─ Services/           # Regras de negócio
 ├─ CreateSQL.txt       # Script do banco
 └─ Program.cs          # Bootstrap da aplicação
```

---

## 📈 Roadmap

- [ ] Alertas automáticos de excesso/ociosidade (+20% / <15%) por e-mail/push
- [ ] Exportação de relatórios (PDF/Excel)
- [ ] Autenticação e permissões por usuário
- [ ] Integração com sensores IoT para leitura em tempo real

---

## 🤝 Contribuindo

1. Faça um fork e crie sua branch: `feature/nome-funcionalidade`
2. Abra um pull request pequeno e objetivo
3. Siga o padrão Clean Code do projeto

---

Feito com 💡 e ☕ pela equipe HyperEfficient.