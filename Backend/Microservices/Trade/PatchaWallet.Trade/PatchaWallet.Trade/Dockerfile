FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PatchaWallet.Trade/PatchaWallet.Trade.csproj", "PatchaWallet.Trade/"]
RUN dotnet restore "PatchaWallet.Trade/PatchaWallet.Trade.csproj"
COPY . .
WORKDIR "/src/PatchaWallet.Trade"
RUN dotnet build "PatchaWallet.Trade.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PatchaWallet.Trade.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatchaWallet.Trade.dll"]