#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Fruits.API/Fruits.API.csproj", "src/Fruits.API/"]
COPY ["src/Fruits.Domain/Fruits.Domain.csproj", "src/Fruits.Domain/"]
COPY ["src/Fruits.Application/Fruits.Application.csproj", "src/Fruits.Application/"]
COPY ["src/Fruits.Infra.IoC/Fruits.Infra.IoC.csproj", "src/Fruits.Infra.IoC/"]
COPY ["src/Fruits.Infra.Data/Fruits.Infra.Data.csproj", "src/Fruits.Infra.Data/"]
RUN dotnet restore "src/Fruits.API/Fruits.API.csproj"
COPY . .
WORKDIR "/src/src/Fruits.API"
RUN dotnet build "Fruits.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fruits.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fruits.API.dll"]
