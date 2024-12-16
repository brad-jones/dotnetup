using System.CommandLine;
using dotnetup;

var rootCommand = new RootCommand("dotnet sdk version manager (ala: rustup)");
rootCommand.AddCommand(new InstallCommand());
return await rootCommand.InvokeAsync(args);
