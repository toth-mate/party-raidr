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

Fill the missing data with the **same ones as in `src/.env`.**

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

#### DB Entities

All database entities are defined in the `Models` folder following EF Core conventions. Every model needs to implement the `IDbEntity` interface.

