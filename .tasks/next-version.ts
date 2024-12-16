import { $ } from "@david/dax";
import { format, parse } from "@std/semver";

try {
  console.log(
    format(
      parse(
        await $`cog bump --dry-run --auto`.text("combined"),
      ),
    ),
  );
} catch (_) {
  console.log("0.0.0");
}
