# Caju - Sistema de Gerenciamento de Transações

O Caju é uma aplicação para gerenciamento de transações financeiras. Ele permite a autorização de transações e o controle de saldo por categoria (FOOD, MEAL, CASH). A aplicação verifica o saldo das contas e debita a quantia correta da categoria apropriada, garantindo o fluxo financeiro correto.

## Funcionalidades

- Processa e autoriza transações com base no MCC (Merchant Category Code).
- Verifica e atualiza o saldo das contas por categoria.
- Suporta múltiplas categorias para transações.
- Oferece uma API RESTful para interagir com o sistema.
## Pré-requisitos

Antes de rodar a aplicação, certifique-se de que você tem as seguintes ferramentas instaladas:

- **.NET 8.0 SDK** - Você pode baixar a versão mais recente do .NET [aqui](https://dotnet.microsoft.com/download).
- **Docker** - Baixe o Docker Desktop [aqui](https://www.docker.com/products/docker-desktop).

## Configuração e Execução

### Rodando o projeto localmente

1. **Clone o repositório**

2. **Entre no diretório do projeto**:

3. **Restaure as dependências** do projeto com o comando:

    ```bash
    dotnet restore
    ```

4. **Aplique as migrações do banco de dados** (caso esteja utilizando o Entity Framework):

    ```bash
    dotnet ef database update
    ```

5. **Execute a aplicação** localmente:

    ```bash
    dotnet run
    ```

    A API estará disponível em `https://localhost:5000`.

### Rodando com Docker

Para rodar a aplicação dentro de um container Docker, siga os passos abaixo:

1. **Execute o docker na pasta do .csproj**:

    ```bash
    docker-compose up
    ```

2. **Verifique se o container está rodando**:

    Use o comando a seguir para verificar se o container está ativo:

    ```bash
    docker ps
    ```
 Isso irá rodar a aplicação na porta `5051` do seu computador. Acesse a aplicação em `https://localhost:5051/index.html`.
 
## Estrutura do Projeto

- **Domain**: Contém os UseCases (TransactionUseCase),Models(Balance, Merchant, etc) e Ports (Interfaces de conexão com os repositories).
- **Driven**: Contém Migrations, Configurações de Banco e Repositorios.
- **Driver**: Contém Controller TransactionController

- ## L4 

Para essa questão existe o plano de usar Lock para travar uma segunda requisição, ate que a primeira seja concluida, destravando assim o Lock, o maior problema dessa abordagem é que ela pode ser demorada, dependendo do que está sendo feito e pela questão ela informa que é preciso fazer isso em 100ms, como o tempo de destravar e travar, depende muito da implementação, do sistema usado e da requisição. Para melhorar essa performance pode ser adicionado um timeout para a segunda requisição, ou seja, se ela tentar fazer uma transação e se a mesma ja está em uso, ela espera um tempo determinado ou pode abordar a transição.

![Lock com Timeout (1)](https://github.com/user-attachments/assets/7fea637b-a4d5-49da-ab78-baf5bd3f9f90)

