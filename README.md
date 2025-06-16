# CustomerAndMatterManagementAPI
This project is a .NET 8 Web API for managing lawyers, customers, and legal matters. It uses Entity Framework Core with PostgreSQL, supports JWT authentication, and is CORS-enabled for frontend integration.
Lint to front-end repo: 

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- (Optional) [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [VS Code](https://code.visualstudio.com/)
- (Optional) [Docker](https://www.docker.com/) if you want to run PostgreSQL in a container


## Project Structure

- `CustomerAndMatterManagementAPI/` - Main Web API project
- `CustomerAndMatterData/` - Entity Framework Core models and context
- `CustomerAndMatterService/` - Business logic and service layer

## Migrations and Entity Framework
Migrations can be added via the Package Manager console in Visual Studio with the the targeet project set to CustomerAndMatterData following command
```
Add-Migration MyMigration
```

Migrations can be applied by running 
```
Update-Database
```

Commands can also be ran directly from the command line. 
First update your machine's version of Entity Framework Core .Net Command-line Tools via the follwoing command within the solution directory
```
dotnet tool update --global dotnet-ef
```
Make sure the version is compatiable with the run time version of Entity Framework in the project, currently 9.05.

To add a migration run the following command
```
dotnet ef migrations add MyMigration --project CustomerAndMatterData --startup-project CustomerAndMatterManagementAPI
```

To update the database run the following command
```
dotnet ef database update --project CustomerAndMatterData --startup-project CustomerAndMatterManagementAPI
```

**NOTE:** Seed data is defined CustomerAndMatterContext. The first time the database is updated it will seed the data for you. 

## API Authentication
Authenticate to hit api with the following steps: 
1. Authenticate by passing a valid LoginEmail and Email in a request body to the auth/login endpoint.
1. This endpoint will return a token to be used with bearer authentication in swagger.
1. Click on 'Authorize' on the swageer page and follow the instructions. 


## CORS
- The API allows requests from `http://localhost:3000` by default (see CORS policy in `Program.cs`).
- Update the CORS policy if your frontend runs on a different origin.

## TODO
The following is a list of things to be completed.
1. Seperate out quieres from the service layer.
2. Proper naming on DTOs
3. Password encryptions
4. Admin Roles
5. Firm management
6. Unit testing