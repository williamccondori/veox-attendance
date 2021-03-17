FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln ./

COPY Veox.Attendance.Workspace.Api/*.csproj Veox.Attendance.Workspace.Api/
COPY Veox.Attendance.Workspace.Application/*.csproj Veox.Attendance.Workspace.Application/
COPY Veox.Attendance.Workspace.Domain/*.csproj Veox.Attendance.Workspace.Domain/
COPY Veox.Attendance.Workspace.Infraestructure.MongoDb/*.csproj Veox.Attendance.Workspace.Infraestructure.MongoDb/
COPY Veox.Attendance.Workspace.IoC/*.csproj Veox.Attendance.Workspace.IoC/

RUN dotnet restore

COPY . ./
RUN dotnet publish ./Veox.Attendance.Workspace.Api/Veox.Attendance.Workspace.Api.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV TZ America/Lima
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Veox.Attendance.Workspace.Api.dll"]