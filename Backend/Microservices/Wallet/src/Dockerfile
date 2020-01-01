FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-dev
WORKDIR /src
COPY ["PatchaWallet.Wallet/PatchaWallet.Wallet.csproj", "PatchaWallet.Wallet/"]
RUN dotnet restore "PatchaWallet.Wallet/PatchaWallet.Wallet.csproj"

FROM build-dev AS publish
RUN dotnet publish "PatchaWallet.Wallet/PatchaWallet.Wallet.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatchaWallet.Wallet.dll"]