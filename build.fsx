#I "tools/FAKE/tools"
#r "FakeLib.dll"

open Fake

// Our project meta information
let projectname = "Algorithm-Implementations"
let authors = ["Florian Kroenert"]

// Versioning
let mutable version = "1.0.0"
let mutable infVersion = "1.0.0"

// Directories for building the project
let buildDir = ".build"
let appBuildDir = buildDir + "/app"

// Directory for building the test projects
let testDir = ".test"

// Directories for publishing our build artifacts
let deployDir = "Publish"
let appDeployDir = deployDir + "/app"


Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
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

Target "Publish" (fun _ ->
    !!(appBuildDir @@ "/**/*.dll")
      |> CopyTo appDeployDir
)

// Targets will be executed from top to bottom
"Clean"
==> "BuildApp"
==> "Publish"

// Run build
RunTargetOrDefault "Publish"