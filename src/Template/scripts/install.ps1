function Install-Tool {
    dotnet tool install -g --add-source ./nupkg rps.template
}

if (-Not (Install-Tool)) {
    Write-Host "Installing tool failed, trying to uninstall first..."
    dotnet tool uninstall -g rps.template
    Install-Tool
}
