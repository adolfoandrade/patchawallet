FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["PatchaWallet.Wallet.csproj", "PatchaWallet.Wallet/"]
RUN dotnet restore "PatchaWallet.Wallet/PatchaWallet.Wallet.csproj"
COPY . .
WORKDIR "/src/PatchaWallet.Wallet"

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatchaWallet.Wallet.dll"]