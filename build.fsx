#I "tools/FAKE/tools"
#r "FakeLib.dll"

open Fake

// Our project meta information
let projectname = "Algorithms"
let authors = ["Florian Kroenert"]

// Versioning
let mutable version = "1.0.0"
let mutable infVersion = "1.0.0"

// Directories for building the project
let buildDir = "build"
let appBuildDir = buildDir + "/app"

// Directory for building the test projects
let testDir = "test"

// Directories for publishing our build artifacts
let deployDir = "Publish"
let appDeployDir = deployDir + "/app"


Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir; testDir]
)

Target "RestoreNuget" (fun _ ->
    RestorePackages()
)

Target "UpdateVersions" (fun _ ->
    BulkReplaceAssemblyInfoVersions "src/" (fun f ->
      {f with
          AssemblyVersion = version
          AssemblyInformationalVersion = infVersion})
)

Target "BuildApp" (fun _ ->
    !!("src/app/**/*.csproj")
      |> MSBuildRelease appBuildDir "Build" 
      |> Log "Build Log: "
)

Target "BuildTest" (fun _ ->
    !!("src/test/**/*.csproj")
      |> MSBuildDebug testDir "Build"
      |> Log "Build Log: "
)

Target "NUnit" (fun _ ->
    let testFiles = !!(testDir @@ "/**/*.Tests.dll")
    if testFiles.Includes.Length <> 0 then
      testFiles
        |> NUnit (fun test ->
             {test with
                   ToolPath = "tools/NUnit.Runners/tools";
                   Framework = "4.5"
                   DisableShadowCopy = true;
                   OutputFile = testDir + "NUnitResults.xml"})
)

Target "Publish" (fun _ ->
    !!(appBuildDir @@ "/**/*.dll")
      |> CopyTo appDeployDir
)

// Targets will be executed from top to bottom
"Clean"
==> "RestoreNuget"
=?> ("UpdateVersions", not isLocalBuild)
==> "BuildApp"
==> "BuildTest"
==> "NUnit"
==> "Publish"

// Run build
RunTargetOrDefault "Publish"