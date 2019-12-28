# DotNetTool.Builder

This dotnet tool helps to generate a cli which will be packed as tool.
A small wizard will guide you through the creation of your expected tool.

## Install

![](./assets/dotnet-tool-builder-install.gif)

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

![](./assets/dotnet-tool-builder.gif)

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

## With Type-Casting

![](./assets/parameter-expression-with-cast.png)

You are able to define the type for any argument with a type cast '[bool]' or '[System.IO.FileInfo]'.
The cast could be written before or after your argument.

Hint: If you do not define argument types the default will be 'object'

Then all the generated classes and services work direct with the expected type.

Sample: (Result from the 'newTool' result)
```csharp
public class ParseParameters
{
    public ParseParameters(System.IO.FileInfo file, bool option)
    {
        File = file;
        Option = option;
    }

    public System.IO.FileInfo File { get; }
    public string Option { get; }
}
```

```csharp
public Command Build()
{
    var command = new Command("parse", "Parse the given file or file path");
    _optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
    command.AddArgument(_argumentBuilder.Build());
    command.Handler = CommandHandler.Create<System.IO.FileInfo, bool>((file, option) => _parseService.HandleAsync(new ParseParameters(file, option)));
    return command;
}
```