# Car Rental Management System

A .NET 8 car-rental application composed of an ASP.NET Core Web API and a Windows Forms desktop client.

## Projects

- `CarRentalApi`: authentication, car inventory, reservations, Entity Framework Core, and Swagger
- `CarRentalWinForms`: desktop client for customer and administrator workflows

## Tech stack

ASP.NET Core Web API, .NET 8, C#, Entity Framework Core, SQL Server, Swagger, and Windows Forms.

## Architecture walkthrough

The API separates controllers, DTOs, repositories, services, models, and EF Core migrations. The Windows Forms client consumes the API through an API client service and exposes separate customer and administrator workflows.

## Run locally

1. Copy `appsettings.example.json` to `CarRentalApi/appsettings.json` and adjust the local SQL Server connection.
2. Apply the Entity Framework migrations from `CarRentalApi`.
3. Start the API, then run the Windows Forms client.

The solution targets .NET 8.

## Demo checklist

1. Start the API and open its Swagger endpoint.
2. Register or sign in from the Windows Forms client.
3. Browse cars, create a reservation, and review the administrator dashboard.