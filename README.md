# Catalog Project

Este é um sistema de gerenciamento de catálogo de produtos desenvolvido com .NET 8, seguindo princípios de arquitetura limpa e separação de preocupações. O sistema é composto por uma API robusta e um frontend moderno em Blazor WebAssembly.

## 🚀 Estrutura de Pastas

```text
Catalog/
├── Catalog.Api/           # API Host, Controllers e Configurações de Infraestrutura
├── Catalog.Data/          # Persistência, Repositórios e Migrations (EF Core)
├── Catalog.Domain/        # Entidades, Contratos (Interfaces) e Lógica de Negócio
└── Catalog.Ui/            # Solução de Frontend
    ├── Catalog.Ui.Client/ # Aplicação Blazor WebAssembly (Pages e UI)
    └── Catalog.UI.Shared/ # Biblioteca Compartilhada (ViewModels, ApiService)
```

O projeto está organizado na seguinte estrutura de camadas:

*   **Catalog.Api**: Camada de host da API REST. Responsável pelos controllers, filtros e configurações de infraestrutura como Health Checks e Injeção de Dependência.
*   **Catalog.Data**: Camada de persistência. Contém o `CatalogDbContext`, as migrações do Entity Framework Core e a implementação dos repositórios (`GenericRepository`, `CategoryRepository`).
*   **Catalog.Domain**: O núcleo da aplicação. Contém as entidades de negócio (`Product`, `Category`), interfaces de repositório (Contracts), exceções de domínio e o padrão `Result` para fluxos de resposta.
*   **Catalog.Ui**: Solução de interface do usuário:
*   **Catalog.Ui.Client**: Aplicação Blazor WebAssembly contendo as páginas de listagem e formulários de produtos.
*   **Catalog.UI.Shared**: Biblioteca de classes compartilhada contendo ViewModels (VMs) e o `ApiService` utilizado para comunicação HTTP.

## 🛠️ Tecnologias Utilizadas

*   **Backend**: .NET 8, ASP.NET Core Web API, Entity Framework Core.
*   **Frontend**: Blazor WebAssembly (WASM).
*   **Banco de Dados**: SQL Server.
*   **Containerização**: Docker e Docker Compose.
*   **Monitoramento**: Microsoft Health Checks (com detalhamento de integridade do banco de dados).
*   **Padrões**: Repository Pattern, Result Pattern, Injeção de Dependência.

## ⚙️ Configuração e Execução

### Pré-requisitos

*   .NET 8 SDK
*   SQL Server

### 1. Banco de Dados

No projeto `Catalog.Api`, localize o arquivo `appsettings.json` e ajuste a string de conexão `DbConnection` conforme seu ambiente local.

Para aplicar as migrações e criar o banco de dados:
```bash
dotnet ef database update --project Catalog.Data --startup-project Catalog.Api
```

### 2. Executando a API

Navegue até a pasta da API e inicie o serviço:
```bash
cd Catalog.Api
dotnet run
```
A API estará disponível para receber requisições. Você pode verificar a saúde do sistema em:
*   `GET /health`: Status simplificado.
*   `GET /health/details`: Status detalhado dos componentes (ex: SQL Server).

### 3. Executando o Frontend (UI)

Navegue até a pasta do cliente Blazor e inicie:
```bash
cd Catalog.Ui/Catalog.Ui.Client
dotnet run
```
O frontend está configurado para a cultura `pt-BR`, garantindo que campos de preço e datas sigam o formato brasileiro.

## 📝 Funcionalidades Principais

*   **Gerenciamento de Produtos**: CRUD completo integrado com a API através de um serviço centralizado (`ApiService`).
*   **Result Pattern**: Todas as operações de API e domínio retornam um objeto `Result<T>`, padronizando o tratamento de sucesso e erro.
*   **Repositório Genérico**: Implementação base que reduz a duplicidade de código para operações comuns de banco de dados.
*   **Cultura Localizada**: Configuração global para `pt-BR` no Client-side, facilitando o uso de decimais com vírgula.

## 🔍 Debug e Logs

A aplicação frontend utiliza `ILogger` e logs no console do navegador para facilitar o rastreio de payloads enviados e recebidos durante as operações de criação e atualização de produtos.

---

## Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Windows ou Mac) ou Docker Engine (Linux).
*   Docker Compose habilitado.

## Como executar a aplicação

Siga os passos abaixo para iniciar o ambiente:

1.  **Navegue até a raiz do projeto**:
    Abra o terminal ou prompt de comando na pasta onde se encontra o arquivo `docker-compose.yml`.

2.  **Subir os serviços**:
    Execute o comando abaixo para construir as imagens (caso seja a primeira vez ou haja alterações) e iniciar os containers em segundo plano:
    ```bash
    docker-compose up -d --build
    ```

3.  **Verificar o status**:
    Você pode verificar se os containers estão rodando com:
    ```bash
    docker-compose ps
    ```

## Encerrando o ambiente

Para parar os serviços e remover os containers criados, utilize:
```bash
docker-compose down
```

*Este projeto foi desenvolvido como parte de um estudo prático de arquitetura .NET.*