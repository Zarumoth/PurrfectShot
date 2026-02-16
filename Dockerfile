# Build Stage - Use SDK, in order to compile the code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj files (faster Restore)
COPY ["PurrfectShot.Web/PurrfectShot.Web.csproj", "PurrfectShot.Web/"]
COPY ["PurrfectShot.Data/PurrfectShot.Data.csproj", "PurrfectShot.Data/"]
COPY ["PurrfectShot.Data.Models/PurrfectShot.Data.Models.csproj", "PurrfectShot.Data.Models/"]
COPY ["PurrfectShot.Services.Data/PurrfectShot.Services.Data.csproj", "PurrfectShot.Services.Data/"]
COPY ["PurrfectShot.Web.ViewModels/PurrfectShot.Web.ViewModels.csproj", "PurrfectShot.Web.ViewModels/"]
COPY ["PurrfectShot.Common/PurrfectShot.Common.csproj", "PurrfectShot.Common/"]

# Download Packages
RUN dotnet restore "PurrfectShot.Web/PurrfectShot.Web.csproj"

# Copy the code
COPY . .

# Билдваме
WORKDIR "/src/PurrfectShot.Web"
RUN dotnet build "PurrfectShot.Web.csproj" -c Release -o /app/build

# Publish (creates the final files)
FROM build AS publish
RUN dotnet publish "PurrfectShot.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Runtime Stage - Using lightweight Runtime Image for startup
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurrfectShot.Web.dll"]