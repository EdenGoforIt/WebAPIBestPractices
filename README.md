# index

146

## use sql from mac

sudo docker pull mcr.microsoft.com/mssql/server:2022-latest

docker run -d --name sqlexpress -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=reallyStrongPwd123' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest

docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=reallyStrongPwd123" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/azure-sql-edge

# dotnet command for mac

dotnet ef database update --verbose -s ../WebApi-BestPractices
dotnet ef migrations -s ../WebApi-BestPractices add ChangeGuidToIDs --verbose;
dotnet ef migrations remove -s ../WebApi-BestPractices

# how to run

- open the `Docker` desktop and run sql express
- go to the webapi-bestpractices by `cd webapi-bestpractices`
- dotnet run

or just
F5
