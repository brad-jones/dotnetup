import ky from "ky";
import { z } from "zod";
import { $ } from "@david/dax";
import { ensureDir } from "@std/fs";

const MAJOR_VERSION = "9.0";

if (await $.commandExists("dotnet")) {
  console.log(`dotnet already installed`);
  Deno.exit(0);
}

const dotnetRoot = Deno.env.get("DOTNET_ROOT");
if (!dotnetRoot) {
  console.log(`DOTNET_ROOT not defined`);
  Deno.exit(1);
}

const downloadUrl = z.object({
  releases: z.array(
    z.object({
      sdk: z.object({
        files: z.array(
          z.object({
            name: z.string(),
            rid: z.string(),
            url: z.string().url(),
            hash: z.string(),
          }),
        ),
      }),
    }),
  ),
}).parse(
  await ky.get(
    `https://raw.githubusercontent.com/dotnet/core/main/release-notes/${MAJOR_VERSION}/releases.json`,
  ).json(),
).releases[0].sdk.files.find((_) => _.rid === "linux-x64")?.url;

if (!downloadUrl) {
  console.log(`failed to find dotnet tarball to download`);
  Deno.exit(1);
}

console.log(downloadUrl);

try {
  await ensureDir(dotnetRoot);
  await $`curl -L --progress-bar ${downloadUrl} -o /tmp/dotnet.tar.gz`;
  await $`tar -xf /tmp/dotnet.tar.gz -C ${dotnetRoot}`;
} finally {
  try {
    await Deno.remove("/tmp/dotnet.tar.gz");
  } catch (_) {
    // swallow any exceptions
  }
}
