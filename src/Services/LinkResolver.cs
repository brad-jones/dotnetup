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

    // Start from the root ("/" on unix, "C:\" on windows, "\\server\share\" on UNC)
    var root = Path.GetPathRoot(origPath);
    if (string.IsNullOrEmpty(root))
      return origPath;

    var realpath = root;

    // Work with the remainder of the path *after* the root
    var remainder = origPath[root.Length..];

    // Split remainder into segments (no drive letter here)
    var segments = remainder.Split(
      [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar],
      StringSplitOptions.RemoveEmptyEntries
    );

    foreach (var segment in segments)
    {
      var recombined = Path.Combine(realpath, segment);

      // Double check the path exists before trying to resolve it,
      // if it doesn't exist just keep going with the recombined path
      if (!Path.Exists(recombined))
      {
        realpath = recombined;
        continue;
      }

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
