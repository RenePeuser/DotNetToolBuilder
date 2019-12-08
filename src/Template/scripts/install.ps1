function Install-Tool {
    dotnet tool install -g --add-source ./nupkg Trumpf.Hmi.Uif
}

if (-Not (Install-Tool)) {
    Write-Host "Installing tool failed, trying to uninstall first..."
    dotnet tool uninstall -g Trumpf.Hmi.Uif
    Install-Tool
}
