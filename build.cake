#addin nuget:?package=Cake.Unity

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
    Console.WriteLine("Build be here!");
});

RunTarget(target);