using System.Formats.Tar;
using System.IO.Compression;

namespace dotnetup.Services.Archive;

public static class Archive
{
  public static void ExtractToDirectory(string src, string dst)
  {
    Directory.CreateDirectory(dst);

    if (src.EndsWith(".tar.gz"))
    {
      ExtractTarGzToDirectory(src, dst);
    }
    else
    {
      ExtractZipToDirectory(src, dst);
    }
  }

  private static void ExtractZipToDirectory(string src, string dst) => ZipFile.ExtractToDirectory(src, dst, true);

  private static void ExtractTarGzToDirectory(string src, string dst)
  {
    using var sr5cStream = File.Open(src, FileMode.Open);
    using var decompressor = new GZipStream(sr5cStream, CompressionMode.Decompress);
    TarFile.ExtractToDirectory(decompressor, dst, true);
  }
}
