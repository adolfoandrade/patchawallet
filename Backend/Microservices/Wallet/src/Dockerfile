# STAGE01 - Build application and its dependencies
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-dev
WORKDIR /app
COPY PatchaWallet.Wallet/*.csproj ./
COPY . ./
RUN dotnet restore 

# STAGE02 - Publish the application
FROM build-dev AS publish
RUN dotnet publish -c Release -o /app

# STAGE03 - Create the final image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
LABEL Author="Gaurav Gahlot"
LABEL Maintainer="quickdevnotes"
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PatchaWallet.Wallet.dll", "--server.urls", "http://*:80"]