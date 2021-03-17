FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln ./

COPY Veox.Attendance.Record.Api/*.csproj Veox.Attendance.Record.Api/
COPY Veox.Attendance.Record.Application/*.csproj Veox.Attendance.Record.Application/
COPY Veox.Attendance.Record.Domain/*.csproj Veox.Attendance.Record.Domain/
COPY Veox.Attendance.Record.Infraestructure.MongoDb/*.csproj Veox.Attendance.Record.Infraestructure.MongoDb/
COPY Veox.Attendance.Record.IoC/*.csproj Veox.Attendance.Record.IoC/

RUN dotnet restore

COPY . ./
RUN dotnet publish ./Veox.Attendance.Record.Api/Veox.Attendance.Record.Api.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TZ America/Lima
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Veox.Attendance.Record.Api.dll"]