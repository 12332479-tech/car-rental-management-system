# Car Rental Management System

A .NET 8 car-rental application composed of an ASP.NET Core Web API and a Windows Forms desktop client.

## Projects

- `CarRentalApi`: authentication, car inventory, reservations, Entity Framework Core, and Swagger
- `CarRentalWinForms`: desktop client for customer and administrator workflows

## Run locally

1. Copy `appsettings.example.json` to `CarRentalApi/appsettings.json` and adjust the local SQL Server connection.
2. Apply the Entity Framework migrations from `CarRentalApi`.
3. Start the API, then run the Windows Forms client.

The solution targets .NET 8.