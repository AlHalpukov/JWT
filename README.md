# JWT

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=abcDEF123098!" --name sql2019 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

dotnet ef migrations add <Migration_name> -s JWT.WebAPI -p JWT.DataAccess.MSSQL

dotnet ef database update -s JWT.WebAPI -p JWT.DataAccess.MSSQL