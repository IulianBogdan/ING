FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY *.sln ./
COPY ChallengeING.Api/ChallengeING.Api.csproj/
COPY . .
WORKDIR /src/ChallengeING.Api
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test /src/.tests/ChallengeING.Tests/ChallengeING.Tests.csproj --list-tests

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChallengeING.dll"]