# Allocella - Backend

This repository contains the **back-end** program of the Allocella app.

## Tech Stacks
- ASP.NET Core 8.0
- Implements REST API
- Entity Framework Core v8.0.0 (ORM)
- EF Core PostgreSQL Provider (database driver)
- EF Core Design Tools (for migrations)

## Commands Used Docummentation
1. ASP.NET WebAPI Initialization (Using v8.0)
```bash
dotnet new webapi -n AllocellaAPI -controllers -f net8.0
```
2. Installing EF Core v8.0.0
```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
```
3. Installing EF Core PostgreSQL Provider
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.0
```
4. Installing EF Core Design Tools
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
```

## Frequently Asked Questions (by myself)
- Q: How to connect the backend API with the database?
- A: I refer to [this tutorial](https://www.c-sharpcorner.com/article/building-a-powerful-asp-net-core-web-api-with-postgresql/) using EntityFrameworkCore (EF).