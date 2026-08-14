# PartyRaidR API

This page is meant to describe how the API works in order to make workflow unambiguous.

## Setup

Before beginning development, a few setup steps are required.

#### Prerequisites:
- .NET 9 (or higher version) installed
- Docker installed
- EF Core CLI tool (dotnet-ef) installed

#### Steps:
Run the following command:
```bash
> cp appsettings.Sample.json appsettings.json
```

In the new `appsettings.json` fill the missing data with the **same ones as in `src/.env`.**

In some cases you might need to run:
```bash
> dotnet restore
> dotnet clean
```

After that, go to `src` and run `docker compose up backend db`.

#### Migrations

After modifying the DB Context or a database model, running a migration is necessary in order to apply changes to the databse.

First, you need to have **EF Core CLI tool** installed.
If you do not have it yet, install it like this:

```bash
> dotnet tool install --global dotnet-ef
```

Once in `src/PartyRaidR.Backend`, in your terminal run:
```bash
> dotnet ef migrations add 
```

###### Warning

On Linux systems, running `dotnet ef` commands might result in the following: `dotnet-ef: command not found`.

If you get this error message, you need to add `dotnet-ef` to your `PATH`.

**Quick fix:**
Open your shell config (`nano ~/.bashrc` or `nano ~/.zshrc`) and insert this line to the end of the file:
```bash
export PATH="$PATH:$HOME/.dotnet/tools"
```
Then save the file and close it.
Reload your config:
```bash
> source ~/.bashrc
```

After that, running `dotnet ef` in your terminal should work properly.

## Architecture

The API is built using **.NET 9.**, `ASP.NET` and uses a *MySQL* database.

The **Entity Framework Core** ORM is used in order to perform a code-first approach. Most of the time writing SQL queries by hand is not needed.

#### API documentation

Swagger is used as the API documentation. It can be accessed through `http://party.test/swagger/index.html` in a local setup by default.

#### DB Entities

All database entities are defined in the `Models` folder following EF Core conventions. Every model needs to implement the `IDbEntity` interface.

#### Layers - Quick Summary

The flow in the API can be broken down into three main layers:

- Repository
- Service
- Controller

##### Repository

Repository classes are responsible for direct communication with the database.
Every repository class extends the `RepositoryBase` class.

##### Service

Service layer is where the business logic happens. Services use repositories, and they might even use other services.

To make services unified and serve the API as easily as possible, each service method returns a `ServiceResponse` including the following information:

```js
{
    data: T,
    success: boolean,
    message: string,
    statusCode: number
}
```

**Data** is the actual target of the request. It might be an object or a scalar value.
**Success** is a boolean value indicating if the request was successful.
**Message** is a `string` value (mostly used when there is no data or if the request was unsuccessful).
**Status Code** is the HTTP code to be returned by the API.

*Example:*
```json
{
    "data": {
        "id": "123",
        "username": "user1",
        "email": "example@mail.org"
    },
    "success": true,
    "message": "User fetched successfully.",
    "statusCode": 200
}
```

##### Controller

This layer is the main gate of the API. Here, **endpoints** are defined. For each endpoint a *service method is called.* So controllers are essentially responsible for nothing but to **call the relevant service method** that does the job instead.

Everything is handled by the service, as the Controller method only gets the object from the service mentioned before, so all the necessary data is ready for the controller to send the response.

Controller methods get data from the request (it might come from the **URL query, the request body or sometimes even the header**) and hands it to the service method.

## Authentication

The backend app implements its own, custom authentication system. The user model is defined in `Models/User.cs`.

Authentication and authorization is done using **JSON Web Tokens.** The token payload contains the following information:
- User ID
- Email address
- User role

Tokens live for 60 minutes, though it would be ideal to implement ***refresh tokens*** in the future.

User role can either be *User* or *Admin*.