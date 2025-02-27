#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["WebCore/Spurious2/Spurious2.csproj", "WebCore/Spurious2/"]
COPY ["WebCore/Spurious2.Core2/Spurious2.Core2.csproj", "WebCore/Spurious2.Core2/"]
COPY ["WebCore/Spurious2.Infrastructure/Spurious2.Infrastructure.csproj", "WebCore/Spurious2.Infrastructure/"]
RUN dotnet restore "./WebCore/Spurious2/./Spurious2.csproj"
COPY . .
WORKDIR "/src/WebCore/Spurious2"
ENV NVM_DIR /usr/local/nvm
ENV NODE_VERSION v22.14.0
RUN mkdir -p /usr/local/nvm && apt-get update && echo "y" | apt-get install curl
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.1/install.sh | bash
RUN /bin/bash -c "source $NVM_DIR/nvm.sh && nvm install $NODE_VERSION && nvm use --delete-prefix $NODE_VERSION"
ENV NODE_PATH $NVM_DIR/versions/node/$NODE_VERSION/bin
ENV PATH $NODE_PATH:$PATH
RUN dotnet build "./Spurious2.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Spurious2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spurious2.dll"]
