HyperEfficient
Sistema web API para gestão de consumo e custos de energia elétrica de equipamentos industriais.
Permite cadastrar setores, categorias e equipamentos, registrar períodos de uso e extrair relatórios de gasto energético.

✨ Funcionalidades já implementadas
Módulo	Operações
Equipamento	CRUD completo + busca/ordenação por diversos critérios EquipamentoRepository
Setor	CRUD completo SetorRepository
Categoria	CRUD completo CategoriaRepository
Registro	CRUD para período de uso do equipamento RegistroRepository
Usuário	CRUD básico de usuários (para expansão futura de permissões) UsuarioRepository
Relatórios	Endpoints prontos para sumarizar consumo (serão expandidos a gráficos)

Pronto para alertas automáticos (detecção de excesso/ociosidade) e exportação PDF/Excel em versões futuras.

🏗️ Arquitetura
ASP.NET Core 7 Web API

Dapper como micro-ORM para acesso MySQL Connection

AutoMapper para mapear DTO ⇆ Entidades

Injeção de dependência automática por reflexão:

AddCamadaInfra() registra todos os Repositories

AddCamadaAplicacao() registra todos os Services ServiceCollectionExtens…

Camadas

Contracts – interfaces

Entity – modelos de domínio

Repository – acesso a dados

Service – regra de negócio

Controllers – endpoints REST

Infrastructure – Connection, DI extensions, Mappings

🗄️ Banco de dados
Script completo em CreateSQL.txt cria schema hyperefficient e todas as FK necessárias. CreateSQL

bash
Copiar
Editar
mysql -u root -p < CreateSQL.txt
🚀 Execução local
bash
Copiar
Editar
# 1. clone
git clone https://github.com/<sua-org>/HyperEfficient.git
cd HyperEfficient

# 2. ajuste a connection string (se necessário)
#    Infraestructure/Connection/Connection.cs

# 3. restaure pacotes e rode
dotnet restore
dotnet run
API sobe em https://localhost:5001.

Documentação interativa via Swagger UI em /swagger. Program

🔌 Endpoints principais
Verbo	Rota	Descrição
GET	/equipamentos	Listar equipamentos
POST	/equipamentos	Cadastrar equipamento
PUT	/equipamentos	Atualizar equipamento
...	...	idem para setores, categorias, registros, usuarios

(Códigos nos controllers correspondentes)

🛠️ Estrutura de diretórios
bash
Copiar
Editar
HyperEfficient/
 ├─ Contracts/          # Interfaces de Repository & Service
 ├─ Controllers/        # APIs REST
 ├─ DTO/                # Data-transfer objects
 ├─ Entity/             # Entidades de domínio
 ├─ Infrastructure/
 │   ├─ Connection/     # MySQL Dapper helper
 │   └─ Extensions/     # Registro automático de DI
 ├─ Repository/         # Implementações Dapper
 ├─ Response/           # Modelos de resposta padrão
 ├─ Services/           # Regras de negócio
 ├─ CreateSQL.txt       # Script do banco
 └─ Program.cs          # Bootstrap da aplicação
📈 Roadmap breve
Alertas automáticos de excesso/ociosidade (+20 % / <15 %) enviados por e-mail ou push.

Exportação de relatórios (PDF / Excel).

Autenticação e permissões por usuário.

Integração com sensores IoT para leitura em tempo real.

🤝 Contribuindo
Fork o repositório e crie sua branch: feature/nome-funcionalidade.

Abra pull request pequeno e objetivo.

Siga o padrão Clean Code usado no projeto.

Feito com 💡 e ☕ pela equipe HyperEfficient.