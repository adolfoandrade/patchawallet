FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Patcha.Security.csproj", "Patcha.Security/"]
RUN dotnet restore "Patcha.Security/Patcha.Security.csproj"
COPY . .
WORKDIR "/src/Patcha.Security"
RUN dotnet build "Patcha.Security.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Patcha.Security.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patcha.Security.dll"]