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
* Authors

## Prerequisites

What needs to be installed on the machine to extend and debug the project:

    Visual Studio Community 2019;
    Net Core SDK 3.1;
    SQL Server

## Project Requirements

In order to experience the features of the ASP.NET Core Identity and establish the best and most efficient usage practices, some requirements have been established.

These requirements aim to bring the case study closer to a real use scenario where several characteristics of the framework must be explored.

Below are listed which requirements the solution meets:

    Many types of login;
	Password Configuration;
	Account customization;
	Account confirmation by email;
	Change database schema;
	Use diferents database;
	Many persistence ways;

## Authentication x Authorization

>**Authentication:** The process that answer the question, Who are you in the application?

>**Authorization:** The process that answer the question, What can you do in the application?