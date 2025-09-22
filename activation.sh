#!/bin/sh

if [ -z "${CI+x}" ]; then
  export DOTNET_ROOT="${CONDA_PREFIX}/lib/dotnet"
  export DOTNET_TOOLS="${DOTNET_ROOT}/tools"
  export DOTNET_TOOLS_PATH="${DOTNET_TOOLS}",
  export DOTNET_ADD_GLOBAL_TOOLS_TO_PATH="false"
  export DOTNET_CLI_TELEMETRY_OPTOUT="true"
  export PATH="${DOTNET_ROOT}:${DOTNET_TOOLS_PATH}:${PATH}"
fi
