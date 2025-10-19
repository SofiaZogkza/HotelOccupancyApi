
A clean architecture .NET API for managing hotels, travellers, rooms, and travel groups.

## Project Structure

- **Api/**: Controllers & middleware
- **Application/**: Services & interfaces
- **Domain/**: Entities, Value Objects, exceptions
- **Persistence/**: EF Core DbContext & repositories
- **Infrastructure/**: External services
- **Tests/**: Unit & integration tests

## Prerequisites

- .NET 8 SDK
- PostgreSQL (or Docker)
- Suggested: Docker

## Setup

1. Clone the repo
2. Setup PostgreSQL: `docker run --name hotel-postgres -e POSTGRES_PASSWORD=Pass123! -e POSTGRES_DB=hotel_db -p 5432:5432 -d postgres`
3. Update `appsettings.json` connection string
4. Apply EF Core migrations: `dotnet ef database update`

### Ensure you have .NET 8 SDK installed:

`dotnet --version`


### Install EF Core CLI globally (if not installed):

`dotnet tool install --global dotnet-ef`


### Restore required NuGet packages:

`dotnet restore`

### Environment Variables

The project uses a `.env` file at the solution root for DB configuration.
You may find the .env.example with empty sample. Remove the .example extension and fill in the empty variables.
Note: The design-time DbContext factory loads this .env file for migrations.

### Database Migrations

Create migrations:

`cd HotelOccupancy.Persistence
dotnet ef migrations add InitialCreated`

Apply migrations to the database:

`dotnet ef database update`


