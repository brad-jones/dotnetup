namespace dotnetup.Services;

public static class LinkResolver
{
  /// <summary>
  /// So despite the name "ResolveLinkTarget()", doesn't actually resolve anything really.
  /// If the exact path you provide happens to be a link it will resolve that but if the
  /// path is inside a linked directory it just returns null. Hence this method.
  /// </summary>
  public static string Realpath(string path)
  {
    if (!Path.Exists(path))
      return path;

    var origPath = Path.GetFullPath(path);
    var realpath = Path.GetPathRoot(origPath)!;
    var segments = origPath.Replace("\\", "/").Split("/").Where(_ => !string.IsNullOrEmpty(_)).ToArray();

    foreach (var segment in segments)
    {
      var recombined = Path.Combine(realpath, segment);

      if (File.GetAttributes(recombined).HasFlag(FileAttributes.Directory))
      {
        var dirInfo = new DirectoryInfo(recombined);
        var dirLinkTarget = dirInfo.ResolveLinkTarget(true)?.FullName;
        realpath = string.IsNullOrEmpty(dirLinkTarget) ? recombined : dirLinkTarget;
        continue;
      }

      var fileInfo = new FileInfo(recombined);
      var fileLinkTarget = fileInfo.ResolveLinkTarget(true)?.FullName;
      realpath = string.IsNullOrEmpty(fileLinkTarget) ? recombined : fileLinkTarget;
    }

    return realpath;
  }
}
