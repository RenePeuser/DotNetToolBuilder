# UIF CLI

## Install

Make sure you have .NET Core SDK 3.0 or higher installed on your machine. Then run

```bash
dotnet tool install -g Trumpf.Hmi.Uif
```

*Note: You need a `NuGet.Config` pointing to the Artifactory. If you don't have one, try the following:*

```bash
dotnet tool install -g Trumpf.Hmi.Uif --add-source https://srv01af2.corp.trumpf.com/artifactory/api/nuget/nuget-libs-release
```

## Run

```bash
uif --help
```

and enjoy.

## Develop

### Prerequisites

* VS Code
* VS 2019 or higher
* .NET Core 3.0 SDK or higher

### Get started

Open in VS Code, run the task `pack and install` and enjoy the glory.

*Note: To run tasks in VS Code, press* <kbd>CTRL</kbd>+<kbd>⇧</kbd>+<kbd>P</kbd>*, enter `Run` and select `Tasks: Run Task`.*
