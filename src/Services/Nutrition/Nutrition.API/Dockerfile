#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Nutrition/Nutrition.API/Nutrition.API.csproj", "Services/Nutrition/Nutrition.API/"]
COPY ["Services/Nutrition/Nutrition.Application/Nutrition.Application.csproj", "Services/Nutrition/Nutrition.Application/"]
COPY ["Services/Nutrition/Nutrition.Domain/Nutrition.Domain.csproj", "Services/Nutrition/Nutrition.Domain/"]
COPY ["Services/Nutrition/Nutrition.Infrastructure/Nutrition.Infrastructure.csproj", "Services/Nutrition/Nutrition.Infrastructure/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Services/Nutrition/Nutrition.API/Nutrition.API.csproj"
COPY ./Services/Nutrition/ ./Services/Nutrition/
COPY ./Shared ./Shared
WORKDIR "/src/Services/Nutrition/Nutrition.API"
RUN dotnet build "Nutrition.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nutrition.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nutrition.API.dll"]