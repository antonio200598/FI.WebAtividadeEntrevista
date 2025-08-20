# Teste Prático – Processo Seletivo

## Objetivo  
Realizar melhorias no sistema **`FI.WebAtividadeEntrevista`** para fins de avaliação técnica.  

## Requisitos  
- Visual Studio 2022 (versão *Community* também é aceita)  
- .NET Framework 4.8  
- SQL Server Express 2019 LocalDB  
- **Solution**: baixe o repositório no formato `.zip`  

## Implementações  

### 1. Inclusão do Campo CPF no Cadastro de Clientes  
- Adicionar o campo **CPF** (formato: `999.999.999-99`) nas telas de cadastro e edição de clientes.  
- O campo deve ser **obrigatório**, com **validação de CPF** e bloqueio de valores **duplicados**.  
- Alterar a tabela **`CLIENTES`** para incluir a coluna **CPF**.  

### 2. Botão para Gerenciamento de Beneficiários  
- Criar um botão que abra um **modal** para cadastrar e gerenciar **beneficiários** (CPF e Nome).  
- O modal deve conter uma **grade** para inclusão, edição e exclusão de beneficiários.  
- Implementar **validação de CPF** e impedir **duplicidade** dentro do mesmo cliente.  
- Criar a tabela **`BENEFICIARIOS`** com os seguintes campos:  
  - `ID`  
  - `CPF`  
  - `NOME`  
  - `IDCLIENTE`  

## Execução  
1. Abrir a solution no **Visual Studio 2022**.  
2. Antes de executar, restaurar os pacotes, compilar(clean build -> rebuild) e rodar a aplicação.  
3. Realizar os testes na aba **Clientes**.