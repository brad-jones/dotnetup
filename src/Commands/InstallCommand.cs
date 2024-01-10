using System.CommandLine;
using System.Formats.Tar;
using System.IO.Compression;
using System.Runtime.InteropServices;
using dotnetup.Services.Archive;
using dotnetup.Services.Releases;

namespace dotnetup;

public class InstallCommand : Command
{
    public InstallCommand()
        : base("install", "Installs the given sdk version")
    {
        this.SetHandler(
            Execute,
            this.Arg<string>(
                "version-no",
                "The version number of the sdk to install.",
                defaultValueFactory: () => ReleasesClient.GetLatestVersion().Result
            ),
            this.Opt<string>(
                "--dotnet-root",
                "The path to the DOTNET_ROOT",
                defaultValueFactory: () =>
                {
                    var rootFromEnv = Environment.GetEnvironmentVariable("DOTNET_ROOT");
                    if (!string.IsNullOrWhiteSpace(rootFromEnv))
                        return rootFromEnv;

                    var homePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                        ? Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")
                        : Environment.GetEnvironmentVariable("HOME");

                    return Path.Join(homePath, ".dotnet");
                }
            )
        );
    }

    private async Task Execute(string sdkVersion, string dotnetRoot)
    {
        var file = await ReleasesClient.DownloadVersion(sdkVersion);

        try
        {
            Console.WriteLine($"extracting...");
            Archive.ExtractToDirectory(file, dotnetRoot);
        }
        finally
        {
            File.Delete(file);
        }
    }
}
