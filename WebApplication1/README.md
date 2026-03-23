# My first C# project 

### goal:
- REST API built with .NET Core
- visualized with Swagger
- db connection with postgress using EF core and Docker

## run the projekt:
- run docker with `docker compose up -d --build`
- open `http://localhost:8080/index.html`

## Update
- for rebuild use `docker compose down `
- run docker again with `docker compose up -d --build`
- update database with `dotnet ef database update`
- (initialized with `dotnet ef migrations add InitialCreate`)