# Desafio Técnico Texo IT

Este é um projeto desenvolvido como parte do desafio proposto pela empresa Texo IT. O objetivo deste projeto é criar uma lista de indicados e vencedores da categoria "Pior Filme" do Golden Raspberry Awards (GRA), uma premiação satírica que elege os piores filmes do ano.

## Funcionalidades
O projeto consiste em uma lista dos indicados e vencedores da categoria "Pior Filme". A lista conta com informações como o título do filme, estúdio, o ano de lançamento e o nome dos diretores.

## Stack
- .NET 6

## Arquitetura / Padrões / Frameworks / Estratégias de desenvolvimento
- DDD
- Notification Pattern
- CQRS Pattern
- Middleware para tratamento de Exceptions de forma global
- EntityFramework Core (InMemory)
- Testes de Integração com xUnit
- Importação de dados em CSV ao iniciar a API
- FluentValidations

Ao executar o projeto, será carregado automaticamente um arquivo .csv por um diretório configurado, com dados de exemplo. Caso não for informado (ou não for localizado) o arquivo, isto não impede a execução do projeto, apenas iniciará os serviços vazios.

## Como utilizar / executar?
Você precisa seguir basicamente dois passos para configurar o carregamento do arquivo ".csv" adequadamente.

1- Dentro do diretório "\Datasets" você encontra um arquivo ".csv" de exemplo como referência, você pode utilizar ele como template.

2- Abra o projeto de "GRA.API.cs", e localize o arquivo "appsettings.json". Você encontrará uma propriedade com o nome de "CsvFilePath", que basta você informar o caminho onde estará fisicamente na sua máquina o arquivo ".csv".

E pronto, basta executar o projeto, que já estará com dados pré-carregados.

