#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TomiSoft.ProductCatalog.Server/TomiSoft.ProductCatalog.Server.csproj", "TomiSoft.ProductCatalog.Server/"]
COPY ["TomiSoft.ProductCatalog.Data.Sqlite/TomiSoft.ProductCatalog.Data.Sqlite.csproj", "TomiSoft.ProductCatalog.Data.Sqlite/"]
COPY ["TomiSoft.ProductCatalog/TomiSoft.ProductCatalog.csproj", "TomiSoft.ProductCatalog/"]
COPY ["TomiSoft.ProductCatalog.Server.OpenApiGenerated/TomiSoft.ProductCatalog.Server.OpenApiGenerated.csproj", "TomiSoft.ProductCatalog.Server.OpenApiGenerated/"]
RUN dotnet restore "TomiSoft.ProductCatalog.Server/TomiSoft.ProductCatalog.Server.csproj"
COPY . .
WORKDIR "/src/TomiSoft.ProductCatalog.Server"
RUN dotnet build "TomiSoft.ProductCatalog.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TomiSoft.ProductCatalog.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TomiSoft.ProductCatalog.Server.dll", "http://0.0.0.0:80"]