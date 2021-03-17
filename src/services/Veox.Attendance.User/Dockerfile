FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY Veox.Attendance.User.Api/*.csproj Veox.Attendance.User.Api/
COPY Veox.Attendance.User.Application/*.csproj Veox.Attendance.User.Application/
COPY Veox.Attendance.User.Domain/*.csproj Veox.Attendance.User.Domain/
COPY Veox.Attendance.User.Infraestructure.MongoDb/*.csproj Veox.Attendance.User.Infraestructure.MongoDb/
COPY Veox.Attendance.User.IoC/*.csproj Veox.Attendance.User.IoC/

RUN dotnet restore Veox.Attendance.User.Api

COPY . ./
RUN dotnet publish ./Veox.Attendance.User.Api/Veox.Attendance.User.Api.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TZ America/Lima
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Veox.Attendance.User.Api.dll"]