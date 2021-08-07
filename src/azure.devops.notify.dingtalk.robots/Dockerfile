#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV TZ=Asia/Shanghai
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src
COPY src/azure.devops.notify.dingtalk.robots .
WORKDIR "/src/azure.devops.notify.dingtalk.robots"
RUN dotnet publish -c Release -o /app/publish -nologo -v:q -clp:NoSummary

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "azure.devops.notify.dingtalk.robots.dll"]