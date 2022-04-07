# Bookkeeper
Expense and profit tracker built for the needs of a bakery with multiple storefronts.  Built using the ABP.io framework.

With this application you can do CRUD operations with:
- expense types
- expenses
- employees
- shops (storefronts)
- daily earnings
- daily cash flow
- monthly cash flow

Usecase and model diagrams are found inside of the TulumbaDijagrami.drawio file.

## Startup
Admin user default credentials:
- admin
- 1q2w3E*
### Docker-compose
To start with docker-compose cd into the root directory of the application and:
`sudo docker-compose build`
`sudo docker-compose up`

The application will be available on localhost:8082 and the api will be on localhost:8083.

### With dotnet and npm/yarn
Requires .NET 5 SDK and runtime, and npm with node version 14.18.0.
#### API
Will also require a PostgreSQL database whose connection string will have to be added to the DbMigrator and HttpApiHost appsettings.json files.
After adding the connection string to the DbMigrator project, run `dotnet run` inside of the root directory of the DbMigrator project to migrate and seed the database.

After migrating the database start the HttpApiHost project, from its root directory, with `dotnet run`, the API will run on localhost:44389. This will run the application in debug mode.
To release for production run `dotnet publish -c Release` and copy the directory with the DLLs.
#### App
Inside of the angular project first install the dependencies with `npm i` and then run the application in the development mode with `npm run start`