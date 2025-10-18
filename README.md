
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

