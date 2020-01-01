FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /repo

COPY PatchaWallet.Wallet.sln .
COPY src/PatchaWallet.Wallet/PatchaWallet.Wallet.csproj ./src/PatchaWallet.Wallet/PatchaWallet.Wallet.csproj
COPY test/PatchaWallet.Wallet.UnitTest/PatchaWallet.Wallet.UnitTest.csproj ./test/PatchaWallet.Wallet.UnitTest/PatchaWallet.Wallet.UnitTest.csproj

RUN dotnet restore

COPY . ./

RUN dotnet publish ./src/PatchaWallet.Wallet/PatchaWallet.Wallet.csproj -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

COPY --from=build /app/ /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "PatchaWallet.Wallet.dll"]