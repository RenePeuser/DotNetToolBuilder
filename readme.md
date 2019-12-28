# DotNetTool.Builder

This dotnet tool helps to generate a cli which will be packed as tool.
A small wizard will guide you through the creation of your expected tool.

## Install

Hint if you do not use ther "--version" option you get the latest version, but if it's a prerelease
state you have to use the specific version otherwise, the tool will not be found.

Install the tool global
```bash 
dotnet tool install DotNetTool.Builder --global --version x.y.z
```

Install the tool local
```bash
dotnet tool install DotNetTool.Builder --version x.y.z
```
## How to use it

```bash
dotnet newtool
```

## Creation-Target
The command 'dotnet newtool' will create in the current execution directory a new folder
with your given project name

![](./assets/new-tool.png)

```bash
📦D:\
 ┣ 📂Test
 ┃ ┣ 📂New.Tool
 ┃ ┃ ┣ 📂src
 ┃ ┃ ┣ 📂...
 ```


![](./assets/dotnet-tool-builder-install.gif)


![](./assets/dotnet-tool-builder.gif)