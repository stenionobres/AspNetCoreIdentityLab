# ASP.NET Core Identity Lab

Read this documentation in other languages: [Portuguese (pt-BR)](README-pt-BR.md)

Application created with the main objective of exploring the features and characteristics of the ASP.NET Core Identity.

In this application, several real usage scenarios were tested based on a mini application.

After the case studies, the main conclusions were documented in this file and serve as a reference for use and source of consultation.

## Table of contents

* [Prerequisites](#prerequisites)
* Getting Started
* [Project Requirements](#project-requirements)
* Identity Default Database Model
* Project Structure
    * Used Packages
* [Authentication x Authorization](#authentication-x-authorization)
* Identity Basic Configuration 
    * IdentityOptions
* Entity Framework x Another persistence
* Logging
* Fast tips
* Lessons learned
* References used
* [Authors](#authors)

## Prerequisites

What needs to be installed on the machine to extend and debug the project:

    Visual Studio Community 2019;
    Net Core SDK 3.1;
    SQL Server

## Project Requirements

In order to experience the features of the ASP.NET Core Identity and establish the best and most efficient usage practices, some requirements have been established.

These requirements aim to bring the case study closer to a real use scenario where several characteristics of the framework must be explored.

Below are listed which requirements the solution meets:

    Use of email or username to login;
	Custom user data;
	Account confirmation by email;
    Explore IdentityOptions;
    Remember, recover and reset password;
    Password Rotation;
    Captchas;
    Two Factor Authentication (2FA);
    Use of authentication providers (Google, Facebook, Github, Microsoft, Twitter);
    Block concurrent logins;
    Track logins of different ips;
    Single SingOn / Windows authentication;
    Password Policy;
    Email and Username Policy;
	Change database schema;
	Use diferents databases;
	Custom storage providers;
    Create an Authentication API;
    Logging;
    WS-Federation;
    Roles;
    Claims;
    Policies;
    User groups;
    Authorization levels (Groups of functionalities, functionalities, functions);
    Create an Authorization API;

## Authentication x Authorization

>**Authentication:** The process that answer the question, Who are you in the application?

>**Authorization:** The process that answer the question, What can you do in the application?

## Authors

* **Stenio Nobres** - [Github](https://github.com/stenionobres)