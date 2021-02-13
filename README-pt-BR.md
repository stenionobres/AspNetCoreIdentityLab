# ASP.NET Core Identity Lab

Aplicação criada com o principal objetivo de explorar as funcionalidades e características do ASP.NET Core Identity.

Nesta aplicação, vários cenários reais de uso foram testados baseados em uma mini aplicação.

Após os estudos de caso, as principais conclusões foram documentadas neste arquivo e servem como referência e fonte de consulta futura.

![](./readme-pictures/aspnetcore-identitylab.gif)

## Índice

* [Pré-requisitos](#pre-requisitos)
* [Como iniciar?](#como-iniciar)
* [Requisitos do projeto](#requisitos-do-projeto)
* [Modelo padrão de banco de dados](#modelo-padrao-de-banco-de-dados)

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

## Requisitos do projeto

Para experimentar os recursos do ASP.NET Core Identity e estabelecer as melhores e mais eficientes práticas de uso, alguns requisitos foram estabelecidos.

Esses requisitos visam aproximar o estudo de caso de um cenário de uso real, onde várias características do framework devem ser exploradas.

Abaixo estão listados quais requisitos a solução atende:

    Uso de email ou username para login;
	Customizar dados de usuários;
    Confirmação de criação de conta por email;
    Explorar IdentityOptions;
    Relembrar, recuperar e trocar senha;
    Password Hashing;
    Password Rotation;
    Captchas;
    Two Factor Authentication (2FA);
    Provedor externo de autenticação (Google, Facebook, etc);
    Bloqueio de logins concorrentes;
    Identificar acessos de diferentes ips;
    Política de senhas;
    Política de email e username;
    Alterar o schema padrão de banco de dados;
    Uso de diferentes banco de dados;
    Mecanismo de persistência customizado;
    API de autenticação;
    Log de execução;
    Roles;
    Claims;
    Policies;
    Grupos de usuários;
    Autorizações dinâmicas (Módulos, submódulos e funcionalidades);
    API de autorização;

## Modelo padrão de banco de dados

O banco de dados padrão do ASP.NET Core Identity consiste nas seguintes entidades:

|**Entidade**       |**Descrição**                                                     |
|-------------------|------------------------------------------------------------------|
|  AspNetUsers      | Representa o usuário                                             |
|  AspNetRoles      | Representa uma Role                                              |
|  AspNetUserClaims | Representa uma Claim que o usuário possui                        |
|  AspNetUserTokens | Representa um token de autenticação para o usuário               |
|  AspNetUserLogins | Associa o usuário com um login                                   |
|  AspNetRoleClaims | Representa uma Claim relacionada a todos os usuários de uma Role |
|  AspNetUserRoles  | Uma entidade associativa que relaciona usuários e Roles          |

Os tipos de entidade estão relacionados entre si das seguintes maneiras:

* Cada usuário pode ter vários UserClaims;
* Cada usuário pode ter vários UserLogins;
* Cada usuário pode ter vários UserTokens;
* Cada Role pode ter várias RoleClaims associados;
* Cada usuário pode ter várias Roles associadas e cada Role pode ser associada a vários usuários. Este é um relacionamento muitos para muitos que requer uma tabela associativa no banco de dados. A tabela associativa é representada pela entidade AspNetUserRoles.

![image info](./readme-pictures/aspnet-core-identity-default-database-model.jpg)