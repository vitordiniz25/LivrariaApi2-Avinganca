#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Livraria2.Api/Livraria2.Api.csproj", "Livraria2.Api/"]
RUN dotnet restore "Livraria2.Api/Livraria2.Api.csproj"
COPY . .
WORKDIR "/src/Livraria2.Api"
RUN dotnet build "Livraria2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Livraria2.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Livraria2.Api.dll"]