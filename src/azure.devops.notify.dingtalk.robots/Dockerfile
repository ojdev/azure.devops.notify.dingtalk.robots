#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0.8 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.302 AS build
WORKDIR /src
COPY ["src/azure.devops.notify.dingtalk.robots/azure.devops.notify.dingtalk.robots.csproj", "src/azure.devops.notify.dingtalk.robots/"]
RUN dotnet restore "src/azure.devops.notify.dingtalk.robots/azure.devops.notify.dingtalk.robots.csproj"
COPY . .
WORKDIR "/src/src/azure.devops.notify.dingtalk.robots"
RUN dotnet build "azure.devops.notify.dingtalk.robots.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "azure.devops.notify.dingtalk.robots.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "azure.devops.notify.dingtalk.robots.dll"]