FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /repo

COPY PatchaWallet.Security.sln .
COPY src/Patcha.Security/Patcha.Security.csproj ./src/Patcha.Security/Patcha.Security.csproj

RUN dotnet restore

COPY . ./

RUN dotnet publish ./src/Patcha.Security/Patcha.Security.csproj -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

COPY --from=build /app/ /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "Patcha.Security.dll"]