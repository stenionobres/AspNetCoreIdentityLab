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
* [Registering an user](#registering-an-user)
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

## Registering an user

To register an user on Asp Net Core Identity the `UserManager` class instance with `CreateAsync` method must be used. 

``` C#
var user = new User { UserName = "Username", Email = "Email" };
var result = await _userManager.CreateAsync(user, "Password");
if (result.Succeeded)
{

}
```

The username field can be filled with an email or alphanumeric username, example: username@email.com or exampleofusername. **However the use of an email as username is most recommended**. The purpose of an email as username is to facilitate password recover.

### How to customize user atributes?

Asp Net Core Identity provides resources to customize user atributes. For this, the user model must be extended. The example below shows an `Occupation` custom field:

``` C#
public class User : IdentityUser<int>
{
    public string Occupation { get; set; }
}
```

The `Occupation` field is added to default user fields showed in [Identity Default Database Model](#identity-default-database-model). For apply the custom field a migration must be created and applied in database.

### Custom register rules

In many cases is necessary add custom rules to be applied on user register. These rules change from business to business. 

To add custom rules the `IUserValidator<TUser>` interface must be used. An example is showed on [CustomUserValidator](./AspNetCoreIdentityLab.Application/IdentityValidators/CustomUserValidator.cs) class. This examples shows a rule that username must be at least 6 characters.

The custom rule is configured on `ConfigureServices` method in [Startup](./AspNetCoreIdentityLab.Application/Startup.cs) class.

### Account confirmation by email

Asp Net Core Identity provides account confirmation by email, for this some configurations are needed.
In the first the `SignIn` options on `IdentityOptions` should be changed.

``` C#
identityOptions.SignIn.RequireConfirmedAccount = true;
identityOptions.SignIn.RequireConfirmedEmail = true;
```

A class that implements the `IEmailSender` interface must be created. This class should use some SMTP credentials like [SendGrid](https://sendgrid.com/).

With a developer purpose the [Gmail](https://www.hostinger.com.br/tutoriais/aprenda-a-utilizar-o-smtp-google/) SMTP was used in this project. The [EmailSmtpSender](./AspNetCoreIdentityLab.Application/EmailSenders/EmailSmtpSender.cs) class shows how to email could be send.

The SMTP credentials was saved using [Secret Manager tool](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows). This resource can be acessed with right click on AspNetCoreIdentityLab.Application project option **Manage User Secrets**, that is showed in a image below:

![image info](./readme-pictures/manage-user-secrets.jpg)

The source code uses a usersecrets.json configuration like that:

``` JSON
{
    "EmailSmtpSender": {
        "Host": "smtp.gmail.com",
        "Port": 587,
        "EnableSSL": true,
        "UserName": "yourEmailAddress",
        "Password": "yourEmailPassword"
    }
}
```

The configuration below is used on `ConfigureServices` method in `Startup` class:

``` C#
services.AddTransient<IEmailSender, EmailSmtpSender>(email => GetEmailConfiguration());
```

In the [Register](./AspNetCoreIdentityLab.Application/Areas/Identity/Pages/Account/Register.cshtml.cs) class is showed an example that uses `SignIn` options to send account confirmation email.

>Is important to know that if there are accounts already created without email confirmation and the configuration is changed to account confirmation, these accounts will not log in. The EmailConfirmed flag in the AspNetUsers table must be changed to the value = 1.

## Authors

* **Stenio Nobres** - [Github](https://github.com/stenionobres)