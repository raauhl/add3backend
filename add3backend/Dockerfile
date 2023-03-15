FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7654

ENV ASPNETCORE_URLS=http://+:7654

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["add3backend.csproj", "./"]
RUN dotnet restore "add3backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "add3backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "add3backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "add3backend.dll"]
