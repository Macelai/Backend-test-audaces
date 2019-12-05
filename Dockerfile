FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 8000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY BackEndTest.sln ./
COPY BackEndTest.DataAccess/*.csproj ./BackEndTest.DataAccess/
COPY BackEndTest.Database/*.csproj ./BackEndTest.Database/
COPY BackEndTest.Types/*.csproj ./BackEndTest.Types/
COPY BackEndTest.Utilities/*.csproj ./BackEndTest.Utilities/
COPY BackEndTest.API/*.csproj ./BackEndTest.API/

RUN dotnet restore
COPY . .
WORKDIR /src/BackEndTest.DataAccess
RUN dotnet build -o /app

WORKDIR /src/BackEndTest.Database
RUN dotnet build -o /app

WORKDIR /src/BackEndTest.Types
RUN dotnet build -o /app

WORKDIR /src/BackEndTest.Utilities
RUN dotnet build -o /app

WORKDIR /src/BackEndTest.API
RUN dotnet build -o /app

FROM build AS publish
RUN dotnet publish -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY BackEndTest.API/backend-test.db .
ENTRYPOINT ["dotnet", "BackEndTest.API.dll"]


