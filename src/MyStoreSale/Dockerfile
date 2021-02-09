FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ./src/MyStoreSale/MyStoreSale.csproj ./MyStoreSale/
COPY ./src/SharedKernel/SharedKernel.csproj ./SharedKernel/

RUN dotnet restore "MyStoreSale/MyStoreSale.csproj"
COPY . ./

FROM build AS publish
WORKDIR /src/MyStoreSale
RUN dotnet publish "MyStoreSale.csproj" -c Release -o /published --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /published .
ENTRYPOINT ["dotnet", "MyStoreSale.dll"]