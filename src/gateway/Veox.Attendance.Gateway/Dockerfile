FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln ./

COPY Veox.Attendance.Gateway.Api/*.csproj Veox.Attendance.Gateway.Api/

RUN dotnet restore

COPY . ./
RUN dotnet publish ./Veox.Attendance.Gateway.Api/Veox.Attendance.Gateway.Api.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TZ America/Lima
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Veox.Attendance.Gateway.Api.dll"]