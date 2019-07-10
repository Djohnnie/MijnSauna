FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base

# Set the working directory
WORKDIR /app

# Copy the app
COPY bin/Release/publish /app

EXPOSE 5000
EXPOSE 5001

ENV TZ=Europe/Brussels
ENV CONNECTIONSTRING=...
ENV CERTIFICATE_FILENAME=...
ENV CERTIFICATE_PASSWORD=...

# Start the app
ENTRYPOINT ["dotnet", "MijnSauna.Backend.Api.dll"]