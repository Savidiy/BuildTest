#addin nuget:?package=Cake.Unity&version=0.8.1

using static Cake.Unity.Arguments.BuildTarget;

var target = Argument("target", "Build-Android");

Task("Clean-Artifacts")
    .Does(() =>
{
    CleanDirectory($"./artifacts");    
});

Task("Build-Android")
    .IsDependentOn("Clean-Artifacts")    
    .Does(() =>
{
    // test

    UnityEditor(2020, 3, 11,
        new UnityEditorArguments(){
            ProjectPath = "./src/JsonTest",
            ExecuteMethod = "Editor.Build.Builder.BuildAndroid",
            BuildTarget = Android,
            LogFile = "./artifacts/build.log",
        },
        new UnityEditorSettings(){
            RealTimeLog = true,
        }
    );
});

RunTarget(target);