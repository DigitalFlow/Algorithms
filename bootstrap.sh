#!/bin/bash
mono --runtime=v4.0.30319 tools/NuGet/nuget.exe install FAKE -OutputDirectory tools -ExcludeVersion
mono --runtime=v4.0.30319 tools/NuGet/nuget.exe install xunit.runners -OutputDirectory tools -ExcludeVersion
