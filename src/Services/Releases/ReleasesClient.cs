using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using dotnetup.Services.Releases.DTOs;
using Semver;
using Shell = Medallion.Shell.Command;

namespace dotnetup.Services.Releases;

public class ReleasesClient
{
    private static HttpClient Client => new();

    public static readonly int[] MajorVersions = [8, 7, 6];

    public static readonly string FileExt = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".tar.gz" : ".zip";

    public static string ReleaseNotesUrl(int v) =>
        $"https://raw.githubusercontent.com/dotnet/core/main/release-notes/{v}.0/releases.json";

    public static Task<ReleaseNotes?> GetReleaseNotes(int v, CancellationToken cancel = default) =>
        Client.GetFromJsonAsync(ReleaseNotesUrl(v), SrcGenCtx.Default.ReleaseNotes, cancel);

    public static async Task<SemVersion> GetLatestVersion(CancellationToken cancel = default)
    {
        var notes = await GetReleaseNotes(MajorVersions[0], cancel);
        return SemVersion.Parse(notes?.Releases.FirstOrDefault()?.Sdk.Version ?? "0.0.0", SemVersionStyles.Any);
    }

    public static async Task<Tuple<SemVersion, ReleaseNoteSdkFile>> GetVersion(
        SemVersion version,
        CancellationToken cancel = default
    )
    {
        var notes = await GetReleaseNotes((int)version.Major, cancel);

        ReleaseNoteSdk? release = null;
        if (version.Minor == 0 && version.Patch == 0)
        {
            release = notes!.Releases.First().Sdk;
        }
        else
        {
            release = notes!
                .Releases.Single(_ => SemVersion.Parse(_.Sdk.Version, SemVersionStyles.Any).Equals(version))
                .Sdk;
        }

        return Tuple.Create(
            SemVersion.Parse(release.Version, SemVersionStyles.Any),
            release.Files.Single(_ => _.Rid == RuntimeInformation.RuntimeIdentifier && _.Name.EndsWith(FileExt))
        );
    }

    public static async Task<Tuple<SemVersion, string>> DownloadVersion(
        SemVersion version,
        string dotnetRoot,
        CancellationToken cancel = default
    )
    {
        Console.Write($"▶️  looking up release notes for {version}...  ");
        var (resolvedVersion, download) = await GetVersion(version, cancel);
        if (!version.Equals(resolvedVersion))
        {
            if (await DotnetCli.VersionInstalled(dotnetRoot, resolvedVersion))
            {
                Console.WriteLine($"resolved({resolvedVersion}) - already installed ✔️");
                return Tuple.Create(resolvedVersion, string.Empty);
            }

            Console.WriteLine($"resolved({resolvedVersion}) ✔️");
        }
        else
        {
            Console.WriteLine($"DONE ✔️");
        }

        var filePath = Path.Combine(Path.GetTempPath(), $"dotnetup-{Guid.NewGuid()}{FileExt}");
        Console.WriteLine($"💾 saving archive to tmp location: {filePath}");

        try
        {
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await Client.DownloadDataAsync(download.Url, file, cancel);
            }

            Console.Write($"▶️  validating download against sha512:{download.Hash}...  ");
            if (!string.Equals(GetDigest(filePath), download.Hash, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Error.WriteLine($"❗❗❗  oh no hash mismatch  ❗❗❗");
                Console.Write($"▶️  cleaning up {filePath}... ");
                File.Delete(filePath);
                Console.WriteLine("DONE ✔️");
                Environment.Exit(1);
            }
            Console.WriteLine($"DONE ✔️");

            return Tuple.Create(resolvedVersion, filePath);
        }
        catch
        {
            Console.Write($"▶️  cleaning up {filePath}... ");
            File.Delete(filePath);
            Console.WriteLine("DONE ✔️");
            throw;
        }
    }

    private static string GetDigest(string file)
    {
        using var stream = File.OpenRead(file);
        using var digest = SHA512.Create();
        byte[] checksum = digest.ComputeHash(stream);
        return BitConverter.ToString(checksum).Replace("-", string.Empty);
    }
}
