FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Simon_Test/Simon_Test.csproj", "Simon_Test/"]
RUN dotnet restore "Simon_Test/Simon_Test.csproj"
COPY . .
WORKDIR "/src/Simon_Test"
RUN dotnet build "Simon_Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simon_Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simon_Test.dll"]
