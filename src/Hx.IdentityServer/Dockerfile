#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Hx.IdentityServer/Hx.IdentityServer.csproj", "/src/Hx.IdentityServer/"]
COPY ["src/Hx.IdentityServer.Extensions/Hx.IdentityServer.Extensions.csproj", "/src/Hx.IdentityServer.Extensions/"]
COPY ["src/Hx.IdentityServer.Model/Hx.IdentityServer.Model.csproj", "/src/Hx.IdentityServer.Model/"]
COPY ["src/Hx.IdentityServer.Common/Hx.IdentityServer.Common.csproj", "/src/Hx.IdentityServer.Common/"]

RUN dotnet restore "/src/Hx.IdentityServer/Hx.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Hx.IdentityServer"
RUN dotnet build "Hx.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hx.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hx.IdentityServer.dll"]