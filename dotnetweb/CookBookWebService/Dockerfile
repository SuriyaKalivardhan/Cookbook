# Stage 1: Build & Publish the Web API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 80 explicitly
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "CookBookWebService.dll"]