#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Hx.IdentityServer/Hx.IdentityServer.csproj", "Hx.IdentityServer/"]
COPY ["src/Hx.IdentityServer.Extensions/Hx.IdentityServer.Extensions.csproj", "Hx.IdentityServer.Extensions/"]
COPY ["src/Hx.IdentityServer.Model/Hx.IdentityServer.Model.csproj", "Hx.IdentityServer.Model/"]
COPY ["src/Hx.IdentityServer.Common/Hx.IdentityServer.Common.csproj", "Hx.IdentityServer.Common/"]
RUN dotnet restore "src/Hx.IdentityServer/Hx.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Hx.IdentityServer"
RUN dotnet build "Hx.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hx.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hx.IdentityServer.dll"]