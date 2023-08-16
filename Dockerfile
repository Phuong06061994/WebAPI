
FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /src
COPY . .
RUN dotnet restore 
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll","Web.dll"]