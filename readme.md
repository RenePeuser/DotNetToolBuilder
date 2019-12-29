# DotNetTool.Builder

This DotNetTool.Builder will creates you a .NetTool based on the [System.CommandLine.Experimental](https://www.nuget.org/packages/System.CommandLine.Experimental/) package.

It will create, based on one or more cli expressions, all classes, options, arguments, middlewares, dependency-injection and services,
so that you only have to implement the logic for your specific command.

## Target

With this DotNetTool.Builder you should focus your work only to your Logic
of your defined cli commands. The hole framework around will be generated. 
With this builder your are able to build quick, fast and good maintainable CLI's.

## Quickguide to your new CLI
```
1. Install the DotNetToolBuilder 'dotnet tool install DotNetTool.Builder --global --version x.y.z'
2. Run 'dotnet newTool'
3. Insert all your expressions, and informations
4. Implement your logic to the created command hanlder which calls a created service for each command

   📦FindAndReplaceTool
    ┣ 📂..
    ┣ 📂RootCommand
    ┃ ┣ 📂SubCommand
    ┃ ┃ ┣ 📂Arguments
    ┃ ┃ ┃ ┣ .. 
    ┃ ┃ ┣ 📂Options
    ┃ ┃ ┃ ┣ .. 
    ┃ ┃ ┣ 📂Service
    ┃ ┃ ┃ ┣ 📜SubCommandService.cs  <= Implement your logic here !

5. Set up your package informations for your project
5. Test it.
6. Pack it.
7. Publish it.
```

## Install

![](./assets/dotnet-tool-builder-install.gif)

Hint if you do not use the "--version" option you get the latest version, but if it's a prerelease
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

```
Hint: If you need multiple expressions like: (see gif below !)

      tool list <file> --xyz
      tool update <file> --abc
      tool add <file> --now
      
      You have to do it one after one. After all infos for the first expression are collected
      you will be asked add another expression, say yes and you can add a secon, third one..    
```

![](./assets/dotnet-tool-builder.gif)


## Creation-Target
The command 'dotnet newtool' will create in the current execution directory a new folder
with your given project, all classes, options, arguments, middlewares, dependency-injection and services,

Based on the sample "FindAndReplaceTool"

```bash
📦D:\
┣ 📂Test
┃ ┣ 📂FindAndReplaceTool
┃ ┃ ┣ 📂src
┃ ┃ ┃ ┣ 📂FindAndReplaceTool
┃ ┃ ┃ ┃ ┣ 📂App
┃ ┃ ┃ ┃ ┃ ┣ ..
┃ ┃ ┃ ┃ ┣ 📂ErrorHandling
┃ ┃ ┃ ┃ ┃ ┣ ..
┃ ┃ ┃ ┃ ┣ 📂Fart
┃ ┃ ┃ ┃ ┃ ┣ 📂Findin
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📂Arguments
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜FindinArgumentBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜IFindinArgumentBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📂Options
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜FindinOptionsBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜IFindinOptionsBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📂Service
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜FindinService.cs
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜IFindinService.cs
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜FindinCommandBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜FindinParameters.cs
┃ ┃ ┃ ┃ ┃ ┣ 📂Replacein
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📂Arguments
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜IReplaceinArgumentBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜ReplaceinArgumentBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📂Options
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜IReplaceinOptionsBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜ReplaceinOptionsBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📂Service
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜IReplaceinService.cs
┃ ┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜ReplaceinService.cs
┃ ┃ ┃ ┃ ┃ ┃ ┣ 📜ReplaceinCommandBuilder.cs
┃ ┃ ┃ ┃ ┃ ┃ ┗ 📜ReplaceinParameters.cs
┃ ┃ ┃ ┃ ┃ ┣ 📜FartCommandBuilder.cs
┃ ┃ ┃ ┃ ┃ ┣ 📜IFartCommandBuilder.cs
┃ ┃ ┃ ┃ ┃ ┗ 📜IFartSubCommandBuilder.cs
┃ ┃ ┃ ┃ ┣ 📂Properties
┃ ┃ ┃ ┃ ┃ ┗ 📜launchSettings.json
┃ ┃ ┃ ┃ ┣ 📂Services
┃ ┃ ┃ ┃ ┃ ┣ 📜..
┃ ┃ ┃ ┃ ┣ 📜..
 ```
![](./assets/solution-command-structure.png)

## Parameter expression structure
```
tool parse <file>[FileInfo] --option <opt-arg>[string]

tool       = Root-Command
parse      = Root-SubCommand
<file>     = Argument for 'parse' command
[FileInfo] = Type casting for argument '<file>'.
--option   = Option for 'parse' command
<opt-arg>  = Argument for the option '--option'
[string]   = Type casting for argument '<opt-arg>'.
```
## Type-Casting

For each 'Argument' you can cast this argument to your needed type.
The cast could be written before or after your argument.

Post-Cast
```
<file>[FileInfo]
```

Pre-Cast
```
[string]<opt-arg>
```
### Sample for primitive type casting

```
tool fetch <file> --pattern <pattern>[string]
tool fetch <file> --count-numbers <count-numbers-value>[int]
```

### Sample for specific type casting

```
tool fetch <file>[System.IO.FileInfo] --all-numbers
tool fetch <directory>[System.IO.DirectoryInfo] --all-files
```

```
Hint: If you do not cast argument with specific types, the default will be
      'object'. 
      For types like 'FileInfo' or 'DirectoryInfo' you have to use full
      quilified type name like 'System.IO.FileInfo'. It will work also with
      FileInfo but your app will not compile because of the missing namespace
      thats all.
```

Then all the generated classes and services work directly with the expected type.

```
tool parse <file>[System.IO.FileInfo] --option <opt-arg>[string]
```
```csharp
public class ParseParameters
{
    public ParseParameters(System.IO.FileInfo file, string option)
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
    ...
    command.Handler = CommandHandler.Create<System.IO.FileInfo, string>((file, option) => _parseService.HandleAsync(new ParseParameters(file, option)));
    return command;
}
```

## Interpreting Options

### Option without argument

Any option without argument is a 'bool' option

```
tool parse <file>[System.IO.FileInfo] --is-json
```

```csharp
public class ParseParameters
{
    public ParseParameters(System.IO.FileInfo file, bool isJson)
    {
        File = file;
        IsJson = isJson;
    }

    public System.IO.FileInfo File { get; }
    public bool IsJson { get; }
}
```

```csharp
public Command Build()
{
    ...
    command.Handler = CommandHandler.Create<System.IO.FileInfo, bool>((file, option) => _parseService.HandleAsync(new ParseParameters(file, isJson)));
    return command;
}
```

### Option with argument
```
tool parse <file>[System.IO.FileInfo] --search-expression <search-expression-value>[string]
```

```csharp
public class ParseParameters
{
    public ParseParameters(System.IO.FileInfo file, string searchExpression)
    {
        File = file;
        SearchExpression = searchExpression;
    }

    public System.IO.FileInfo File { get; }
    public string SearchExpression { get; }
}
```

```csharp
public Command Build()
{
    ...
    command.Handler = CommandHandler.Create<System.IO.FileInfo, string>((file, option) => _parseService.HandleAsync(new ParseParameters(file, searchExpression)));
    return command;
}
```

### Option with argument but NO cast

```
tool parse <file>[System.IO.FileInfo] --search-expression <search-expression-value>
```

```csharp
public class ParseParameters
{
    public ParseParameters(System.IO.FileInfo file, object searchExpression)
    {
        File = file;
        SearchExpression = searchExpression;
    }

    public System.IO.FileInfo File { get; }
    public object SearchExpression { get; }
}
```

```csharp
public Command Build()
{
    ...
    command.Handler = CommandHandler.Create<System.IO.FileInfo, object>((file, option) => _parseService.HandleAsync(new ParseParameters(file, searchExpression)));
    return command;
}
```