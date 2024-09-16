# Sistema de transações

Este é um sistema de transações de cartão de crédito desenvolvido para integrar com o gateway de pagamentos Cielo. O sistema permite a criação, captura e cancelamento de transações através de uma interface web. O projeto foi construído usando C#, ASP.NET Core e Blazor WebAssembly.

## Requisitos

- **.NET 8**
- **Visual Studio 2022** ou **Visual Studio Code**
- **Navegador web** (para a interface web)
- **Postman** (para testes de API)

## Estrutura do Projeto

O projeto está dividido em duas partes principais:

1. **Backend (`UmblerCielo.Server`)**:
   - Desenvolvido em C# com ASP.NET Core.
   - Fornece endpoints para criar, capturar e cancelar transações.
   - Integra com o gateway de pagamentos Cielo usando o sandbox disponibilizado pela Cielo.

2. **Frontend (`UmblerCielo.Client`)**:
   - Desenvolvido com Blazor WebAssembly.
   - Oferece uma interface para usuários inserirem informações de cartão de crédito e interagirem com a API do backend.

## Instalação

1. **Clone o Repositório**

    ```bash
    git clone https://github.com/seuusuario/umblercielo.git
    cd umblercielo
    ```

2. **Instale as Dependências**

    Navegue para o diretório do backend e execute:

    ```bash
    cd UmblerCielo.Server
    dotnet restore
    ```

    Navegue para o diretório do frontend e execute:

    ```bash
    cd ../UmblerCielo.Client
    dotnet restore
    ```

## Execução

1. **Execute o Backend**

    No diretório do backend (`UmblerCielo.Server`), execute:

    ```bash
    dotnet run
    ```

    O backend estará disponível em `http://localhost:5179`.

2. **Execute o Frontend**

    No diretório do frontend (`UmblerCielo.Client`), execute:

    ```bash
    dotnet run
    ```

    O frontend estará disponível em `http://localhost:5180`.

## Endpoints da API

- **Criar Transação**
  - **Método:** POST
  - **URL:** `http://localhost:5179/api/transaction/create`
  - **Corpo da Requisição (JSON):**

    ```json
    {
      "MerchantOrderId": "12345",
      "Payment": {
        "Amount": 10000,
        "Installments": 1,
        "CreditCard": {
          "CardNumber": "1234567812345678",
          "Holder": "Nome do Titular",
          "ExpirationDate": "12/2025",
          "SecurityCode": "123",
          "Brand": "Visa"
        }
      }
    }
    ```

- **Capturar Transação**
  - **Método:** POST
  - **URL:** `http://localhost:5179/api/transaction/capture/{transactionId}`
  - **Corpo da Requisição (JSON):**

    ```json
    {
      "amount": 10000
    }
    ```

- **Cancelar Transação**
  - **Método:** POST
  - **URL:** `http://localhost:5179/api/transaction/cancel/{transactionId}`

- **Testar Conexão**
  - **Método:** GET
  - **URL:** `http://localhost:5179/api/transaction/TestConnection`

## Testes

### Testes no Postman

Todos os endpoints da API foram testados usando o Postman. Aqui estão alguns detalhes dos testes realizados:

1. **Criar Transação:** Verificado se a transação é criada corretamente e se o retorno é conforme esperado.
2. **Capturar Transação:** Testado o processo de captura de uma transação existente.
3. **Cancelar Transação:** Confirmado o cancelamento de uma transação e a resposta recebida.
4. **Testar Conexão:** Validado se o backend está respondendo corretamente.

### Problemas Conhecidos

- **Erro 500 (Internal Server Error):** Se ocorrer um erro 500, verifique os logs do servidor para detalhes adicionais. Pode estar relacionado a problemas de deserialização ou configuração no backend.

## Contribuições

Se você deseja contribuir para este projeto, siga estas etapas:

1. Fork o repositório.
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`).
3. Faça as alterações e commit (`git commit -am 'Adiciona nova feature'`).
4. Push para a branch (`git push origin feature/nova-feature`).
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
