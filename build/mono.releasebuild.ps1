# ====================================================================================================
# ROADKILL Mono release build script
#  
# See releasebuild.ps1 for what the script does - this script just builds using the MONO target.
# ====================================================================================================

$ErrorActionPreference = "Stop"
$zipFileName = "Roadkill.mono.2.0.zip"

# ---- Up to the root directory
cd ..

# ---- Add the tool paths to our path
$runtimeDir = [System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()
$env:Path = $env:Path + ";" +$runtimeDir
$env:Path = $env:Path + ";C:\Program Files (x86)\IIS\Microsoft Web Deploy V3"
$env:Path = $env:Path + ";C:\Program Files\7-Zip"

# ---- Make sure the roadkill.config,connectionstrings.config files are the download template one
copy -Force lib\Configs\roadkill.download.config src\Roadkill.Web\roadkill.config
copy -Force lib\Configs\connectionStrings.config src\Roadkill.Web\connectionStrings.config

# ---- Build the solution using the Mono target
msbuild roadkill.sln "/p:Configuration=Mono;DeployOnBuild=True;PackageAsSingleFile=False;AutoParameterizationWebConfigConnectionStrings=false;outdir=deploytemp\;OutputPath=bin\debug"

# ---- Use msdeploy to publish the website to disk
$currentDir = $(get-location).toString()
$packageSource = $currentDir +"\src\roadkill.Web\obj\Mono\Package\PackageTmp\"
$packageDest = $currentDir + "\_WEBSITE"
msdeploy -verb:sync -source:contentPath=$packageSource -dest:contentPath=$packageDest

# ---- Copy licence
copy -Force textfiles\licence.txt _WEBSITE\

# ---- Delete files that don't work on Apache/Mono
del _WEBSITE\bin\System.Web.dll
del _WEBSITE\bin\Microsoft.Web.Infrastructure.dll
del _WEBSITE\bin\Microsoft.Web.Administration.dll

# ---- Add Lightspeed files that are Mono specific
copy -Force lib\LightSpeed\Mindscape.LightSpeed.MetaData.dll _WEBSITE\bin
copy -Force lib\LightSpeed\Providers\log4net.dll _WEBSITE\bin
copy -Force lib\LightSpeed\Providers\Memcached.ClientLibrary.dll _WEBSITE\bin

# ---- Apache config
copy -Force lib\Configs\apache.txt _WEBSITE\

# ---- Zip up the folder (requires 7zip)
CD _WEBSITE
7z a $zipFileName
copy $zipFileName ..\$zipFileName
CD ..

# ---- Clean up the temporary deploy folders
Remove-Item -Force -Recurse _WEBSITE
Remove-Item -Force -Recurse src\Roadkill.Core\deploytemp
Remove-Item -Force -Recurse src\Roadkill.Web\deploytemp
Remove-Item -Force -Recurse src\Roadkill.Tests\deploytemp

"Mono release build complete."