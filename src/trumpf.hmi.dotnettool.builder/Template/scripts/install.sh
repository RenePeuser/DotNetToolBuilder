#!/bin/bash

install-tool() {
    dotnet tool install -g --add-source ./nupkg Trumpf.Hmi.Uif
}

install-tool && exit 0;

echo "Installing tool failed, trying to uninstall first..."
dotnet tool uninstall -g Trumpf.Hmi.Uif && install-tool
