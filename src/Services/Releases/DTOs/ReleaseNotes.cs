namespace dotnetup.Services.Releases.DTOs;

public class ReleaseNotes
{
    public ReleaseNote[] Releases { get; set; } = [];
}

public class ReleaseNote
{
    public ReleaseNoteSdk Sdk { get; set; } = new();
}

public class ReleaseNoteSdk
{
    public string Version { get; set; } = "";
    public ReleaseNoteSdkFile[] Files { get; set; } = [];
}

public class ReleaseNoteSdkFile
{
    public string Name { get; set; } = "";
    public string Rid { get; set; } = "";
    public string Url { get; set; } = "";
    public string Hash { get; set; } = "";
}
