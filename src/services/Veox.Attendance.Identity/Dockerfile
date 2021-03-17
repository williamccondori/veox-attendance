FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY Veox.Attendance.Identity.Api/*.csproj Veox.Attendance.Identity.Api/
COPY Veox.Attendance.Identity.Application/*.csproj Veox.Attendance.Identity.Application/
COPY Veox.Attendance.Identity.Domain/*.csproj Veox.Attendance.Identity.Domain/
COPY Veox.Attendance.Identity.Infraestructure.Jwt/*.csproj Veox.Attendance.Identity.Infraestructure.Jwt/
COPY Veox.Attendance.Identity.Infraestructure.MongoDb/*.csproj Veox.Attendance.Identity.Infraestructure.MongoDb/
COPY Veox.Attendance.Identity.Infraestructure.RabbitMq/*.csproj Veox.Attendance.Identity.Infraestructure.RabbitMq/
COPY Veox.Attendance.Identity.IoC/*.csproj Veox.Attendance.Identity.IoC/

RUN dotnet restore Veox.Attendance.Identity.Api

COPY . ./
RUN dotnet publish ./Veox.Attendance.Identity.Api/Veox.Attendance.Identity.Api.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TZ America/Lima
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Veox.Attendance.Identity.Api.dll"]