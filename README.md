Add-Migration CleanIdentity_Y -Project Infrastructure -OutputDir Persistence\Migrations

##ejeuctra test
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:Application.Tests\TestResults\**\coverage.cobertura.xml  -targetdir:coveragereport   -reporttypes:Html