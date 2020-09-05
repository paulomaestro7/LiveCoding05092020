#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Live.Caqui.WebApi/Live.Caqui.WebApi.csproj", "Live.Caqui.WebApi/"]
COPY ["Live.Coding.Caqui.Model/Live.Coding.Caqui.Model.csproj", "Live.Coding.Caqui.Model/"]
RUN dotnet restore "Live.Caqui.WebApi/Live.Caqui.WebApi.csproj"
COPY . .
WORKDIR "/src/Live.Caqui.WebApi"
RUN dotnet build "Live.Caqui.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Live.Caqui.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Live.Caqui.WebApi.dll"]