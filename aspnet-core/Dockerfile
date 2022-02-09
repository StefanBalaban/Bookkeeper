#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 44389
 ENV ASPNETCORE_URLS=http://+:44389
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY ["*.sln", "."]

COPY ["./src/Tulumba.Application/Tulumba.Application.csproj", "src/Tulumba.Application/"]
COPY ["./src/Tulumba.Application.Contracts/Tulumba.Application.Contracts.csproj", "src/Tulumba.Application.Contracts/"]
COPY ["./src/Tulumba.DbMigrator/Tulumba.DbMigrator.csproj", "src/Tulumba.DbMigrator/"]
COPY ["./src/Tulumba.Domain/Tulumba.Domain.csproj", "src/Tulumba.Domain/"]
COPY ["./src/Tulumba.Domain.Shared/Tulumba.Domain.Shared.csproj", "src/Tulumba.Domain.Shared/"]
COPY ["./src/Tulumba.EntityFrameworkCore/Tulumba.EntityFrameworkCore.csproj", "src/Tulumba.EntityFrameworkCore/"]
COPY ["./src/Tulumba.HttpApi/Tulumba.HttpApi.csproj", "src/Tulumba.HttpApi/"]
COPY ["./src/Tulumba.HttpApi.Client/Tulumba.HttpApi.Client.csproj", "src/Tulumba.HttpApi.Client/"]
COPY ["./src/Tulumba.HttpApi.Host/Tulumba.HttpApi.Host.csproj", "src/Tulumba.HttpApi.Host/"]
COPY ["./test/Tulumba.Application.Tests/Tulumba.Application.Tests.csproj", "test/Tulumba.Application.Tests/"]
COPY ["./test/Tulumba.EntityFrameworkCore.Tests/Tulumba.EntityFrameworkCore.Tests.csproj", "test/Tulumba.EntityFrameworkCore.Tests/"]
COPY ["./test/Tulumba.TestBase/Tulumba.TestBase.csproj", "test/Tulumba.TestBase/"]
COPY ["./test/Tulumba.Domain.Tests/Tulumba.Domain.Tests.csproj", "test/Tulumba.Domain.Tests/"]
COPY ["./test/Tulumba.HttpApi.Client.ConsoleTestApp/Tulumba.HttpApi.Client.ConsoleTestApp.csproj", "test/Tulumba.HttpApi.Client.ConsoleTestApp/"]

RUN dotnet restore "Tulumba.sln"

COPY . .
WORKDIR "/src/Tulumba.HttpApi.Host"
RUN dotnet build "Tulumba.HttpApi.Host.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "Tulumba.HttpApi.Host.csproj" -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tulumba.HttpApi.Host.dll"]