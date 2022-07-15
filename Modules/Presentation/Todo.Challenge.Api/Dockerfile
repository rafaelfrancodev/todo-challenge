#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Modules/Presentation/Todo.Challenge.Api/Todo.Challenge.Api.csproj", "Modules/Presentation/Todo.Challenge.Api/"]
RUN dotnet restore "Modules/Presentation/Todo.Challenge.Api/Todo.Challenge.Api.csproj"
COPY . .
WORKDIR "/src/Modules/Presentation/Todo.Challenge.Api"
RUN dotnet build "Todo.Challenge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.Challenge.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Challenge.Api.dll"]