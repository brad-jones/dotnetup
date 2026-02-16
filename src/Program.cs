using System.CommandLine;
using dotnetup;

var rootCommand = new RootCommand() { Description = "dotnet sdk version manager (ala: rustup)" };
rootCommand.Subcommands.Add(new InstallCommand());
var parseResult = rootCommand.Parse(args);
return await parseResult.InvokeAsync();
