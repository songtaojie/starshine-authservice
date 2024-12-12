#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Starshine.Authservice.Web/Starshine.Authservice.Web.csproj", "/src/Starshine.Authservice.Web/"]
COPY ["src/Starshine.Authservice.Web.Extensions/Starshine.Authservice.Web.Extensions.csproj", "/src/Starshine.Authservice.Web.Extensions/"]
COPY ["src/Starshine.Authservice.Web.Model/Starshine.Authservice.Web.Model.csproj", "/src/Starshine.Authservice.Web.Model/"]
COPY ["src/Starshine.Authservice.Web.Common/Starshine.Authservice.Web.Common.csproj", "/src/Starshine.Authservice.Web.Common/"]

RUN dotnet restore "/src/Starshine.Authservice.Web/Starshine.Authservice.Web.csproj"
COPY . .
WORKDIR "/src/src/Starshine.Authservice.Web"
RUN dotnet build "Starshine.Authservice.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Starshine.Authservice.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Starshine.Authservice.Web.dll"]