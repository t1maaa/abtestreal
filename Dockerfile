FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY abtestreal/bin/Release/net5.0/publish/ ./

ENTRYPOINT ["dotnet", "abtestreal.dll"]
