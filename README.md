# HelloAPITest
HelloAPITest -> Build ASP.NET CORE WEB API with SQL Server

#Initian HelloAPITest First time
- Install SQL Server
- to config server go to file "appsettings.json" and change "ConnectionStrings"
- Use migration to generate database
  dotnet ef migrations add InitialCreate
  dotnet ef database update
