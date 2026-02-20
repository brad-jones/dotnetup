#!/bin/sh

if [ -z "${CI+x}" ]; then
  export DOTNET_ROOT="${CONDA_PREFIX}/lib/dotnet"
  export DOTNET_CLI_HOME="${CONDA_PREFIX}/lib/dotnet/home"
  export DOTNET_ADD_GLOBAL_TOOLS_TO_PATH="false"
  export DOTNET_CLI_TELEMETRY_OPTOUT="true"
  export NUGET_PACKAGES="${CONDA_PREFIX}/var/cache/nuget"
  export PATH="${DOTNET_ROOT}:${PATH}"
fi
