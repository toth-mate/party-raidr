# PartyRaidR API

This page is meant to describe how the API works in order to make workflow unambiguous.

## Architecture

The API is built using **.NET 9.**, `ASP.NET` and uses a *MySQL* database.

The **Entity Framework Core** ORM is used in order to perform a code-first approach. Most of the time writing SQL queries by hand is not needed.