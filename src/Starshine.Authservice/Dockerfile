#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Starshine.Authservice/Starshine.Authservice.csproj", "/src/Starshine.Authservice/"]
COPY ["src/Starshine.Authservice.Extensions/Starshine.Authservice.Extensions.csproj", "/src/Starshine.Authservice.Extensions/"]
COPY ["src/Starshine.Authservice.Model/Starshine.Authservice.Model.csproj", "/src/Starshine.Authservice.Model/"]
COPY ["src/Starshine.Authservice.Common/Starshine.Authservice.Common.csproj", "/src/Starshine.Authservice.Common/"]

RUN dotnet restore "/src/Starshine.Authservice/Starshine.Authservice.csproj"
COPY . .
WORKDIR "/src/src/Starshine.Authservice"
RUN dotnet build "Starshine.Authservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Starshine.Authservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Starshine.Authservice.dll"]