using ShellProgressBar;

namespace System.Net.Http;

/// <summary>
/// credit: https://gist.github.com/dalexsoto/9fd3c5bdbe9f61a717d47c5843384d11
/// </summary>
public static class HttpClientProgressExtensions
{
  public static async Task DownloadDataAsync(
    this HttpClient client,
    string requestUrl,
    Stream destination,
    CancellationToken cancel = default
  )
  {
    using var response = await client.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead, cancel);
    using var download = await response.Content.ReadAsStreamAsync(cancel);
    var contentLength = response.Content.Headers.ContentLength;

    // Can't report progress if we have no content length
    if (!contentLength.HasValue)
    {
      await download.CopyToAsync(destination, cancel);
      return;
    }

    using var progressBar = new ProgressBar(10000, $"downloading {requestUrl}");
    var progress = progressBar.AsProgress<float>();

    static float GetProgressPercentage(float totalBytes, float currentBytes) => totalBytes / currentBytes;
    var progressWrapper = new Progress<long>(totalBytes =>
      progress.Report(GetProgressPercentage(totalBytes, contentLength.Value))
    );
    await download.CopyToAsync(destination, 81920, progressWrapper, cancel);
  }

  static async Task CopyToAsync(
    this Stream source,
    Stream destination,
    int bufferSize,
    IProgress<long>? progress = null,
    CancellationToken cancel = default
  )
  {
    if (bufferSize < 0)
      throw new ArgumentOutOfRangeException(nameof(bufferSize));

    if (source is null)
      throw new ArgumentNullException(nameof(source));

    if (!source.CanRead)
      throw new InvalidOperationException($"'{nameof(source)}' is not readable.");

    if (destination == null)
      throw new ArgumentNullException(nameof(destination));

    if (!destination.CanWrite)
      throw new InvalidOperationException($"'{nameof(destination)}' is not writable.");

    int bytesRead;
    long totalBytesRead = 0;
    var buffer = new byte[bufferSize];
    while ((bytesRead = await source.ReadAsync(buffer, cancel).ConfigureAwait(false)) != 0)
    {
      await destination.WriteAsync(buffer.AsMemory(0, bytesRead), cancel).ConfigureAwait(false);
      totalBytesRead += bytesRead;
      progress?.Report(totalBytesRead);
    }
  }
}
