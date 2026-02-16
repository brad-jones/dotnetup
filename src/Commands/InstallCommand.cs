using System.CommandLine;
using System.Runtime.InteropServices;
using dotnetup.Services;
using dotnetup.Services.Archive;
using dotnetup.Services.Releases;
using Semver;

namespace dotnetup;

public class InstallCommand : Command
{
  public InstallCommand()
    : base("install")
  {
    Description = "Installs the given sdk version";

    var versionArg = this.Arg(
      "version-no",
      "The version number of the sdk to install.",
      defaultValueFactory: () => new string[] { ReleasesClient.GetLatestVersion().Result.ToString() }
    );

    var dotnetRootOpt = this.Opt(
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
    );

    SetAction(
      (parseResult, cancellationToken) =>
      {
        var sdkVersions = parseResult.GetValue(versionArg);
        var dotnetRoot = parseResult.GetValue(dotnetRootOpt);
        return Execute(sdkVersions!, dotnetRoot!);
      }
    );
  }

  private static async Task Execute(string[] sdkVersions, string dotnetRoot)
  {
    foreach (var v in sdkVersions)
    {
      var sdkVersion = new SemVersion(0);
      try
      {
        sdkVersion = SemVersion.Parse(v, SemVersionStyles.Any) ?? throw new Exception("invalid version");
      }
      catch
      {
        Console.Error.WriteLine($"❗❗❗ '{v}' is an invalid version number, see: https://semver.org/ ❗❗❗");
        Environment.Exit(1);
      }

      Console.Write($"🔎  searching in {dotnetRoot} for {sdkVersion}...  ");
      if (await DotnetCli.VersionInstalled(dotnetRoot, sdkVersion))
      {
        Console.WriteLine("already installed ✔️");
        continue;
      }
      Console.WriteLine("not found");

      string? file = null;
      SemVersion? resolvedVersion = null;
      try
      {
        (resolvedVersion, file) = await ReleasesClient.DownloadVersion(sdkVersion, dotnetRoot);
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

      Console.Write($"▶️  testing installation...  ");
      if (!await DotnetCli.VersionInstalled(dotnetRoot, resolvedVersion))
      {
        Console.Error.WriteLine($"❗❗❗ sdk install failed ❗❗❗");
        Environment.Exit(1);
      }
      Console.WriteLine("DONE ✔️");
      Console.WriteLine($"😄 {resolvedVersion} up");
    }
  }
}
