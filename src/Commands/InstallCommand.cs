using System.CommandLine;
using System.Runtime.InteropServices;
using dotnetup.Services.Archive;
using dotnetup.Services.Releases;
using Semver;
using Shell = Medallion.Shell.Command;

namespace dotnetup;

public class InstallCommand : Command
{
    public InstallCommand()
        : base("install", "Installs the given sdk version")
    {
        this.SetHandler(
            Execute,
            this.Arg<string[]>(
                "version-no",
                "The version number of the sdk to install.",
                defaultValueFactory: () => new string[] { ReleasesClient.GetLatestVersion().Result.ToString() }
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

    private async Task Execute(string[] sdkVersions, string dotnetRoot)
    {
        var isWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        var exe = Path.GetFullPath(Path.Join(dotnetRoot, $"dotnet{(isWin ? ".exe" : "")}"));

        foreach (var v in sdkVersions)
        {
            var sdkVersion = new SemVersion(0);
            try
            {
                sdkVersion = SemVersion.Parse(v, SemVersionStyles.Any) ?? throw new Exception("invalid version");
            }
            catch
            {
                Console.Error.WriteLine($"❗❗❗  '{v}' is an invalid version number, see: https://semver.org/  ❗❗❗");
                Environment.Exit(1);
            }

            Console.Write($"🔎  searching in {dotnetRoot} for {sdkVersion}...  ");
            if (File.Exists(exe))
            {
                var r = await Shell.Run(exe, ["--info"]).Task;
                var sdkString = $"{sdkVersion} [{Path.GetFullPath(Path.Join(dotnetRoot, "sdk"))}]";
                if (r.ExitCode == 0 && r.StandardOutput.Contains(sdkString))
                {
                    Console.WriteLine("already installed ✔️");
                    continue;
                }
            }
            Console.WriteLine("not found");

            string? file = null;
            string? resolvedVersion = null;
            try
            {
                (resolvedVersion, file) = await ReleasesClient.DownloadVersion(sdkVersion, exe);
                if (!string.IsNullOrWhiteSpace(file))
                {
                    Console.Write($"▶️  extracting to {dotnetRoot}...  ");
                    Archive.ExtractToDirectory(file, dotnetRoot);
                    Console.WriteLine("DONE ✔️");
                }
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(file))
                {
                    Console.Write($"▶️  cleaning up {file}...  ");
                    File.Delete(file);
                    Console.WriteLine("DONE ✔️");
                }
            }

            if (string.IsNullOrWhiteSpace(file))
                continue;

            if (!isWin)
                File.SetUnixFileMode(exe, UnixFileMode.UserExecute);

            Console.Write($"▶️  testing installation...  ");
            var result = await Shell.Run(exe, ["--info"], (opt) => { }).Task;
            if (result.ExitCode != 0)
            {
                Console.Error.WriteLine($"❗❗❗ sdk install failed ❗❗❗");
                Console.Error.WriteLine($"{result.StandardOutput}{result.StandardError}");
                Environment.Exit(1);
            }
            var sdkString2 = $"{resolvedVersion} [{Path.GetFullPath(Path.Join(dotnetRoot, "sdk"))}]";
            if (!result.StandardOutput.Contains(sdkString2))
            {
                Console.Error.WriteLine($"❗❗❗ sdk version mismatch ❗❗❗");
                Console.Error.WriteLine($"{result.StandardOutput}{result.StandardError}");
                Environment.Exit(1);
            }
            Console.WriteLine("DONE ✔️");
            Console.WriteLine($"😄 {resolvedVersion} up");
        }
    }
}
