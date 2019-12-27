FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR ./
COPY publish/TenancyPlatform.dll ./
ENTRYPOINT ["dotnet", "TenancyPlatform.dll"]