# Use this to transpile your latest changes to the model into the Migration class (e.g. public partial class Create : Migration)
Add-Migration -name 'Create' -OutputDir 'Data\Migrations'

# Use this to then apply the Migration classes to the actual database in the defaultconnection in the appsettings.json file of the MeyerInfrastructureApi project.
Update-Database