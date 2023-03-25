$basePath = "I:\military service\MoaMoh\PriceAdjustmentApp\PA"
Set-Location -Path $basePath
# remove previous database file
$empty_db = ".\database\pa.sqlite"
IF (Test-Path $empty_db) { Remove-Item -Path $empty_db }

# create the database again. this one has only the structure and schema
Set-Location -Path "DbMigratorJob"
dotnet ef database update 

# fill database with data
Set-Location -Path ".."
dotnet clean
IF (Test-Path ".\DbHolder\pa.sqlite" ) { Remove-Item -Path ".\DbHolder\pa.sqlite" }
Move-Item -Path database/pa.sqlite -Destination DbHolder/pa.sqlite
Set-Location ".\PopulateDbJob"
dotnet build
Set-Location -Path ".\bin\Debug\net7.0"
.\PopulateDbJob.exe
$filled_db = Resolve-Path "pa.sqlite"
Set-Location -Path $basePath
IF (Test-Path ".\DbHolder\pa.sqlite" ) { Remove-Item -Path ".\DbHolder\pa.sqlite" }
Move-Item -Path $filled_db -Destination "DbHolder/pa.sqlite"
dotnet clean
dotnet build
