#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Address Book Pro/Address Book Pro.csproj", "Address Book Pro/"]
RUN dotnet restore "Address Book Pro/Address Book Pro.csproj"
COPY . .
WORKDIR "/src/Address Book Pro"
RUN dotnet build "Address Book Pro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Address Book Pro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Address Book Pro.dll"]