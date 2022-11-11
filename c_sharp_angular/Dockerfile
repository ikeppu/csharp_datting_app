#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["c_sharp_angular/c_sharp_angular.csproj", "c_sharp_angular/"]
RUN dotnet restore "c_sharp_angular/c_sharp_angular.csproj"
COPY . .
WORKDIR "/src/c_sharp_angular"
RUN dotnet build "c_sharp_angular.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "c_sharp_angular.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "c_sharp_angular.dll"]
