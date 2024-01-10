using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using dotnetup.Services.Releases.DTOs;

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

    public static async Task<string> GetLatestVersion(CancellationToken cancel = default)
    {
        var notes = await GetReleaseNotes(MajorVersions[0], cancel);
        return notes?.Releases.FirstOrDefault()?.Sdk.Version ?? "0.0.0";
    }

    public static async Task<ReleaseNoteSdkFile> GetVersion(string version, CancellationToken cancel = default)
    {
        var majorVersion = int.Parse(version[..1]);
        var notes = await GetReleaseNotes(majorVersion, cancel);
        var release = notes!.Releases.Single(_ => _.Sdk.Version == version).Sdk;
        return release.Files.Single(_ => _.Rid == RuntimeInformation.RuntimeIdentifier && _.Name.EndsWith(FileExt));
    }

    public static async Task<string> DownloadVersion(string version, CancellationToken cancel = default)
    {
        var download = await GetVersion(version, cancel);
        var filePath = Path.Combine(Path.GetTempPath(), $"dotnetup-{Guid.NewGuid()}.{FileExt}");
        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await Client.DownloadDataAsync(download.Url, file, cancel);
        }

        if (!string.Equals(GetDigest(filePath), download.Hash, StringComparison.InvariantCultureIgnoreCase))
        {
            File.Delete(filePath);
            throw new Exception("downloaded archive does not match digest");
        }

        return filePath;
    }

    private static string GetDigest(string file)
    {
        using var stream = File.OpenRead(file);
        using var digest = SHA512.Create();
        byte[] checksum = digest.ComputeHash(stream);
        return BitConverter.ToString(checksum).Replace("-", string.Empty);
    }
}
