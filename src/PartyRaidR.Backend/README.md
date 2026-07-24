# PartyRaidR API

This page is meant to describe how the API works in order to make workflow unambiguous.

## Setup

Before beginning development, a few setup steps are required.

#### Prerequisites:
- .NET 9 (or higher version) installed
- Docker installed

#### Steps:
Run the following command:
```bash
> cp appsettings.Sample.json appsettings.json
```

Fill the missing data with the **same ones as in `src/.env`.**

In some cases you might need to run:
```bash
> dotnet restore
> dotnet clean
```

After that, go to `src` and run `docker compose up backend db`.

## Architecture

The API is built using **.NET 9.**, `ASP.NET` and uses a *MySQL* database.

The **Entity Framework Core** ORM is used in order to perform a code-first approach. Most of the time writing SQL queries by hand is not needed.

#### DB Entities

All database entities are defined in the `Models` folder following EF Core conventions. Every model needs to implement the `IDbEntity` interface.

