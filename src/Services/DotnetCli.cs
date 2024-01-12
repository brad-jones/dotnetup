using System.Runtime.InteropServices;
using Medallion.Shell;
using Semver;

namespace dotnetup.Services;

public static class DotnetCli
{
    public static async Task<bool> VersionInstalled(string dotnetRoot, SemVersion version)
    {
        var isWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        var dotnetExe = LinkResolver.Realpath(Path.Combine(dotnetRoot, $"dotnet{(isWin ? ".exe" : "")}"));
        if (!File.Exists(dotnetExe))
            return false;

        var infoResult = await Command.Run(dotnetExe, ["--info"]).Task;
        if (infoResult.ExitCode != 0)
            return false;

        var resolvedDotnetRoot = Path.GetDirectoryName(dotnetExe)!;
        var searchString = $"{version} [{Path.Combine(resolvedDotnetRoot, "sdk")}]";
        return infoResult.StandardOutput.Contains(searchString);
    }
}
