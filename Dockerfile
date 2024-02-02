# Use the official .NET SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV TZ="America/Sao_Paulo"
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Set the working directory
WORKDIR /src

COPY  . .

RUN dotnet build "./Empreendimento.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./Empreendimento.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=publish /app .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Empreendimento.dll"]
