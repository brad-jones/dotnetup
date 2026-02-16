using System.Text.Json.Serialization;

namespace dotnetup.Services.Releases.DTOs;

[JsonSerializable(typeof(ReleaseNotes))]
[JsonSerializable(typeof(ReleaseNote))]
[JsonSerializable(typeof(ReleaseNoteSdk))]
[JsonSerializable(typeof(ReleaseNoteSdkFile))]
[JsonSourceGenerationOptions(
  WriteIndented = true,
  PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
  GenerationMode = JsonSourceGenerationMode.Metadata
)]
internal partial class SrcGenCtx : JsonSerializerContext { }
