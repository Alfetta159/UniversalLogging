# Script allows convenient publishing of NuGet packages from Visual Studio
# Match the version number in each line to match the version number in the csproj files for each NuGet package.

param (
    [string]$buildconfiguration = "Debug"
 )
 
function Update-Package ([string]$package,[string]$version,[string]$path)
{
	#dotnet nuget delete $package $version -s https://meyernuget.azurewebsites.net -k C7331B09-BAFD-4AEE-96BC-EB8233B3342E --non-interactive
	dotnet nuget push "$path\bin\$buildconfiguration\$package.$version.nupkg" -s https://meyernuget.azurewebsites.net -k C7331B09-BAFD-4AEE-96BC-EB8233B3342E --skip-duplicate
}

Update-Package -package "Meyer.Logging.Client" -version "1.0.0-preview-8" -path "LoggingClient"
