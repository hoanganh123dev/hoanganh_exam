# Đồ án chuyên ngành
project domain using DDD,CQRL, Mongo DB and Identity Server 4
công nghệ và phần mềm sử dụng
1 VS code
2 Docker
3 Sql server
4 Mongo Db
5 Identity Server 4
6 DDD
7 Clean architecture
# Application URl
- Identity STS: https://localhost:5001
- Exam API: https://localhost:5002
- Exam Admin: https://localhost:6001
- Exam Portal: https://localhost:6002
- Identity Admin: https://localhost:6003
--clone quickstart UI: iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/main/getmain.ps1'))

Packages References
https://github.com/serilog/serilog/wiki/Getting-Started
https://github.com/IdentityServer/IdentityServer4.Quickstart.UI
https://mudblazor.com/
https://github.com/Garderoben/MudBlazor.Templates

Local secret
Exam API
{ "DatabaseSettings": { "Server": "localhost:27017", "DatabaseName": "ExamDb", "User": "admin", "Password": "Admin%40123%24" }, "IdentityUrl": "https://localhost:5001" }

Identity.Admin
{ "ConnectionStrings": { "ConfigurationDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "PersistedGrantDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "IdentityDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "AdminLogDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "AdminAuditLogDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "DataProtectionDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true" }, "AdminConfiguration": { "IdentityAdminRedirectUri": "https://localhost:6003/signin-oidc", "IdentityServerBaseUrl": "https://localhost:5001", } }

Identity.STS
{ "ConnectionStrings": { "ConfigurationDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "PersistedGrantDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "IdentityDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true", "DataProtectionDbConnection": "Server=.;Database=IdentityDb;Trusted_Connection=True;MultipleActiveResultSets=true" } }