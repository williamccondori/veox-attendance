FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

COPY Veox.Attendance.Gateway/*.csproj Veox.Attendance.Gateway/

RUN dotnet restore Veox.Attendance.Gateway

COPY . ./
RUN dotnet publish ./Veox.Attendance.Gateway/Veox.Attendance.Gateway.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TZ America/Lima
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Veox.Attendance.Gateway.dll"]