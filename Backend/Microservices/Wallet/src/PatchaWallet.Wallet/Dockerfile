FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PatchaWallet.Wallet.csproj", "PatchaWallet.Wallet/"]
RUN dotnet restore "src/PatchaWallet.Wallet/PatchaWallet.Wallet.csproj"
COPY . .
WORKDIR "/src/PatchaWallet.Wallet"
RUN dotnet build "PatchaWallet.Wallet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PatchaWallet.Wallet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatchaWallet.Wallet.dll"]