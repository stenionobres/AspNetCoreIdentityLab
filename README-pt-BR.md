# ASP.NET Core Identity Lab

Aplicação criada com o principal objetivo de explorar as funcionalidades e características do ASP.NET Core Identity.

Nesta aplicação, vários cenários reais de uso foram testados baseados em uma mini aplicação.

Após os estudos de caso, as principais conclusões foram documentadas neste arquivo e servem como referência e fonte de consulta futura.

![](./readme-pictures/aspnetcore-identitylab.gif)

## Índice

* [Pré-requisitos](#pre-requisitos)
* [Como iniciar?](#como-iniciar)

## Pré-requisitos

O que precisa ser instalado na máquina para estender e depurar o projeto:

    Visual Studio Community 2019;
    Net Core SDK 3.1;
    SQL Server

## Como iniciar?

* Instale e/ou configure todos os pré-requisitos mencionados acima;
* Clone o repositório na máquina local;
* Crie as bases de dados utilizadas na aplicação: AspNetCoreIdentityLab, AspNetCoreIdentityLabCustomModel e Authentication;
* Baixe as dependências do Nuget para a solução no Visual Studio;
* Execute as migrações para o banco de dados desejado com o comando: Update-Database -Context [ClassName of context];
* Execute o projeto AspNetCoreIdentityLab.Application MVC;